using Oanda.RestLibrary.Locator;
using StructureMap;

namespace Oanda.TestApp.Locator
{
    public static class ServiceLocator
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