using System.Collections.Generic;
using System.ComponentModel.Composition;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;

namespace DemoLeadingIndicator
{
    [Export(typeof(ILeadingIndicator))]
    public class DemoLeadingIndicator : ILeadingIndicator
    {
        public string Version { get { return "0.9"; } }

        public string Name { get { return "Leading Demo"; } }

        public string Description { get { return "Description of leading demo"; } }

        public string Title { get { return string.Format("{0} v{1}", Name, Version); } }

        public IRequirements GetRequirements()
        {
            return new Requirements() {MinPoints = 1, MaxPoints = 1};
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
