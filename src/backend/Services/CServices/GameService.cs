using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Entities;
using Services.Enums;
using Services.Requests;

namespace Services.CServices;

public interface IGameService
{
    Task<GuessingResult> GuessTargetAsync(int userId, int value);
}

public sealed class GameService : IGameService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IRandomizer _randomizer;
    private readonly IValidator<GuessRequest> _requestValidator;

    public GameService(
        ApplicationDbContext dbContext,
        IRandomizer randomizer,
        IValidator<GuessRequest> requestValidator)
    {
        _dbContext = dbContext;
        _randomizer = randomizer;
        _requestValidator = requestValidator;
    }

    public async Task<GuessingResult> GuessTargetAsync(int userId, int value)
    {
        var request = new GuessRequest
        {
            Value = value
        };

        await _requestValidator.ValidateAndThrowAsync(request);
        
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.UserId == userId && g.State == GameState.Started);
        if (game is not null)
        {
            var result = GetGuessingResult(game, request.Value);
            if (result is GuessingResult.Guessed)
            {
                game.State = GameState.Finished;
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }
        else
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Games)
                .FirstAsync();
            
            game = new Game
            {
                User = user,
                Target = _randomizer.GetInteger(0, 100),
                State = GameState.Started,
                UserId = user.Id
            };
            
            user.Games.Add(game);
            
            var result = GetGuessingResult(game, request.Value);
            if (result is GuessingResult.Guessed)
            {
                game.State = GameState.Finished;
            }
            
            await _dbContext.SaveChangesAsync();
            
            return result;
        }
    }

    private GuessingResult GetGuessingResult(Game game, int value)
    {
        if (game.Target > value)
        {
            return GuessingResult.More;
        }

        if (game.Target < value)
        {
            return GuessingResult.Less;
        }
        
        return GuessingResult.Guessed;
    }
}