namespace GlobalBlue_Homework.Model;

public class VatValuesResult
{
    public int VatRate { get; set; }
    
    public decimal GrossAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal NetAmount { get; set; }
}