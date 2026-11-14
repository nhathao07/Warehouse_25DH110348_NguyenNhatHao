using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Warehouse_25DH110348
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
