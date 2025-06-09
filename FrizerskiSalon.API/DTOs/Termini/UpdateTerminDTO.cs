using System.ComponentModel.DataAnnotations;
namespace FrizerskiSalon.API.DTOs;
using System;
using System.ComponentModel.DataAnnotations;



public record class UpdateTerminDTO(
[Required] [FutureDate] DateTime Datum,
    [Required] int RadnikID,
    [Required] int KlijentID,
    [Required] int UslugaID,
    [Required] string Status
);