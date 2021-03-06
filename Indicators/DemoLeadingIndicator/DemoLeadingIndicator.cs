﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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

        private ISpecification _specification;

        public IRequirements GetRequirements()
        {
            return new Requirements { MinPoints = 1, MaxPoints = 10 };
        }

        public void SetSpecification(ISpecification specification)
        {
            _specification = specification;
        }

        public IList<PricePoint> CalculatePoints(IList<OhlciPoint> pricePoints, Depth depth = null)
        {
            return new List<PricePoint>(Enumerable.Repeat(PricePoint.Empty, _specification.NumberOfPoints));
        }

        private PricePoint CalculatePoint(IList<OhlciPoint> pricePoints, Depth depth = null)
        {
            return PricePoint.Empty;
        }
    }
}
