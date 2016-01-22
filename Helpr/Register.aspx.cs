using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateUser_Click(object sender, EventArgs e)
    {
        String username = Username.Text;
        String mail = Email.Text;
        String password = Password.Text;

        String query = "INSERT INTO [dbo].[Users] (Username, Email, Password) values('" + username + "' , '" + mail + "' , '" + password + "')";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Server.Transfer("Default.aspx", true);
    }
}