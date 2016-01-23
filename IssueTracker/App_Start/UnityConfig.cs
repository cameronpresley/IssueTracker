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
            
            container.RegisterInstance<UserLayer.IUserDataAccess>(new UserLayer.UserDataAccess());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}