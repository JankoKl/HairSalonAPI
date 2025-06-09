using System.Globalization;
using FrizerskiSalon.API.DTOs;

namespace FrizerskiSalon.API.Endpoints;

public static class Endpoints{
    private static readonly List<RadnikDTO> zaposleni = [
    new (1, "Marko", "Jovanović", "+381641234567", "marko.jovanovic@example.com", "Frizer"),
    new (2, "Ana", "Petrović", "+381652345678", "ana.petrovic@example.com", "Pomoćnik"),
    new (3, "Milan", "Nikolić", "+381631112233", "milan.nikolic@example.com", "Pomoćnik"),
    new (4, "Ivana", "Stojanović", "+381691234567", "ivana.stojanovic@example.com", "Čistačica"),
    new (5, "Stefan", "Radović", "+381612223344", "stefan.radovic@example.com", "Administrator")
    ];

private static readonly List<UslugaDTO> usluge = [
    new (1, "Šišanje", "Skraćivanje, sredjivanje, podšišavanje", 600),
    new (2, "Brijanje", "Skraćivanje, oblikovanje brade i brkova", 200),
    new (3, "Pranje kose", "Pranje kose šamponom i sušenje fenom", 200),
];

private static readonly List<KlijentDTO> klijenti= [
    new (1, "Miloš", "Milić", "+3816600001"),
    new (2, "Miladin", "Marić", "+3816600001"),
    new (3, "Milica","Jović","+381693000"),
    new (4, "Tatjana", "Konstatinović", "+38164000770")
];

private static readonly List<TerminDTO> termini = [
    new (1, new DateOnly(2025, 2, 10), "Zakazan", 1, "Marko Jovanović", 1, "Miloš Milić", 1, "Šišanje", 600),
     new (2, new DateOnly(2025, 2, 12), "Otkazan", 2, "Ana Petrović", 3, "Milica Jović", 2, "Brijanje", 200),
     new (3, new DateOnly(2025, 2, 15), "Završen", 3, "Milan Nikolić", 2, "Miladin Marić", 3, "Pranje kose", 200),
      new (1, new DateOnly(2025, 3, 10), "Zakazan", 1, "Marko Jovanović", 1, "Miloš Milić", 1, "Šišanje", 600),
     new (2, new DateOnly(2025, 3, 12), "Otkazan", 2, "Ana Petrović", 3, "Milica Jović", 2, "Brijanje", 200),
     new (3, new DateOnly(2025, 3, 15), "Završen", 3, "Milan Nikolić", 2, "Miladin Marić", 3, "Pranje kose", 200)
];



const String GetRadnikEndpointName = "GetRadnik";
const String GetUslugaEndpointName = "GetUsluga";
const String GetKlijentEndpointName = "GetKlijent";
const String GetTerminEndpointName = "GetTermin";
public static WebApplication MapEndpoints(this WebApplication app){
    //------------------------------ZAPOSLENI----------------------------------------------------\\

// PUT /zaposleni/{id}
app.MapPut("zaposleni/{id}", (int id, UpdateRadnikDTO updatedRadnik) => {

var index = zaposleni.FindIndex(radnik => radnik.Id == id);
zaposleni[index] = new RadnikDTO(
    id,
    updatedRadnik.Ime,
    updatedRadnik.Prezime,
    updatedRadnik.BrTel,
    updatedRadnik.Email,
    updatedRadnik.Pozicija
);

return Results.NoContent();
    
}).WithParameterValidation();

//POST /zaposleni
app.MapPost("zaposleni", (CreateRadnikDTO newRadnik) => 
{
    RadnikDTO radnik = new(
        zaposleni.Count + 1,
        newRadnik.Ime,
        newRadnik.Prezime,
        newRadnik.BrTel,
        newRadnik.Email,
        newRadnik.Pozicija
    );
    zaposleni.Add(radnik);
    return Results.CreatedAtRoute(GetRadnikEndpointName, new  { id = radnik.Id }, radnik);
}).WithParameterValidation();

// GET /zaposleni/{id}
app.MapGet("zaposleni/{id}", (int id) => { 
    
   RadnikDTO? radnik = zaposleni.Find(radnik => radnik.Id == id);
   if(radnik is null){
    return Results.NotFound();
   }
   else return Results.Ok(radnik);
    
    })
   .WithName (GetRadnikEndpointName);

// GET /zaposleni
app.MapGet("zaposleni", () => zaposleni);

// DELETE /zaposleni/{id}
app.MapDelete("zaposleni/{id}", (int id) =>{
RadnikDTO? radnik = zaposleni.Find(radnik => radnik.Id==id);
if(radnik is null){
    return Results.NotFound();
}
else{
zaposleni.RemoveAll(radnik => radnik.Id==id);
return Results.Ok(radnik);}

});

//---------------------------------USLUGE--------------------------------------------------------

// PUT usluge/{id}
app.MapPut("usluge/{id}", (int id, UpdateUslugaDTO updatedUsluga) =>
{
    int index = usluge.FindIndex(usluga => usluga.Id == id);
    
    if(index == -1)
    {
        return Results.NotFound();
    }
    else
    {
        
        var updated = new UslugaDTO(
            id,
            updatedUsluga.Naziv,
            updatedUsluga.Opis,
            updatedUsluga.Cena
        );
        
        usluge[index] = updated;
        
        return Results.Ok(updated);
    }
}).WithParameterValidation();

// DELETE Usluga/{id}
  app.MapDelete("usluge/{id}", (int id) =>{
UslugaDTO? usluga = usluge.Find(usluga => usluga.Id==id);
if(usluga is null){
    return Results.NotFound();
}
else{
usluge.Remove(usluga);
return Results.Ok(usluga);}

});

// GET usluge
app.MapGet("usluge", () => usluge);

// GET usluge/{id}
app.MapGet("usluge/{id}", (int id) =>{
    UslugaDTO? usluga = usluge.Find(usluga => usluga.Id == id);
    if(usluga is null){
        return Results.NotFound();
    }
    else{
        return Results.Ok(usluga);
    }
})
.WithName(GetUslugaEndpointName);

// POST usluge
app.MapPost("usluge", (CreateUslugaDTO newUsluga) =>{
    UslugaDTO usluga = new(
        usluge.Count +1,
        newUsluga.Naziv,
        newUsluga.Opis,
        newUsluga.Cena
    );
    usluge.Add(usluga);
    return Results.CreatedAtRoute(GetUslugaEndpointName, new{id = usluga.Id}, usluga);

}).WithParameterValidation();

//----------------------------KLIJENTI------------------------------------------------
// PUT klijenti/{id}
app.MapPut("klijenti/{id}", (int id, UpdateKlijentDTO updatedKlijent) => {

int index = klijenti.FindIndex(klijent => klijent.Id==id);
if(index==-1){
    return Results.NotFound();
}
else{
    KlijentDTO klijent = new(
        id,
        updatedKlijent.Ime,
        updatedKlijent.Prezime,
        updatedKlijent.BrTel
    ); 
    klijenti[index]=klijent;
    return Results.Ok(klijent);
}
}).WithParameterValidation();


// DELETE klijenti/{id}
app.MapDelete("klijenti/{id}", (int id) => {
    KlijentDTO? klijent = klijenti.Find(klijent => klijent.Id == id); 
    if(klijent is null){
        return Results.NotFound();
    }
    else{
        klijenti.Remove(klijent);
        return Results.Ok(klijent);
    }
});

//GET klijenti
app.MapGet("klijenti", () => klijenti);
//GET klijenti/{id}
app.MapGet("klijenti/{id}", (int id) => {
    KlijentDTO? klijent = klijenti.Find(klijent => klijent.Id == id);
    if(klijent is null){
        return Results.NotFound();
    }
    else{
        return Results.Ok(klijent);
    }
}).WithName(GetKlijentEndpointName);

// POST klijenti
app.MapPost("klijenti", (CreateKlijentDTO newKlijent) => {
    
    KlijentDTO klijent = new(
        klijenti.Count+1,
        newKlijent.Ime,
        newKlijent.Prezime,
        newKlijent.BrTel
    );
    klijenti.Add(klijent);
    return Results.CreatedAtRoute(GetKlijentEndpointName, new{id = klijent.Id
    }, klijent);

}).WithParameterValidation();

 // ------------------------------ TERMINI --------------------------------
        app.MapGet("termini", () => termini);

        app.MapGet("termini/{id}", (int id) => {
            TerminDTO? termin = termini.Find(t => t.Id == id);
            return termin is null ? Results.NotFound() : Results.Ok(termin);
        }).WithName(GetTerminEndpointName);

// POST termini

        app.MapPost("termini", (CreateTerminDTO newTermin) => {
            TerminDTO termin = new(
                termini.Count + 1,
                DateOnly.FromDateTime(newTermin.Datum),
                newTermin.Status,
                newTermin.RadnikID,
                zaposleni.Find(z => z.Id == newTermin.RadnikID)?.Ime ?? "Nepoznato",
                newTermin.KlijentID,
                klijenti.Find(k => k.Id == newTermin.KlijentID)?.Ime ?? "Nepoznato",
                newTermin.UslugaID,
                usluge.Find(u => u.Id == newTermin.UslugaID)?.Naziv ?? "Nepoznato",
                usluge.Find(u => u.Id == newTermin.UslugaID)?.Cena ?? 0
            );
            termini.Add(termin);
            return Results.CreatedAtRoute(GetTerminEndpointName, new { id = termin.Id }, termin);
        }).WithParameterValidation();
// PUT termini
        app.MapPut("termini/{id}", (int id, UpdateTerminDTO updatedTermin) => {
            int index = termini.FindIndex(t => t.Id == id);
            if (index == -1) return Results.NotFound();
            
            TerminDTO termin = new(
                id,
                DateOnly.FromDateTime(updatedTermin.Datum),
                updatedTermin.Status,
                updatedTermin.RadnikID,
                zaposleni.Find(z => z.Id == updatedTermin.RadnikID)?.Ime ?? "Nepoznato",
                updatedTermin.KlijentID,
                klijenti.Find(k => k.Id == updatedTermin.KlijentID)?.Ime ?? "Nepoznato",
                updatedTermin.UslugaID,
                usluge.Find(u => u.Id == updatedTermin.UslugaID)?.Naziv ?? "Nepoznato",
                usluge.Find(u => u.Id == updatedTermin.UslugaID)?.Cena ?? 0
            );
            
            termini[index] = termin;
            return Results.Ok(termin);
        }).WithParameterValidation();

        app.MapDelete("termini/{id}", (int id) => {
            TerminDTO? termin = termini.Find(t => t.Id == id);
            if (termin is null) return Results.NotFound();
            termini.Remove(termin);
            return Results.Ok(termin);
        }).WithParameterValidation();


//---------------------------------Izvestaji----------------------
// GET /izvestaji/dnevni/{datum}
        app.MapGet("izvestaji/dnevni/{datum}", (string datum) =>
        {
            if (!DateOnly.TryParseExact(datum, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return Results.BadRequest("Invalid date format. Use yyyy-MM-dd.");
            }

            var dnevniTermini = termini.Where(t => t.Datum == parsedDate && t.Status == "Završen").ToList();

            if (!dnevniTermini.Any())
            {
                return Results.NotFound("No completed appointments for this date.");
            }

            var izvestaj = new IzvestajDTO(
                parsedDate,
                dnevniTermini.Count,
                dnevniTermini.Select(t => t.KlijentID).Distinct().Count(),
                dnevniTermini.Sum(t => t.UslugaCena)
            );

            return Results.Ok(izvestaj);
        });

        // GET /izvestaji/mesecni/{godina}/{mesec}
        app.MapGet("izvestaji/mesecni/{godina}/{mesec}", (int godina, int mesec) =>
        {
            var mesecniTermini = termini.Where(t => t.Datum.Year == godina && t.Datum.Month == mesec && t.Status == "Završen").ToList();

            if (!mesecniTermini.Any())
            {
                return Results.NotFound("No completed appointments for this month.");
            }

            var izvestaj = new IzvestajDTO(
                new DateOnly(godina, mesec, 1),
                mesecniTermini.Count,
                mesecniTermini.Select(t => t.KlijentID).Distinct().Count(),
                mesecniTermini.Sum(t => t.UslugaCena)
            );

            return Results.Ok(izvestaj);
        });


return app;

}
}