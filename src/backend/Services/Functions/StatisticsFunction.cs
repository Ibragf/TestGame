using System.Threading.Tasks;
using DarkLoop.Azure.Functions.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Services.CServices;
using Services.Extensions;
using Services.Responses;

namespace Services.Functions;

public sealed class StatisticsFunction
{
    private readonly IStatisticsService _statisticsService;
    
    public StatisticsFunction(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    [FunctionAuthorize]
    [FunctionName("StatisticsFunction")]
    public async Task<ActionResult<GamesCountResponse>> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "games/count")] HttpRequest req, ILogger log)
    {
        var userId = req.HttpContext.User.TryGetUserId();
        if (userId is null)
        {
            return new ForbidResult();
        }

        var gamesCount = await _statisticsService.GetFinishedGamesCountAsync(int.Parse(userId));
        var response = new GamesCountResponse
        {
            UserName = req.HttpContext.User.TryGetUserName(),
            GamesCount = gamesCount
        };
        
        return response;
    }
}