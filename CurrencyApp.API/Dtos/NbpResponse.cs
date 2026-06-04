namespace CurrencyApp.API.Dtos;

public class NbpResponse
{
    public string Table { get; set; } = string.Empty;

    public string No { get; set; } = string.Empty;

    public DateTime EffectiveDate { get; set; }

    public List<NbpRate> Rates { get; set; } = [];
}

public class NbpRate
{
    public string Currency { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public decimal Mid { get; set; }
}