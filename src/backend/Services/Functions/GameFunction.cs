using System.Linq;
using System.Threading.Tasks;
using DarkLoop.Azure.Functions.Authorize;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.CServices;
using Services.Enums;
using Services.Extensions;
using Services.Responses;

namespace Services.Functions;

public sealed class GameFunction
{
    private readonly IGameService _gameService;
    
    public GameFunction(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    [FunctionName("GameFunction")]
    [FunctionAuthorize]
    public async Task<ActionResult<GuessingResponse>> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "games/target/guess")] HttpRequest req, ILogger log)
    {
        var parsed = int.TryParse(req.Query["value"].ToString(), out int value);
        if (!parsed)
        {
            return new BadRequestResult();
        }

        var userId = req.HttpContext.User.TryGetUserId();
        if (userId is null)
        {
            return new ForbidResult();
        }

        try
        {
            var result = await _gameService.GuessTargetAsync(int.Parse(userId), value);
            return new GuessingResponse { GuessingResult = result };
        }
        catch (ValidationException exception)
        {
            return Validation.ResponseWithProblemDetails(exception.Errors.ToList());
        }
    }
}