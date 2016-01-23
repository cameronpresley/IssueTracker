using Microsoft.Practices.Unity;
using System.Web.Http;
using IssueTracker.DataAccess;
using Unity.WebApi;

namespace IssueTracker
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterInstance<UserLayer.IUserDataAccess>(new UserLayer.UserDataAccess());
            //container.RegisterInstance<IUserRepository>(new UserRepository(container.Resolve<UserDatabaseAccess>()));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}