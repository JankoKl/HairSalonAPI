using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.Entities;

public class Klijent
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(30)]
    public string Ime { get; set; } = null!;
    
    [Required, StringLength(30)]
    public string Prezime { get; set; } = null!;
    
    [Required, Phone]
    public string BrTel { get; set; } = null!;

    public List<Termin> Termini { get; set; } = new();
}
