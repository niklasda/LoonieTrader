using LoonieTrader.Library.ViewModels;
using LoonieTrader.LiveCharts.ViewModels;
using StructureMap;

namespace LoonieTrader.LiveCharts.Locator
{
    public class LiveChartsRegistry : Registry
    {
        public LiveChartsRegistry()
        {
            For<ChartBaseViewModel>().Use<LiveChartsPartViewModel>();
        }
    }
}