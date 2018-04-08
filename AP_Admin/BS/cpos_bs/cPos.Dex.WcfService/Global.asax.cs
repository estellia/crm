using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;
using cPos.Dex.Common;

namespace cPos.Dex.WcfService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes); 
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                Utils.SaveFile(Common.Config.LogFolder() + "app", Utils.GetNowString() + ".log", ex.ToString());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("TestService", new WebServiceHostFactory(), typeof(TestService)));
            routes.Add(new ServiceRoute("BasicService", new WebServiceHostFactory(), typeof(BasicService)));
            routes.Add(new ServiceRoute("AuthService", new WebServiceHostFactory(), typeof(AuthService)));
            routes.Add(new ServiceRoute("LogService", new WebServiceHostFactory(), typeof(BizLogService)));
            routes.Add(new ServiceRoute("OrderService", new WebServiceHostFactory(), typeof(OrderService)));
            routes.Add(new ServiceRoute("ComService", new WebServiceHostFactory(), typeof(ComService)));
        }
    }
}