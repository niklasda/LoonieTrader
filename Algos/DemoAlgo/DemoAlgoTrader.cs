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

        public IRequirements GetRequirements()
        {
            return new Requirements() { MinPoints = 1, MaxPoints = 1 };
        }

        public ISpecification SetSpecification()
        {
            return new Specification();
        }

        public TradeAction Decide(IList<OhlcPoint> pricePoints, Depth depth = null)
        {
            return TradeAction.None;
        }
    }

}
