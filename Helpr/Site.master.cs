using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            alogin.Visible = false;
            aregister.Visible = false;
            LoginName.Text = Session["Username"].ToString();
            ausername.Visible = true;
            alogout.Visible = true;
            afollowups.Visible = true;
            settings.Visible = true;

            //check followup count
            String query = "SELECT COUNT(FU.Id) AS FUCount, U.Id FROM [dbo].[FollowUp] AS FU RIGHT JOIN [dbo].[Users] U ON U.Id=FU.UserId WHERE U.Id=@userid GROUP BY U.Id";
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            FUCountlbl.Text = cmd.ExecuteScalar().ToString();

            con.Close();
        }

        else
        {
            alogin.Visible = true;
            aregister.Visible = true;
            ausername.Visible = false;
            alogout.Visible = false;
            afollowups.Visible = false;
            settings.Visible = false;
        }
    }

    protected void logout_click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx", true);
    }

    protected void search_click(object sender, EventArgs e)
    {
        String query = Searchboxtxt.Text;
        if (!string.IsNullOrEmpty(query))
        {
            Response.Redirect("Search.aspx?SearchString=" + query, true);
        }
        else
        {
            Label1.Text = "Arama boş olamaz.";
        }
    }


    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        
    }
}