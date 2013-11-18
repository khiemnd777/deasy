using System;
using System.Collections.Generic;
using System.Linq;
using Deasy.Infrastructure.DependencyManagement;
using Autofac;

namespace Deasy.Infrastructure
{
    public class Core : ICore
    {
        private ContainerManager _containerManager;

        public Core() 
            : this(new ContainerConfigurer(), EventBroker.Instance)
		{
		}

		public Core(ContainerConfigurer configurer, EventBroker broker)
		{
            InitializeContainer(configurer, broker);
		}

        private void InitializeContainer(ContainerConfigurer configurer, EventBroker broker)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build(), builder);
            configurer.Configure(this, _containerManager, broker);
        }

        public void Initialize()
        {
            
        }

        public T Resolve<T>() where T:class
        {
            return ContainerManager.Resolve<T>();
        }

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
