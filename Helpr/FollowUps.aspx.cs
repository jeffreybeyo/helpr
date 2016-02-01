using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FollowUps : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {
        GetFollowUps();

    }

    private void GetFollowUps()
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "SELECT FU.UserId, FU.QueryId, Q.Text, U.Username AS QSender, C.Name AS CName, Q.RegDate FROM [dbo].[FollowUp] AS FU INNER JOIN [dbo].[Queries] Q ON Q.Id=FU.QueryId INNER JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId INNER JOIN [dbo].[Users] U ON U.Id=Q.UserId WHERE FU.UserId=@userid ORDER BY FU.RegDate DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            FollowList.DataSource = dr;
            FollowList.DataBind();

            con.Close();
        }

        else
        {
            lblLoginError.Text = "Bu sayfayı görüntülemek için Giriş Yap veya Kayıt Ol.";
            Panel1.Visible = true;
        }
    }
    protected string FormatUrl(int Id)
    {
        return "Answers.aspx?QueryId=" + Id;

    }
}