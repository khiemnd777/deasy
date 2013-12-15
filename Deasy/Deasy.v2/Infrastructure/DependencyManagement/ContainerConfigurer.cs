using System;
using System.Collections.Generic;
using System.Linq;

namespace Deasy.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Configures the inversion of control container with services used by Aaron.
    /// </summary>
    public class ContainerConfigurer
    {
        /// <summary>
        /// Known configuration keys used to configure services.
        /// </summary>
        public static class ConfigurationKeys
        {
            /// <summary>Key used to configure services intended for medium trust.</summary>
            public const string MediumTrust = "MediumTrust";
            /// <summary>Key used to configure services intended for full trust.</summary>
            public const string FullTrust = "FullTrust";
        }

        public virtual void Configure(ICore core, ContainerManager containerManager, EventBroker broker/*, AaronConfig configuration*/)
        {
            //other dependencies
            //containerManager.AddComponentInstance<AaronConfig>(configuration, "aaron.configuration");
            containerManager.AddComponentInstance<ICore>(core, "aaron.core");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "aaron.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("aaron.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });

            //event broker
            containerManager.AddComponentInstance(broker);

            containerManager.AddComponent<DeasyAttributeRegistrator>();
            var deasyAttributeRegistrator = containerManager.Resolve<DeasyAttributeRegistrator>();
            var deasyAttributes = deasyAttributeRegistrator.FindDeasyAttributes();
            deasyAttributeRegistrator.RegisterDeasy(deasyAttributes);
        }

        protected virtual string[] GetComponentConfigurations(/*AaronConfig configuration*/)
        {
            var configurations = new List<string>();
            string trustConfiguration = (CommonHelper.GetTrustLevel() > System.Web.AspNetHostingPermissionLevel.Medium)
                ? ConfigurationKeys.FullTrust
                : ConfigurationKeys.MediumTrust;
            configurations.Add(trustConfiguration);
            return configurations.ToArray();
        }
    }
}
