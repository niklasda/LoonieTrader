using System.Collections.Generic;
using System.ComponentModel.Composition;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;

namespace DemoLaggingIndicator
{
    [Export(typeof(ILaggingIndicator))]
    public class DemoLaggingIndicator : ILaggingIndicator
    {
        public string Version { get { return "0.9"; } }

        public string Name { get { return "Lagging Demo"; } }

        public string Description { get { return "Description of lagging demo"; } }

        public IRequirements GetRequirements()
        {
            return new Requirements() { MinPoints = 1, MaxPoints = 1 };
        }

        public ISpecification SetSpecification()
        {
            return new Specification();
        }

        public IList<PricePoint> CalculatePoints(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return new List<PricePoint>(new[] { PricePoint.Empty });
        }

        private PricePoint CalculatePoint(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return PricePoint.Empty;
        }
    }
}
