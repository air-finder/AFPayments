namespace Domain.Entities.Dtos;

public class PriceDto(Guid id, long unitAmount, string currency, string platform)
{
    public Guid Id { get; set; } = id;
    public long UnitAmount { get; set; } = unitAmount;
    public string Currency { get; set; } = currency;
    public string Platform { get; set; } = platform;

    public static implicit operator PriceDto(Price price) => new PriceDto(price.Id, price.UnitAmount, price.Currency, price.Platform);
}