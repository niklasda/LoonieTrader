using LoonieTrader.Library.Locator;
using StructureMap;

namespace LoonieTrader.TestApp.Locator
{
    public static class ServiceLocator
    {
        public static IContainer Initialize()
        {
            var container = new Container(c =>
            {
                c.AddRegistry<LibraryRegistry>();
            });

            return container;
        }
    }
}