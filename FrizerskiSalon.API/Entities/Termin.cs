using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrizerskiSalon.API.Entities;

public class Termin
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateTime Datum { get; set; }
    
    [Required]
    public string Status { get; set; } = null!;

    // Foreign keys
    [Required, ForeignKey("Radnik")]
    public int RadnikID { get; set; }
    public Radnik? Radnik { get; set; }

    [Required, ForeignKey("Klijent")]
    public int KlijentID { get; set; }
    public Klijent? Klijent { get; set; }

    [Required, ForeignKey("Usluga")]
    public int UslugaID { get; set; }
    public Usluga? Usluga { get; set; }
}
