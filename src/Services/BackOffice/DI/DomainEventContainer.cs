using BuildingBlock.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BackOffice.DI
{
    public class DomainEventContainer : IContainer
    {

        private readonly IServiceProvider _serviceProvider;

        public DomainEventContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}
