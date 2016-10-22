using System.Collections.Generic;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;

namespace DemoLeadingIndicator
{
    public class DemoLeadingIndicator : ILeadingIndicator
    {
        public ISettings GetSettings()
        {
            return new Settings();
        }

        public IList<PricePoint> CalculatePoints(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return new List<PricePoint>(new[] {PricePoint.Empty});
        }

        private PricePoint CalculatePoint(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return PricePoint.Empty;
        }
    }
}
