using System.Collections.Generic;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;

namespace DemoAlgo
{
    public class DemoAlgoTrader : IAlgorithmicTrader
    {
        public ISettings GetSettings()
        {
            return new Settings();
        }

        public TradeAction Decide(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return TradeAction.None;
        }
    }

}
