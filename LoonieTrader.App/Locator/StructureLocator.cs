using System;
using System.Collections.Generic;
using CommonServiceLocator;
using StructureMap;

namespace LoonieTrader.App.Locator
{
    public class StructureLocator : IServiceLocator
    {
        public StructureLocator(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;

        public TService GetInstance<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.GetInstance<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances<TService>();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type serviceType, string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}