using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.DTOs;

public record class UpdateRadnikDTO(
[Required] [StringLength(30)]string Ime, 
[Required] [StringLength(30)] string Prezime, 
[RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Please enter a valid phone number")] string BrTel, 
[Required] [DataType(DataType.EmailAddress)] [EmailAddress(ErrorMessage = "Please enter a valid email address")]string Email, 
[Required]string Pozicija );
