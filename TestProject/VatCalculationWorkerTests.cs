using FluentAssertions;
using GB_Homework.Exceptions;
using GB_Homework.Model;
using GB_Homework.Worker;

namespace TestProject;

public class VatCalculationWorkerTests
{
    [Theory]
    [InlineData(10, 10, null, null, 10, 1, 11)]
    [InlineData(10, null, 1, null, 10, 1, 11)]
    [InlineData(10, null, null, 11, 10, 1, 11)]
    public void ValidValues_Test(int vatRate, int? netAmount, int? vatAmount, int? grossAmount,  
        int? expectedNetAmount, int? expectedVatAmount, int? expectedGrossAmount)
    {
        new VatCalculationWorker()
            .Run(new VatCalculationValues()
            {
                VatRate = vatRate,
                NetAmount = netAmount,
                VatAmount = vatAmount,
                GrossAmount = grossAmount
            })
            .Should().BeEquivalentTo(new VatCalculationValues()
            {
                VatRate = vatRate,
                NetAmount = expectedNetAmount,
                VatAmount = expectedVatAmount,
                GrossAmount = expectedGrossAmount,
            });
    }

    [Fact]
    public void ThrowsArgumentNullException_OnNullInput()
    {
        new Action(() => new VatCalculationWorker().Run(null))
            .Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void ThrowsAmbiguousInputException_OnEmptyInput()
    {
        new Action(() => new VatCalculationWorker().Run(new VatCalculationValues()))
            .Should().ThrowExactly<AmbiguousInputException>();
    }

    [Fact]
    public void ThrowsAmbiguousInputException_OnAmbiguousInput()
    {
        new Action(() => new VatCalculationWorker().Run(new VatCalculationValues()
            {
                VatRate = 10,
                VatAmount = 1,
                NetAmount = 10
            }))
            .Should().ThrowExactly<AmbiguousInputException>();
    }
}