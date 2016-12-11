using LoonieTrader.Library.ViewModels;
using LoonieTrader.SciChart.ViewModels;
using StructureMap;

namespace LoonieTrader.SciChart.Locator
{
    public class SciChartRegistry : Registry
    {
        public SciChartRegistry()
        {
            For<ChartBaseViewModel>().Use<SciChartPartViewModel>();
        }
    }
}