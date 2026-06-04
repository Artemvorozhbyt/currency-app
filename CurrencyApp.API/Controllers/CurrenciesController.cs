using CurrencyApp.API.Data;
using CurrencyApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using CurrencyApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.API.Controllers;

[ApiController]
[Route("currencies")]
public class CurrenciesController : ControllerBase
{
    private readonly NbpService _nbpService;
    private readonly AppDbContext _context;

    public CurrenciesController(
        NbpService nbpService,
        AppDbContext context)
    {
        _nbpService = nbpService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencies()
    {
        var data = await _nbpService.GetRatesAsync();

        return Ok(data);
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> FetchCurrencies()
    {
        var data = await _nbpService.GetRatesAsync();

        if (data == null || !data.Any())
            return BadRequest("No data received from NBP");

        var table = data.First();

        foreach (var rate in table.Rates)
        {
            var date = DateTime.SpecifyKind(
                table.EffectiveDate,
                DateTimeKind.Utc);

            var exists = await _context.CurrencyRates
                .AnyAsync(x =>
                    x.Code == rate.Code &&
                    x.Date == date);

            if (exists)
                continue;

            var currencyRate = new CurrencyRate
            {
                Code = rate.Code,
                Currency = rate.Currency,
                Rate = rate.Mid,
                Date = date
            };

            _context.CurrencyRates.Add(currencyRate);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Currencies saved successfully",
            Count = table.Rates.Count
        });
    }
    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate(DateTime date)
    {
        var start = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var end = start.AddDays(1);

        var rates = await _context.CurrencyRates
            .Where(x => x.Date >= start && x.Date < end)
            .ToListAsync();

        return Ok(rates);
    }
    [HttpGet("year/{year}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        var rates = await _context.CurrencyRates
            .Where(x => x.Date.Year == year)
            .ToListAsync();

        return Ok(rates);
    }
    [HttpGet("month/{year}/{month}")]
    public async Task<IActionResult> GetByMonth(int year, int month)
    {
        var rates = await _context.CurrencyRates
            .Where(x => x.Date.Year == year &&
                        x.Date.Month == month)
            .ToListAsync();

        return Ok(rates);
    }
    [HttpGet("quarter/{year}/{quarter}")]
    public async Task<IActionResult> GetByQuarter(int year, int quarter)
    {
        int startMonth = (quarter - 1) * 3 + 1;
        int endMonth = startMonth + 2;

        var rates = await _context.CurrencyRates
            .Where(x => x.Date.Year == year &&
                        x.Date.Month >= startMonth &&
                        x.Date.Month <= endMonth)
            .ToListAsync();

        return Ok(rates);
    }
}