using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.DTOs;

public record class UslugaDTO(
    [Required]int Id,
    [Required] [StringLength(30)]string Naziv,
    [StringLength(100)]string Opis,
    [Required] [Range(0, 10000)] int Cena
);



