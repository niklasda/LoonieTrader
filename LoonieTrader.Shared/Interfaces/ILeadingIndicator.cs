using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface ILeadingIndicator
    {
        string Name { get; }
        string Version { get; }
        string Description { get; }

        IRequirements GetRequirements();
        ISpecification SetSpecification();
        IList<PricePoint> CalculatePoints(IList<OhlcPoint> pricePoints, Depth depth = null);
    }
}
