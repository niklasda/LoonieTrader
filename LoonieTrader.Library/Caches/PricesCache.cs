using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.Caches;

public class PricesCache : IPricesCache
{
    private Dictionary<string, ObservableCollection<PricesResponse.Price>> Prices { get; } = new();

    public Dictionary<string, ObservableCollection<PricesResponse.Price>> Cache => Prices;

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

            foreach (var item in args.NewItems.Cast<PricesResponse.Price> ())
            {
                Debug.WriteLine(item.ToString());

            }

        }
    }

}

public interface IPricesCache
{
    Dictionary<string, ObservableCollection<PricesResponse.Price>> Cache { get; }

    void CacheIt(PricesResponse.Price pr);
}
