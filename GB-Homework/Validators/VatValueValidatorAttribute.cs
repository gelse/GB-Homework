using System.ComponentModel.DataAnnotations;
using GB_Homework.Model;

namespace GB_Homework.Validators;

/// <summary>
/// Injectable IVatValueValidator class.
/// </summary>
public interface IVatValueValidator
{
    IEnumerable<ValidationResult> Validate(object? vatValues);
}

/// <summary>
/// Base class for VatValueCalculator and i hope the ONLY class where i broke the KISS and YAGNI principle.
/// But i did it on purpose.
/// </summary>
public abstract class VatValueValidator : IVatValueValidator
{
    protected abstract int[] AllowedVatValues { get; }

    private string AllowedVatValuesString =>
        string.Join(", ", AllowedVatValues);
    
    /// <summary>
    /// Executes the validation.
    /// </summary>
    /// <returns>An enumerable of <see cref="ValidationResult"/>s</returns>
    public IEnumerable<ValidationResult> Validate(object? validatedObject)
    {
        var vatValues = validatedObject as VatCalculationValues;
        if (vatValues == null)
        {
            yield return new ValidationResult("object must not be null.");
            yield break;
        }
        
        if(!AllowedVatValues.Contains(vatValues.VatRate))
            yield return new ValidationResult($"VatRate must be one of the values {AllowedVatValuesString}", new[]{nameof(VatCalculationValues.VatRate)});
        
        if(vatValues.VatAmount <= 0)
            yield return new ValidationResult($"{nameof(VatCalculationValues.VatAmount)} must have a greater value than 0.", new[]{nameof(VatCalculationValues.VatAmount)});
        if(vatValues.NetAmount <= 0)
            yield return new ValidationResult($"{nameof(VatCalculationValues.NetAmount)} must have a greater value than 0.", new[]{nameof(VatCalculationValues.NetAmount)});
        if(vatValues.GrossAmount <= 0)
            yield return new ValidationResult($"{nameof(VatCalculationValues.GrossAmount)} must have a greater value than 0.", new[]{nameof(VatCalculationValues.GrossAmount)});
        
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

/// <summary>
/// Validator for Austria.
/// </summary>
public class AustrianVatValueValidator : VatValueValidator
{
    private static readonly int[] _allowedVatValues = { 10, 13, 20 };

    protected override int[] AllowedVatValues => _allowedVatValues;
}