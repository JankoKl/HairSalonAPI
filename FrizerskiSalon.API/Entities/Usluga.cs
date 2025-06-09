using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.Entities;

public class Usluga
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(50)]
    public string Naziv { get; set; } = null!;
    
    [Required, StringLength(200)]
    public string Opis { get; set; } = null!;
    
    [Required, Range(0, double.MaxValue)]
    public decimal Cena { get; set; }


    public List<Termin> Termini { get; set; } = new();
}
