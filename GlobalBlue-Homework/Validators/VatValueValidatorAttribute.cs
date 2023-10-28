using System.ComponentModel.DataAnnotations;
using GlobalBlue_Homework.Model;

namespace GlobalBlue_Homework.Validators;

public interface IVatValueValidator
{
    IEnumerable<ValidationResult> Validate(object? vatValues);
}

/// <summary>
/// Base class for VatValueCalculator and i hope the ONLY class where i broke the KISS and YAGNI principle.
/// But i did it on purpose.
/// </summary>
public abstract class VatValueCalculator : IVatValueValidator
{
    protected abstract int[] AllowedVatValues { get; }

    private string AllowedVatValuesString =>
        string.Join(", ", AllowedVatValues);
    
    public IEnumerable<ValidationResult> Validate(object? validatedObject)
    {
        var vatValues = validatedObject as VatValuesRequest;
        if (vatValues == null)
        {
            yield return new ValidationResult("object must not be null.");
            yield break;
        }
        
        if(!AllowedVatValues.Contains(vatValues.VatRate))
            yield return new ValidationResult($"VatRate must be one of the values {AllowedVatValuesString}", new[]{nameof(VatValuesRequest.VatRate)});
        
        switch (vatValues)
        {
            case { GrossAmount: not null, NetAmount: null, VatAmount: null }:
                break;
            case { GrossAmount: null, NetAmount: not null, VatAmount: null }:
                break;
            case { GrossAmount: null, NetAmount: null, VatAmount: not null }:
                break;
            default:
                yield return new ValidationResult($"Exactly one of vat, net or gross amount must be given.", new[]{string.Empty});
                break;
        }
    }
}

public class AustrianVatValueValidator : VatValueCalculator
{
    private static readonly int[] _allowedVatValues = { 10, 13, 20 };

    protected override int[] AllowedVatValues => _allowedVatValues;
}