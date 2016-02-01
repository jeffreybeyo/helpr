using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
public partial class Search : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        String SearchString;

        if (Request.QueryString["SearchString"] != null)
        { 
            SearchString = Request.QueryString["SearchString"];
            SearchStringlbl.Text = SearchString;

            //hashtag search
            if(SearchString.Substring(0,1).Equals("-"))
            {
                GetHashtagQueries(SearchString);
            }

            //user search
            if(SearchString.Substring(0,1).Equals("@"))
            {
                GetUsersQueries(SearchString);
            }

            if (SearchString.Substring(0, 1).Equals("*"))
            {
                GetCategoryQueries(SearchString);
            }
            
            //ordinary query search
            else
            {
                GetQueries(SearchString);
            }

        }
    }

    protected void GetQueries(String SearchString)
    {
        String query = "SELECT TOP 100 Q.Id, Q.UserId, Q.Text,Q.Hashtag,C.Name,U.Username,[dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId WHERE (Q.Text LIKE '%'+@searchstring+'%') OR (U.Username LIKE '%'+@searchstring+'%') OR (Q.Hashtag LIKE '%'+@searchstring+'%') OR (C.Name LIKE '%'+@searchstring+'%') ORDER BY Q.RegDate DESC";

        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@searchstring",SearchString);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            SearchList.DataSource = dr;
            SearchList.DataBind();
        }

        else
        {
            SearchStringlbl.Text = "Üzgünüz :( Aramayla eşleşen hiçbir Soralet bulamadık.";
        }
        con.Close();
    }

    protected void GetHashtagQueries(String SearchString)
    {
        String query = "SELECT TOP 100 Q.Id, Q.UserId, Q.Text,Q.Hashtag,C.Name,U.Username,[dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId WHERE Q.Hashtag LIKE '%'+@searchstring+'%' ORDER BY Q.RegDate DESC";

        String tempStr = SearchString.Remove(0,1);
        SearchStringlbl.Text = "#" + tempStr;
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@searchstring", tempStr);
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.HasRows)
        {
            SearchList.DataSource = dr;
            SearchList.DataBind();
        }

        else
        {
            SearchStringlbl.Text = "Üzgünüz :( Aramayla eşleşen hiçbir Soralet bulamadık.";
        }
        con.Close();
    }

    protected void GetCategoryQueries(String SearchString)
    {
        String query = "SELECT TOP 100 Q.Id, Q.UserId, Q.Text,Q.Hashtag,C.Name,U.Username,[dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId WHERE C.Name LIKE '%'+@searchstring+'%' ORDER BY Q.RegDate DESC";

        String tempStr = SearchString.Remove(0, 1);
        SearchStringlbl.Text = "*" + tempStr;
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@searchstring", tempStr);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            SearchList.DataSource = dr;
            SearchList.DataBind();
        }

        else
        {
            SearchStringlbl.Text = "Üzgünüz :( Aramayla eşleşen hiçbir Soralet bulamadık.";
        }
        con.Close();
    }

    protected void GetUsersQueries(String SearchString)
    {
        String query = "SELECT TOP 1 Q.Id, Q.Text, U.Username, U.Id AS UserId, [dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter AS AnswerCount, UQC.UserQueryCount AS QueryCount FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId LEFT JOIN [dbo].[UserQueryCounts] UQC ON UQC.UserId=Q.UserId WHERE U.Username LIKE '%'+@searchstring+'%' ORDER BY Q.RegDate DESC";

        String tempStr = SearchString.Remove(0, 1);
        SearchStringlbl.Text = "@" + tempStr;
        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@searchstring", tempStr);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            SearchUserList.DataSource = dr;
            SearchUserList.DataBind();
        }

        else
        {
            SearchStringlbl.Text = "Üzgünüz :( Aramayla eşleşen hiçbir Soraletci bulamadık.";
        }
        con.Close();
    }


    protected string FormatUrl(int Id)
    {
        return "Answers.aspx?QueryId=" + Id;

    }

    protected string FormatUrlUser(int Id)
    {
        return "OProfile.aspx?UserId=" + Id;

    }
}