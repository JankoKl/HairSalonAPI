using System.ComponentModel.DataAnnotations;
namespace FrizerskiSalon.API.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date <= DateTime.Now)
                return new ValidationResult("Date must be atleast one day in future");
        }
        return ValidationResult.Success;
    }
}
public record class CreateTerminDTO(
    [Required] [FutureDate] DateTime Datum,
    [Required] int RadnikID,
    [Required] int KlijentID,
    [Required] int UslugaID,
    [Required] string Status
);