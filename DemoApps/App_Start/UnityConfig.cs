using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using DemoApps.Repository;

namespace DemoApps
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            // container.RegisterType<IEventMasterRepository, EventMasterRepository>();
            container.RegisterType<IEventMasterRepository, EventMasterRepository>();
            container.RegisterType<IGuestRepository, GuestRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}