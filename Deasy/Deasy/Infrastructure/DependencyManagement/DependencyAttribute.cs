﻿using System;

namespace Deasy.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Markes a service that is registered in automatically registered in Aaron's inversion of control container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute(LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            LifeStyle = lifeStyle;
        }

        public DependencyAttribute(Type serviceType, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            LifeStyle = lifeStyle;
            ServiceType = serviceType;
        }

        /// <summary>The type of service the attributed class represents.</summary>
        public Type ServiceType { get; protected set; }

        public LifeStyle LifeStyle { get; protected set; }

        /// <summary>Optional key to associate with the service.</summary>
        public string Key { get; set; }

        /// <summary>Configurations for which this service is registered.</summary>
        public string Configuration { get; set; }

        public virtual void RegisterService(AttributeInfo<DependencyAttribute> attributeInfo, ContainerManager container)
        {
            Type serviceType = attributeInfo.Attribute.ServiceType ?? attributeInfo.DecoratedType;
            container.AddComponent(serviceType, attributeInfo.DecoratedType,
                                       attributeInfo.Attribute.Key ?? attributeInfo.DecoratedType.FullName, attributeInfo.Attribute.LifeStyle);
        }
    }
}
