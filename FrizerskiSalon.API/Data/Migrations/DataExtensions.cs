using System;
using FrizerskiSalon.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FrizerskiSalon.API.Data.Migrations;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FrizerskiSalonContext>();
        dbContext.Database.Migrate();
    }

    
    


}
