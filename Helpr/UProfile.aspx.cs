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
            GetUserProfile();
            GetUserQueries(userid);
            GetUserAnswers(userid);
            GetUserCounts();
        }

        else
        {
            lblLoginError.Text = "Please Log In or Sign Up to display this page.";
            Panel1.Visible = true;
        }
        
    }

    private void GetUserProfile()
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "SELECT U.Username FROM [dbo].[Users] AS U WHERE U.Id=@userid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            lblUsername.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
    }

        private void GetUserCounts()
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            //check user query count
            String query = "SELECT COUNT(Q.Id) AS UQueryCount FROM [dbo].[Queries] AS Q WHERE Q.UserId=@userid";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            UQueryCount.Text = cmd.ExecuteScalar().ToString();
            con.Close();

            //check user answer count
            String query2 = "SELECT COUNT(A.Id) AS UAnswerCount FROM [dbo].[Answers] AS A WHERE A.UserId=@userid";
            con.Open();
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@userid", userid);
            UAnswerCount.Text = cmd2.ExecuteScalar().ToString();
            con.Close();
        }
    }



    private void GetUserQueries(object userid)
    {
        String query = "SELECT TOP 100 Q.Id,Q.Text,Q.Hashtag,C.Name,U.Username, [dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId WHERE U.Id=@userid ORDER BY Q.RegDate DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@userid",userid);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.HasRows)
        {
            UserQueryList.DataSource = dr;
            UserQueryList.DataBind();
        }
        //else
        //{
        //    lblempty.Text = "You hav";
        //}
        
        con.Close();
    }

    private void GetUserAnswers(object userid)
    {
        String query = "SELECT A.Text AS AText, Q.Text AS QText, A.UserId, U.Username, Q.Id FROM [dbo].[Answers] AS A INNER JOIN [dbo].[Users] U ON U.Id=A.UserId INNER JOIN [dbo].[Queries] Q ON Q.Id=A.QueryId WHERE A.UserId=@userid ORDER BY A.RegDate DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@userid", userid);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        UserAnswerList.DataSource = dr;
        UserAnswerList.DataBind();
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