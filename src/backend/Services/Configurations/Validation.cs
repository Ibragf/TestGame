using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.Extensions.DependencyInjection;

public static class Validation
{
    public static ActionResult ResponseWithProblemDetails(List<ValidationFailure> errors)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return new BadRequestObjectResult(modelState);
    }
}