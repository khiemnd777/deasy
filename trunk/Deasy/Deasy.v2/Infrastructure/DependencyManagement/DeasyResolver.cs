using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Deasy.Infrastructure.DependencyManagement
{
    public class DeasyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return IoC.Container.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)IoC.Resolve(type);
        }
    }
}
