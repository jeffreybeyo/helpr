using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OProfile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        int userid = -1;

        if (Request.Params["UserId"] != null && int.TryParse(Request.Params["UserId"], out userid))
        {
            GetUserProfile(userid);
            GetUserQueries(userid);
            GetUserAnswers(userid);
            GetUserCounts(userid);
        }

        else
        {
            lblError.Text = "Aradığınız kullanıcı hesabını silmiş olabilir... ";
            PanelError.Visible = true;
        }
    }

    private void GetUserProfile(int userid)
    {
       
            String query = "SELECT U.Username FROM [dbo].[Users] AS U WHERE U.Id=@userid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            lblUsername.Text = cmd.ExecuteScalar().ToString();
            Page.Title = lblUsername.Text;
            con.Close();
    }

        private void GetUserCounts(int userid)
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

        else
        {
            lblemptyquery.Visible=true;
        }
        
        con.Close();
    }

    private void GetUserAnswers(object userid)
    {
        String query = "SELECT A.Text AS AText, Q.Text AS QText, A.UserId, U.Username, Q.Id FROM [dbo].[Answers] AS A INNER JOIN [dbo].[Users] U ON U.Id=A.UserId INNER JOIN [dbo].[Queries] Q ON Q.Id=A.QueryId WHERE A.UserId=@userid ORDER BY A.RegDate DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@userid", userid);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            UserAnswerList.DataSource = dr;
            UserAnswerList.DataBind();
        }

        else
        {
            lblemptyanswer.Visible = true;
        }
        con.Close();
    }

    
    protected string FormatUrl(int Id)
    {
        return "Answers.aspx?QueryId=" + Id;

    }
}