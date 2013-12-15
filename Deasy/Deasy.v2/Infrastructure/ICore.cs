using System;
using Deasy.Infrastructure.DependencyManagement;

namespace Deasy.Infrastructure
{
    public interface ICore
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}