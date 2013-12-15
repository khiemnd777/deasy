using System.Linq;
using System.Collections.Generic;
using Deasy.Infrastructure;
using Deasy.Infrastructure.DependencyManagement;

namespace Deasy
{
    public class DeasyAttributeRegistrator
    {
        private readonly ITypeFinder _typeFinder;
        private readonly ICore _core;

        public DeasyAttributeRegistrator(ITypeFinder typeFinder, ICore core)
        {
            _typeFinder = typeFinder;
            _core = core;
        }

        public IEnumerable<AttributeInfo<DeasyAttribute>> FindDeasyAttributes()
        {
            var assemblies = _typeFinder.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var attributes = type.GetCustomAttributes(typeof(DeasyAttribute), false);
                    foreach (var attribute in attributes)
                    {
                        yield return new AttributeInfo<DeasyAttribute> { Attribute = (DeasyAttribute)attribute, DecoratedType = type };
                    }
                }
            }
        }

        public void RegisterDeasy(IEnumerable<AttributeInfo<DeasyAttribute>> deasyList)
        {
            deasyList.ForEach(d => 
            {
                Register(d);
            });
        }

        private void Register(AttributeInfo<DeasyAttribute> info)
        {
            var type = info.DecoratedType;
            if (type.IsNull()) return;
            _typeFinder.FindClassesOfType(type)
                .Where(t => !t.IsInterface)
                .ForEach(c =>
                {
                    _core.ContainerManager.AddComponent(type, c, info.Attribute.Key, info.Attribute.LifeStyle);
                }); 
            
        }
    }
}
