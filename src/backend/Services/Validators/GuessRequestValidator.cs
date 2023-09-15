using FluentValidation;
using Services.Enums;
using Services.Requests;

namespace Services.Validators;

public class GuessRequestValidator : AbstractValidator<GuessRequest>
{
    public GuessRequestValidator()
    {
        RuleFor(r => r.Value)
            .Must((_, value) => value >= 0 && value <= 100)
            .WithMessage("Value should be between 0 and 100");
    }
}