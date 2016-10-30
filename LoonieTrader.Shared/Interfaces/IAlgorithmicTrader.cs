using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface IAlgorithmicTrader : ILoadable
    {
        string Name { get; }
        string Version { get; }
        string Description { get; }

        IRequirements GetRequirements();
        ISpecification SetSpecification();
        TradeAction Decide(IList<OhlciPoint> pricePoints, Depth depth = null);
    }
}
