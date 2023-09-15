using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Constants;
using Microsoft.Extensions.Logging;
using Services.CServices;
using Services.Entities;
using Services.Requests;

namespace Services.Functions
{
    public sealed class LoginFunction
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IValidator<LoginRequest> _requestValidator;
        private readonly ApplicationDbContext _dbContext;

        public LoginFunction(
            IJwtGenerator jwtGenerator,
            IValidator<LoginRequest> requestValidator,
            ApplicationDbContext dbContext)
        {
            _jwtGenerator = jwtGenerator;
            _requestValidator = requestValidator;
            _dbContext = dbContext;
        }
        
        [FunctionName("LoginFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get","post", Route = "login")] HttpRequest req,
            ILogger log)
        {
            var request = new LoginRequest
            {
                Name = req.Query["name"],
                Password = req.Query["password"]
            };

            var result = await _requestValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Validation.ResponseWithProblemDetails(result.Errors);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Name);
            if (user is null)
            {
                user = new User
                {
                    Name = request.Name,
                    Password = request.Password,
                    Games = new List<Game>()
                };
                
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                if (user.Password != request.Password)
                {
                    return new UnauthorizedResult();
                }
            }
            
            var claims = new List<Claim>
            {
                new(ClaimTypeConstants.UserId, user.Id.ToString()),
                new(ClaimTypeConstants.UserName, user.Name)
            };

            var token = _jwtGenerator.Generate(claims);
            return new OkObjectResult(token);
        }
    }
}
