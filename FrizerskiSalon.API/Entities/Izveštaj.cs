using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.Entities;

public class Izvestaj
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Datum { get; set; }

    [Required]
    public int BrojTermina { get; set; }

    [Required]
    public int BrojRazlicitihKlijenata { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal UkupnaZarada { get; set; }
}
