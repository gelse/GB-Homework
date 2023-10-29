using GB_Homework.Exceptions;
using GB_Homework.Model;

namespace GB_Homework.Worker;

public interface IVatWorker
{
    public VatCalculationValues Run(VatCalculationValues request);
}

public class VatWorker : IVatWorker
{
    public VatCalculationValues Run(VatCalculationValues request)
    {
        decimal grossAmount;
        decimal netAmount;
        decimal vatAmount;
        
        switch (request)
        {
            case { GrossAmount: not null, NetAmount: null, VatAmount: null }:
                grossAmount = request.GrossAmount.Value;
                netAmount = request.GrossAmount.Value / (1M + 0.01M * request.VatRate);
                vatAmount = grossAmount - netAmount;
                break;
            case { GrossAmount: null, NetAmount: not null, VatAmount: null }:
                netAmount = request.NetAmount.Value;
                vatAmount = netAmount * 0.01M * request.VatRate;
                grossAmount = netAmount + vatAmount;
                break;
            case { GrossAmount: null, NetAmount: null, VatAmount: not null }:
                vatAmount = request.VatAmount.Value;
                netAmount = vatAmount / (0.01M * request.VatRate);
                grossAmount = netAmount + vatAmount;
                
                break;
            default:
                throw new AmbiguousInputException(request.GrossAmount, request.NetAmount, request.VatAmount);
        }
        
        return new VatCalculationValues()
        {
            GrossAmount = grossAmount,
            NetAmount = netAmount,
            VatAmount = vatAmount,
            VatRate = request.VatRate
        };
    }
}