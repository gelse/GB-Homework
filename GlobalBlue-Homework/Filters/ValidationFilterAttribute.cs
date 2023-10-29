using GlobalBlue_Homework.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalBlue_Homework.Filters;

public class ValidationFilterAttribute : IActionFilter
{
    private readonly IVatValueValidator _vatValueValidator;

    public ValidationFilterAttribute(IVatValueValidator vatValueValidator)
    {
        _vatValueValidator = vatValueValidator ?? throw new ArgumentNullException(nameof(vatValueValidator));
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // custom validation
        foreach (var error in _vatValueValidator.Validate(context.ActionArguments["vatCalculationValues"]))
        {
            if(error.MemberNames.Any())
            {
                foreach (var errorMemberName in error.MemberNames)
                {
                    context.ModelState.AddModelError(errorMemberName, error.ErrorMessage ?? "An error occurred.");
                }
            }
            else
            {
                context.ModelState.AddModelError(string.Empty, error.ErrorMessage ?? "An error occurred.");
            }
        }
        
        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // NOOP
    }
}