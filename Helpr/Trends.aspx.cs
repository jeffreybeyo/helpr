using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Trends : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        GetTrendHashtags();
        GetPopularQueries();
        GetPopularCategories();
    }

    private void GetTrendHashtags()
    {
        String query = "SELECT  TOP 4 Q.Hashtag, COUNT(Q.Hashtag) AS HCounter, COUNT(FU.QueryId) AS FUCounter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[FollowUp] FU ON FU.QueryId=Q.Id WHERE Q.Hashtag!='' GROUP BY Q.Hashtag ORDER BY HCounter DESC, FUCounter DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        HashtagList.DataSource = dr;
        HashtagList.DataBind();
        con.Close();
    }

    private void GetPopularCategories()
    {
        String query = "SELECT TOP 4 COUNT(C.Id) AS CCount, C.Name FROM [dbo].[Categories] AS C INNER JOIN [dbo].[Queries] Q ON Q.CategoryId=C.Id GROUP BY C.Name ORDER BY CCount DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        CategoryList.DataSource = dr;
        CategoryList.DataBind();
        con.Close();
    }

    private void GetPopularQueries()
    {
        String query = "SELECT TOP 10 Q.Id, Q.UserId, Q.Text,Q.Hashtag,C.Name,U.Username,Q.RegDate, AC.Counter AS AnswerCount, FUC.FUCount AS FollowUpCount, ((AC.Counter*0.6)+(FUC.FUCount*0.4)) AS 'TotalCount' FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId LEFT JOIN [dbo].[FollowUpCount] FUc ON FUC.QueryId=Q.Id ORDER BY TotalCount DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        PopularQList.DataSource = dr;
        PopularQList.DataBind();
        con.Close();
    }

    protected void hashtag_click(object sender, EventArgs e)
    {
        String query = "SELECT Q.Text, Q.Id Q.Hashtag, C.Name, U.Username, AC.Counter AS AnswerCount FROM [dbo].[Queries] AS Q  INNER JOIN [dbo].[Users] U ON U.Id=Q.UserId INNER JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId INNER JOIN [dbo].[AnswersCount] AC ON AC.QueryId=Q.Id WHERE Q.Hashtag=@hashtag";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@hashtag", "cokaciz");
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        
        PopularQList.DataSource = dr;
        PopularQList.DataBind();
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

    protected string FormatUrlSearchHash(String Hashtag)
    {
        Hashtag = "-" + Hashtag;
        return "Search.aspx?SearchString=" + Hashtag;
    }

    protected string FormatUrlSearchCat(String Category)
    {
        Category = "*" + Category;
        return "Search.aspx?SearchString=" + Category;
    }
}