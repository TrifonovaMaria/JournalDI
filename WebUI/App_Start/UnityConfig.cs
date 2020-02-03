using DomainModel;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IRepository, DataAccessXML.DataXML>(new InjectionConstructor("F:\\journal.xml"));
            IRepository rp = container.Resolve<IRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

           /* var container = new UnityContainer();
            var s = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            s.Configure(container);
            IRepository rp = container.Resolve<IRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));*/

        }
    }
}