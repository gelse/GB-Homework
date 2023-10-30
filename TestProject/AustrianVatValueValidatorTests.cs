using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using GB_Homework.Model;
using GB_Homework.Validators;

namespace TestProject;

public class AustrianVatValueValidatorTests
{
    [Fact]
    public void Null_Should_Not_Throw()
    {
        new Action(() => new AustrianVatValueValidator().Validate(null).ToList())
            .Should()
            .NotThrow();
    }

    [Fact]
    public void Null_returns_ValidationResult()
    {
        new AustrianVatValueValidator()
            .Validate(null)
            .Should()
            .ContainSingle()
            .Which.ErrorMessage.Should().Be("object must not be null.");
    }

    [Fact]
    public void WrongVatRate_returns_ValidationResult()
    {
        new AustrianVatValueValidator()
            .Validate(new VatCalculationValues
                {
                    VatRate = 12,
                    GrossAmount = 10
                })
            .Should()
            .ContainSingle()
            .Should()
            .BeEquivalentTo(new {
                ErrorMessage = "VatRate must be one of the values 10, 13, 20", 
                MemberNames = new []{"VatRate"}
                }, options => options.ExcludingMissingMembers());
    }

    [Theory]
    [InlineData(null,null,10)]
    [InlineData(null,10,null)]
    [InlineData(10,null,null)]
    public void ValidValues_Test(int? grossAmount, int? vatAmount, int? netAmount)
    {
        new AustrianVatValueValidator().Validate(new VatCalculationValues
        {
            GrossAmount = grossAmount,
            VatAmount = vatAmount,
            NetAmount = netAmount,
            VatRate = 10
        }).Should().BeEmpty();
    }

    [Theory]
    [InlineData(null,20,10)]
    [InlineData(20,10,null)]
    [InlineData(10,null,20)]
    [InlineData(null,null,null)]
    [InlineData(10,20,30)]
    public void NotExactlyOneAmountValue_Test(int? grossAmount, int? vatAmount, int? netAmount)
    {
        new AustrianVatValueValidator().Validate(new VatCalculationValues
        {
            GrossAmount = grossAmount,
            VatAmount = vatAmount,
            NetAmount = netAmount,
            VatRate = 10
        }).Should()
        .ContainSingle()
        .Should()
        .BeEquivalentTo(new {
            ErrorMessage = "Exactly one of vat, net or gross amount must be given.", 
            MemberNames = new []{string.Empty}
        }, options => options.ExcludingMissingMembers());
    }

    [Theory]
    [InlineData(-10,null,null, "GrossAmount")]
    [InlineData(0,null,null, "GrossAmount")]
    [InlineData(null,-10,null, "VatAmount")]
    [InlineData(null,0,null, "VatAmount")]
    [InlineData(null,null,-10, "NetAmount")]
    [InlineData(null,null,0, "NetAmount")]
    public void AmountValuesMustBeGtZero(int? grossAmount, int? vatAmount, int? netAmount, string propertyName)
    {
        new AustrianVatValueValidator().Validate(new VatCalculationValues
            {
                GrossAmount = grossAmount,
                VatAmount = vatAmount,
                NetAmount = netAmount,
                VatRate = 10
            }).Should()
            .ContainSingle()
            .Should()
            .BeEquivalentTo(new {
                ErrorMessage = $"{propertyName} must have a greater value than 0.", 
                MemberNames = new []{propertyName}
            }, options => options.ExcludingMissingMembers());
    }
}