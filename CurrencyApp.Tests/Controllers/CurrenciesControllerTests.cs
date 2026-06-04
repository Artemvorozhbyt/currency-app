using CurrencyApp.API.Controllers;
using CurrencyApp.API.Data;
using CurrencyApp.API.Models;
using CurrencyApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CurrencyApp.Tests.Controllers;

public class CurrenciesControllerTests
{
    [Fact]
    public async Task GetByYear_ShouldReturnCurrenciesForGivenYear()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new AppDbContext(options);

        context.CurrencyRates.Add(new CurrencyRate
        {
            Code = "USD",
            Currency = "Dollar",
            Rate = 4.0m,
            Date = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc)
        });

        context.CurrencyRates.Add(new CurrencyRate
        {
            Code = "EUR",
            Currency = "Euro",
            Rate = 4.3m,
            Date = new DateTime(2025, 6, 3, 0, 0, 0, DateTimeKind.Utc)
        });

        await context.SaveChangesAsync();

        var nbpMock = new Mock<NbpService>(new HttpClient());

        var controller =
            new CurrenciesController(
                nbpMock.Object,
                context);

        // Act
        var result = await controller.GetByYear(2026);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var rates =
            Assert.IsAssignableFrom<IEnumerable<CurrencyRate>>
            (okResult.Value);

        Assert.Single(rates);
    }
}