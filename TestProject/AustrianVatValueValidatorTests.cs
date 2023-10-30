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
        var validator = new AustrianVatValueValidator();
        new Action(() => validator.Validate(null).ToList()).Should().NotThrow();
    }

    [Fact]
    public void Null_returns_ValidationResult()
    {
        var validator = new AustrianVatValueValidator();
        validator.Validate(null).Should()
            .ContainSingle()
            .Which.ErrorMessage.Should().Be("object must not be null.");
    }

    [Fact]
    public void WrongVatRate_returns_ValidationResult()
    {
        var validator = new AustrianVatValueValidator();
        validator.Validate(new VatCalculationValues
            {
                VatRate = 12,
                GrossAmount = 10
            }).ToList().Should()
            .ContainSingle()
            .Should().BeEquivalentTo(new {
                ErrorMessage = "VatRate must be one of the values 10, 13, 20", 
                MemberNames = new []{"VatRate"}
                }, options => options.ExcludingMissingMembers());
    }
}