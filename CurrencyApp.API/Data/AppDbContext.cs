using CurrencyApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CurrencyApp.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();
}