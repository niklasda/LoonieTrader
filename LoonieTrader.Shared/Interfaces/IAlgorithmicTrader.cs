using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface IAlgorithmicTrader
    {
        ISettings GetSettings();

        TradeAction Decide(IList<OhlcPoint> pricePoints, Depth depth = null);
    }
}
