using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Deasy.Infrastructure;
using Deasy.Infrastructure.DependencyManagement;

namespace Deasy
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            var assemblies = typeFinder.GetAssemblies().ToArray();
            // controllers
            builder.RegisterControllers(assemblies);
            // deasy
            builder.RegisterSource(new DeasySource());
        }

        public int Order
        {
            get { return 1; }
        }

        public class DeasySource : IRegistrationSource
        {
            private static ITypeFinder TypeFinder(IComponentContext c)
            {
                return Singleton<ITypeFinder>.Instance ?? 
                    (Singleton<ITypeFinder>.Instance = c.Resolve<ITypeFinder>());
            }

            private static readonly MethodInfo method = typeof(DeasySource)
                .GetMethod("RegistrySource",
                BindingFlags.NonPublic |
                BindingFlags.Static);

            private static IComponentRegistration RegistrySource<TService>() where TService : IDeasy
            {
                Type t = null;
                return RegistrationBuilder.ForDelegate((c, p) =>
                {
                    // Type Finder
                    var typeFinder = TypeFinder(c);
                    if (typeFinder.FindClassesOfType<TService>().First() != null)
                    {
                        t = typeFinder.FindClassesOfType<TService>().First();
                    }
                    var _pobj = t.GetConstructors()
                        .SelectMany(x => x.GetParameters())
                        .Select(x =>
                        {
                            return c.Resolve(x.ParameterType);
                        });

                    return (TService)Activator.CreateInstance(t, _pobj.ToArray());
                })
                .As<TService>()
                .InstancePerHttpRequest()
                .CreateRegistration();
            }

            public bool IsAdapterForIndividualComponents
            {
                get { return false; }
            }

            public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
            {
                var ts = service as TypedService;
                if (ts != null && typeof(IDeasy).IsAssignableFrom(ts.ServiceType) && ts.ServiceType.IsInterface)
                {
                    var builder = method.MakeGenericMethod(ts.ServiceType);
                    yield return (IComponentRegistration)builder.Invoke(null, null);
                }
            }
        }
    }
}
