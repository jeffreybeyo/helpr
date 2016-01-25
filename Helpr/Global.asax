<%@ Application Language="C#" %>
<%@ Import Namespace="Helpr" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

     void Session_Start(object sender, EventArgs e)
 {
  // Code that runs when a new session is started
 Session.Timeout = 15;
 }
</script>
