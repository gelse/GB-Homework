using System.Net;
using GB_Homework.Exceptions;
using GB_Homework.Filters;
using GB_Homework.Model;
using GB_Homework.Validators;
using GB_Homework.Worker;
using Microsoft.AspNetCore.Mvc;

namespace GB_Homework.Controllers;

[ApiController]
[Route("[controller]")]
public class VatCalculationValuesController : ControllerBase
{
    private readonly ILogger<VatCalculationValuesController> _logger;
    private readonly IVatValueValidator _vatValueValidator;
    private readonly IVatWorker _vatWorker;

    public VatCalculationValuesController(ILogger<VatCalculationValuesController> logger, IVatValueValidator vatValueValidator, IVatWorker vatWorker)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _vatValueValidator = vatValueValidator ?? throw new ArgumentNullException(nameof(vatValueValidator));
        _vatWorker = vatWorker ?? throw new ArgumentNullException(nameof(vatWorker));
    }

    /// <summary>
    /// Calculates the missing values for the VAT of Austria.
    /// </summary>
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public IActionResult FillVatCalculationValues(VatCalculationValues vatCalculationValues)
    {
        try
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            return Ok(_vatWorker.Run(vatCalculationValues));
        }
        catch (AmbiguousInputException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return BadRequest(ModelState);
        }
    }
}