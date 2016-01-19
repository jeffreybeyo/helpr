using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        GetQueries();
    }

    private void GetQueries()
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        //String query = "SELECT * FROM [dbo].[Queries]";
        //SqlCommand cmd = new SqlCommand(query, con);
        //con.Open();
        //SqlDataReader dr = cmd.ExecuteReader();

        QueryList.DataSource = ListQueriesSql;
        QueryList.DataBind();

        //con.Close();
    }
    protected void BtnQuerySubmit_Click(object sender, EventArgs e)
    {
            int selected = Convert.ToInt32(ddlCategory.SelectedValue);
            String query = "INSERT INTO [dbo].[Queries](Text, Hashtag, CategoryId) values('" + AddQuerytxt.Text + "' , '" + AddHashtagtxt.Text + "' , " + selected + ")";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        
    }

    protected string FormatUrl(int Id) 
    { 
        return "Answers.aspx?QueryId=" + Id;

    }
}