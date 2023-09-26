using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.Caches;

public class PricesCache : IPricesCache//, IPricesCache1
{
    private Dictionary<string, ObservableCollection<PricesResponse.Price>> Prices { get; } = new();

    public Dictionary<string, ObservableCollection<PricesResponse.Price>> Cache => Prices;

    public event EventHandler<PriceEventArgs> NewPrice ;//{ add; remove; }

    public void CacheIt(PricesResponse.Price pr)
    {
        if (Prices.ContainsKey(pr.instrument))
        {
            Prices[pr.instrument].Add(pr);
        }
        else
        {
            var newList = new ObservableCollection<PricesResponse.Price>();
            newList.CollectionChanged += OnCollectionChange;



            Prices.Add(pr.instrument, newList);

            Prices[pr.instrument].Add(pr);
        }
    }

    void OnCollectionChange(object sender, NotifyCollectionChangedEventArgs args)
    {
        if (args.Action == NotifyCollectionChangedAction.Add)
        {

            foreach (var item in args.NewItems.Cast<PricesResponse.Price>())
            {
                NewPrice?.Invoke(this, new PriceEventArgs() { date=item.time, ask = item.asks.First().price });
                Debug.WriteLine(item.ToString());

            }

        }
    }

}

public interface IPricesCache
{
    Dictionary<string, ObservableCollection<PricesResponse.Price>> Cache { get; }

    event EventHandler<PriceEventArgs> NewPrice;

    void CacheIt(PricesResponse.Price pr);
}

public class PriceEventArgs : EventArgs
{
    public string date;
    public string ask;
}