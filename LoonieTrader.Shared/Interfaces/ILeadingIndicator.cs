using System.Collections.Generic;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.Shared.Interfaces
{
    public interface ILeadingIndicator
    {
        ISettings GetSettings();

        IList<PricePoint> CalculatePoints(IList<OhlcPoint> pricePoints, Depth depth = null);
    }
}
