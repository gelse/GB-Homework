using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GB_Homework.Model;

// that would be one solution of the validation problem of the vat validation.
// as there are only 3 possible values - and it would also be visible in swagger.
// but it would counteract the given error request "missing or invalid (0 or non-numeric) VAT rate input"
// and ALSO - with a different validator registered my approach would work for any other values as well 
// public enum VatRate
// {
//     VatRate10 = 10,
//     VatRate13 = 13,
//     VatRate30 = 30
// }

/// <summary>
/// Wrapper object that represents the different amounts of VAT.
/// </summary>
public class VatCalculationValues
{
    /// <summary>
    /// The VAT rate as int - in austria restricted to the values 10, 13 and 20
    /// </summary>
    [Required]
    public int VatRate { get; set; }
    
    /// <summary>
    /// The gross (brutto) amount. Must be greater than 0. 
    /// </summary>
    [Range(double.Epsilon, double.MaxValue)]
    public decimal? GrossAmount { get; set; }
    
    /// <summary>
    /// The amount of the vat. Must be greater than 0. 
    /// </summary>
    [Range(double.Epsilon, double.MaxValue)]
    public decimal? VatAmount { get; set; }
    
    /// <summary>
    /// The net (netto) amount. Must be greater than 0. 
    /// </summary>
    [Range(double.Epsilon, double.MaxValue)]
    public decimal? NetAmount { get; set; }
}