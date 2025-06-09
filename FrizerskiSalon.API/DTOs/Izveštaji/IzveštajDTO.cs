using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.DTOs;

public record class IzvestajDTO(
    [Required] DateOnly Datum,  
    [Required] double BrojTermina,
    [Required] int BrojKlijenata,  
    [Required] double UkupanPrihod 
);
