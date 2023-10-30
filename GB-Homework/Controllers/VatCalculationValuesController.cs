using System.Net;
using GB_Homework.Exceptions;
using GB_Homework.Filters;
using GB_Homework.Model;
using GB_Homework.Validators;
using GB_Homework.Worker;
using Microsoft.AspNetCore.Mvc;

namespace GB_Homework.Controllers;

/// <summary>
/// REST controller for <see cref="VatCalculationValues"/>
/// </summary>
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
    /// POST verb, as that is the closest what we have.
    /// we do not have real REST here, as this endpoint, according to the description is a custom functionality
    /// and a mixture of typical "GET" and "POST" entity. After all, we do not really have an entity here.
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