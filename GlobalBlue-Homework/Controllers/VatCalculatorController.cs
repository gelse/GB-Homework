using System.Net;
using GlobalBlue_Homework.Exceptions;
using GlobalBlue_Homework.Filters;
using GlobalBlue_Homework.Model;
using GlobalBlue_Homework.Validators;
using GlobalBlue_Homework.Worker;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue_Homework.Controllers;

[ApiController]
[Route("[controller]")]
public class VatCalculatorController : ControllerBase
{
    private readonly ILogger<VatCalculatorController> _logger;
    private readonly IVatValueValidator _vatValueValidator;
    private readonly IVatWorker _vatWorker;

    public VatCalculatorController(ILogger<VatCalculatorController> logger, IVatValueValidator vatValueValidator, IVatWorker vatWorker)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _vatValueValidator = vatValueValidator ?? throw new ArgumentNullException(nameof(vatValueValidator));
        _vatWorker = vatWorker ?? throw new ArgumentNullException(nameof(vatWorker));
    }

    /// <summary>
    /// Calculates the missing values for the VAT of Austria.
    /// </summary>
    [HttpPost(Name = "FillMissing")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public IActionResult FillMissing(VatValuesRequest vatValues)
    {
        try
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            return Ok(_vatWorker.Run(vatValues));
        }
        catch (AmbiguousInputException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return BadRequest(ModelState);
        }
    }
}