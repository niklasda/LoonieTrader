﻿using LoonieTrader.RestLibrary.Locator.v3;
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