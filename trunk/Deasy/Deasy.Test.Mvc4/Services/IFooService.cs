using Deasy.Infrastructure.DependencyManagement;

namespace Deasy.Test.Mvc4.Services
{
    [Deasy(lifeStyle : ComponentLifeStyle.LifetimeScope)]
    public interface IFooService
    {
        string Foo();
    }
}