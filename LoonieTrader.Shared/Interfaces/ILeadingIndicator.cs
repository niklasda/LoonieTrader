using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface ILeadingIndicator : ILoadable
    {
        string Name { get; }
        string Version { get; }
        string Description { get; }

        IRequirements GetRequirements();
        ISpecification SetSpecification();
        IList<PricePoint> CalculatePoints(IList<OhlciPoint> pricePoints, Depth depth = null);
    }
}
