using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Enums;

namespace Services.CServices;

public interface IStatisticsService
{
    Task<int> GetFinishedGamesCountAsync(int userId);
}

public class StatisticsService : IStatisticsService
{
    private readonly ApplicationDbContext _dbContext;
    
    public StatisticsService(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetFinishedGamesCountAsync(int userId)
    {
        return await _dbContext.Games
            .Where(g => g.UserId == userId && g.State == GameState.Finished)
            .CountAsync();
    }
}