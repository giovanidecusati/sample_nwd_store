using System;
using System.Collections.Generic;

namespace BuildingBlock.Core.DI
{
    public interface IContainer
    {
        IEnumerable<object> GetServices(Type serviceType);
    }
}
