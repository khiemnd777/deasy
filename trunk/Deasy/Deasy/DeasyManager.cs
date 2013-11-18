using System.Web;
using System.Web.Mvc;
using Deasy;
using Deasy.Infrastructure;
using Deasy.Infrastructure.DependencyManagement;

[assembly: PreApplicationStartMethod(typeof(DeasyManager), "Initialize")]
namespace Deasy
{
    public class DeasyManager
    {
        public static void Initialize()
        {
            AppManager.Initialize(false);

            var dependencyResolver = new DeasyResolver();
            DependencyResolver.SetResolver(dependencyResolver);   
        }
    }
}
