using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UProfile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            GetUserQueries(userid);
        }

        else
        {
            lblLoginError.Text = "Please Log In or Sign Up to display this page.";
            Panel1.Visible = true;
        }
        
    }

    private void GetUserQueries(object userid)
    {
        String query = "SELECT TOP 100 Q.Id,Q.Text,Q.Hashtag,C.Name,U.Username, [dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId WHERE U.Id=@userid ORDER BY Q.RegDate DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@userid",userid);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        UserQueryList.DataSource = dr;
        UserQueryList.DataBind();
        con.Close();
    }

    protected void DeleteQuery_Click(object sender, EventArgs e)
    {
        LinkButton btnRemoveQuery = (LinkButton)sender;
        int queryid = Convert.ToInt32(btnRemoveQuery.CommandArgument.ToString());

        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "DELETE FROM [dbo].[Queries] WHERE UserId=@userid AND Id=@queryid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@queryid", queryid);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        else
        {
            lblLoginError.Text = "Your session is closed for security reason. Please Log In or Sign Up to remove this content.";
        }
    }

    
    protected string FormatUrl(int Id)
    {
        return "Answers.aspx?QueryId=" + Id;

    }
}