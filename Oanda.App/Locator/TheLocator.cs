using Oanda.RestLibrary.Locator;
using StructureMap;

namespace Oanda.App.Locator
{
    public static class TheLocator
    {
        public static void Initialize()
        {
            var container = new Container(c =>
            {
                c.AddRegistry<ServiceRegistry>();    
            });
        }
    }
}