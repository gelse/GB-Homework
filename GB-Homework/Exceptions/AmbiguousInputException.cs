namespace GB_Homework.Exceptions;

/// <summary>
/// Describes an error where we could not calculate the missing values, either because we have too few or too many values.
/// </summary>
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