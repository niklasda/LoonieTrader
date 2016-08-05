using LoonieTrader.RestLibrary.Locator;
using StructureMap;

namespace LoonieTrader.TestApp.Locator
{
    public static class ServiceLocator
    {
        public static IContainer Initialize()
        {
            var container = new Container(c =>
            {
                c.AddRegistry<ServiceRegistry>();
            });

            return container;
        }
    }
}