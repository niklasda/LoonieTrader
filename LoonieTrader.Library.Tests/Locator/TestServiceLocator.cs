using LoonieTrader.Library.Locator;
using StructureMap;

namespace LoonieTrader.Library.Tests.Locator
{
    public static class TestServiceLocator
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