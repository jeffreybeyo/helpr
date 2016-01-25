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
        String username = Username.Text.ToLower().Trim();
        String mail = Email.Text;
        String password = Password.Text;
           
        //check username availability
        //String checkusername = "SELECT * FROM [dbo].[Users] AS U WHERE U.Username=@Username";
        //SqlCommand cmd_check = new SqlCommand(checkusername, con);
        //cmd_check.Parameters.AddWithValue("@Username", username);
        //con.Open();
        //SqlDataReader dr = cmd_check.ExecuteReader();
        
        //if(dr.HasRows)
        //{
            
        //    lblUsername_Error.Text = "The username " + username + " is not available.";
        //    PanelUsernameError.Visible = true;
           
        //}
        //con.Close();

        //else //if username available, insert user into database
        //{
        String query = "INSERT INTO [dbo].[Users] (Username, Email, Password) values('" + username + "' , '" + mail + "' , '" + password + "')";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
         
    }
}