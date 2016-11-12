using System.Collections.Generic;
using System.ComponentModel.Composition;
using LoonieTrader.Shared.Interfaces;
using LoonieTrader.Shared.Models;

namespace DemoAlgo
{
    [Export(typeof(IAlgorithmicTrader))]
    public class DemoAlgoTrader : IAlgorithmicTrader
    {
        public string Version { get { return "0.9"; } }

        public string Name { get { return "Algo Demo"; } }

        public string Description { get { return "Description of algo demo"; } }

        public string Title { get { return string.Format("{0} v{1}", Name, Version); } }

        private ISpecification _specification;

        public IRequirements GetRequirements()
        {
            return new Requirements { MinPoints = 1, MaxPoints = 1 };
        }

        public void SetSpecification(ISpecification specification)
        {
            _specification = specification;
        }

        public TradeAction Decide(IList<OhlciPoint> pricePoints, Depth depth = null)
        {
            return TradeAction.None;
        }
    }

}
