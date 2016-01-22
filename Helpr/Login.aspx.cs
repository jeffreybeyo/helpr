using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "~/Register";

        
    }

    protected void LogIn(object sender, EventArgs e)
    {
        String usernamestr = Username.Text;
        String passwordstr = Password.Text;

        String query = "SELECT * FROM [dbo].[Users] AS U WHERE U.Username=@Username AND U.Password=@Password";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@Username", usernamestr);
        cmd.Parameters.AddWithValue("@Password", passwordstr);

        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if(dr.Read())
        {
            Session.Add("UserId", dr["Id"]);
            Session.Add("Username", dr["Username"].ToString());
            con.Close();
            Response.Redirect("Default.aspx", true);
        }
        else
        {
            Panel1.Visible = true;
            lblLoginError.Text = "Username or password is not correct";
        }
        
    }
}