using GlobalBlue_Homework.Model;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue_Homework.Controllers;

[ApiController]
[Route("[controller]")]
public class VatCalculatorController : ControllerBase
{
    private readonly ILogger<VatCalculatorController> _logger;

    public VatCalculatorController(ILogger<VatCalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "FillMissing")]
    public IEnumerable<VatValues> FillMissing(VatValues vatValues)
    {
        throw new NotImplementedException();
    }
}