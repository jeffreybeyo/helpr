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
    }

    private void GetTrendHashtags()
    {
        String query = "SELECT  TOP 10 Q.Hashtag, COUNT(Q.Hashtag) AS HCounter, COUNT(FU.QueryId) AS FUCounter FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[FollowUp] FU ON FU.QueryId=Q.Id GROUP BY Q.Hashtag ORDER BY HCounter DESC, FUCounter DESC";
        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        HashtagList.DataSource = dr;
        HashtagList.DataBind();
        con.Close();
    }

    private void GetPopularQueries()
    {
        String query = "SELECT TOP 4 Q.Id,Q.Text,Q.Hashtag,C.Name,U.Username,Q.RegDate, AC.Counter AS AnswerCount, FUC.FUCount AS FollowUpCount FROM [dbo].[Queries] AS Q LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId LEFT JOIN [dbo].[FollowUpCount] FUc ON FUC.QueryId=Q.Id ORDER BY AC.Counter DESC, FUC.FUCount DESC";
        SqlCommand cmd = new SqlCommand(query, con);
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
}