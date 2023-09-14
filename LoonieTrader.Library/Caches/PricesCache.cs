using System.Collections.Generic;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.Caches;

public class PricesCache : IPricesCache
{
    private Dictionary<string, List<PricesResponse.Price>> Prices { get; } = new();

    public Dictionary<string, List<PricesResponse.Price>> Cache => Prices;

    public void CacheIt(PricesResponse.Price pr)
    {
        if (Prices.ContainsKey(pr.instrument))
        {
            Prices[pr.instrument].Add(pr);
        }
        else
        {
            Prices.Add(pr.instrument, new List<PricesResponse.Price>());

            Prices[pr.instrument].Add(pr);
        }
    }
}

public interface IPricesCache
{
    Dictionary<string, List<PricesResponse.Price>> Cache { get; }

    void CacheIt(PricesResponse.Price pr);
}