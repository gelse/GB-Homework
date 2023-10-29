namespace GB_Homework.Exceptions;

public class AmbiguousInputException : Exception
{
    public decimal? GrossAmount { get; }
    public decimal? NetAmount { get; }
    public decimal? VatAmount { get; }

    public AmbiguousInputException(decimal? grossAmount, decimal? netAmount, decimal? vatAmount)
        : base($"Exactly one of vat {vatAmount}, net {netAmount} or gross {grossAmount} amount must be given.")
    {
        GrossAmount = grossAmount;
        NetAmount = netAmount;
        VatAmount = vatAmount;
    }
}