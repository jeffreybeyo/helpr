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
        if (!Page.IsPostBack)
        {
            GetQueries();
        }
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
            object userid = Session["UserId"];
            if (userid != null)
            {
                int selected = Convert.ToInt32(ddlCategory.SelectedValue);
                
                //fix apostrophe problem
                string q = AddQuerytxt.Text.Replace("'", "''"); 
                
                //avoid bad words
                if (q.Contains("sex") || q.Contains("seks") || q.Contains("prezervatif") || q.Contains("yarrak") || q.Contains("malafat") || q.Contains("pezevenk") || q.Contains("sikis") || q.Contains("sikiş") || q.Contains("condom") || q.Contains("kondom") || q.Contains(" am ") || q.Contains(" amcik ") || q.Contains(" sik ") || q.Contains(" amina ") || q.Contains(" amına ") || q.Contains("amk") || q.Contains("Amk") || q.Contains("veled-i zina") || q.Contains("orospu") || q.Contains("yavsak") || q.Contains("yavşak") || q.Contains("ibne") || q.Contains("göt") || q.Contains("fuck") || q.Contains("siktir"))
                {
                    lblLoginError.Text = "Çok ilginç... Ama buraya uygun değil.";
                    Panel1.Visible = true;
                }

                else
                {
                String h =AddHashtagtxt.Text.Replace(" ", String.Empty);
                String query = "INSERT INTO [dbo].[Queries](Text, Hashtag, CategoryId, UserId) values('" + q + "' , '" + h + "' , '" + selected + "', '" + userid + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
            }

            else
            {
                lblLoginError.Text = "Soru sormak için Giriş Yap veya Kayıt Ol.";
                Panel1.Visible = true;
            }
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