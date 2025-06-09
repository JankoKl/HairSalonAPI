using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.DTOs;

public record class UpdateKlijentDTO(
[Required] [StringLength(30)] string Ime, 
[Required] [StringLength(30)] string Prezime, 
[Required] [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$")] string BrTel);


