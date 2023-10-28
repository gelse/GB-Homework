using System.ComponentModel.DataAnnotations;

namespace GlobalBlue_Homework.Model;

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

public class VatValuesRequest
{
    [Required]
    public int VatRate { get; set; }
    
    public decimal? GrossAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal? NetAmount { get; set; }
}