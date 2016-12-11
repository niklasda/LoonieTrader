using LoonieTrader.Library.ViewModels;
using LoonieTrader.OxyPlot.ViewModels;
using StructureMap;

namespace LoonieTrader.OxyPlot.Locator
{
    public class OxyPlotRegistry : Registry
    {
        public OxyPlotRegistry()
        {
            For<ChartBaseViewModel>().Use<OxyPlotPartViewModel>();
        }
    }
}