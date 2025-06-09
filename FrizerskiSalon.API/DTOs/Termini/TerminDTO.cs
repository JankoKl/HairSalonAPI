using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.API.DTOs;

public record class TerminDTO(
[Required] int Id, 
[Required] DateOnly Datum,
[Required] string Status,
int RadnikID,
string RadnikIme,
int KlijentID,
string KlijentIme,
int UslugaID,
string UslugaNaziv,
double UslugaCena);
