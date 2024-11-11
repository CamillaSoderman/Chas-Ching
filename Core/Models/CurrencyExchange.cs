using Chas_Ching.Core.Enums;

internal class CurrencyExchange
{
    private static readonly Dictionary<(CurrencyType from, CurrencyType to), decimal> ExchangeRates = new Dictionary<(CurrencyType, CurrencyType), decimal>
        {
            { (CurrencyType.SEK, CurrencyType.EUR), 0.086m},
            { (CurrencyType.SEK, CurrencyType.USD), 0.092m},

            { (CurrencyType.EUR, CurrencyType.SEK), 11.65m},
            { (CurrencyType.EUR, CurrencyType.USD), 1.07m},

            { (CurrencyType.USD, CurrencyType.SEK), 10.85m},
            { (CurrencyType.USD, CurrencyType.EUR), 0.93m},
        };

    public static decimal Convert(decimal amount, CurrencyType fromCurrency, CurrencyType toCurrency)
    {
        if (fromCurrency == toCurrency)
        {
            return amount;
        }
        if (ExchangeRates.TryGetValue((fromCurrency, toCurrency), out decimal rate))
        {
            return amount * rate;
        }
        else
        {
            throw new Exception($"Exchange rate from {fromCurrency} to {toCurrency} not defined");
        }
    }
}