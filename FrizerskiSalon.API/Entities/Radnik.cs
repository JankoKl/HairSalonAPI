using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.Entities;

public class Radnik
{
    [Key]
    public int Id { get; set; }
    
    [Required, StringLength(30)]
    public string Ime { get; set; } = null!;
    
    [Required, StringLength(30)]
    public string Prezime { get; set; } = null!;
    
    [Required, Phone]
    public string BrTel { get; set; } = null!;
    
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string Pozicija { get; set; } = null!;

  
    public List<Termin> Termini { get; set; } = new();
}