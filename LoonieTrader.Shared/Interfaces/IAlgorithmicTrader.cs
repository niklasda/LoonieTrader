using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface IAlgorithmicTrader
    {
        string Name { get; }
        string Version { get; }
        string Description { get; }

        IRequirements GetRequirements();
        ISpecification SetSpecification();
        TradeAction Decide(IList<OhlcPoint> pricePoints, Depth depth = null);
    }
}
