using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Answers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    int Queryglb;
    String title;
    String text;
    String username;
    protected void Page_Load(object sender, EventArgs e)
    {
        int QueryId = -1;

        if (Request.Params["QueryId"] != null && int.TryParse(Request.Params["QueryId"], out QueryId))
        {
            GetQuery(QueryId);
            GetAnswers(QueryId);
            Queryglb = QueryId;
            CheckFollowUp();
        }

        else
        {
            PanelDown.Visible = false;
            PanelUp.Visible = false;
            BtnAddAnswer.Visible = false;
            txtAnswer.Visible = false;
        }



    }

    protected void CheckFollowUp()
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "SELECT * FROM [dbo].[FollowUp] AS FU WHERE FU.UserId=@userid AND FU.QueryId=@queryid";
            SqlCommand cmd_check = new SqlCommand(query, con);
            cmd_check.Parameters.AddWithValue("@userid", userid);
            cmd_check.Parameters.AddWithValue("@queryid", Queryglb);
            con.Open();
            SqlDataReader dr = cmd_check.ExecuteReader();

            if (dr.HasRows)
            {
                PanelDown.Visible = true;
                PanelUp.Visible = false;

            }
            con.Close();
        }

        else
        {
            lblLoginError.Text = "You must Log In or Sign Up to follow up this.";
            lblLoginError.Visible = true;
        }


    }


    private void GetAnswers(int QueryId)
    {

        String queryans = String.Format("SELECT A.Text, U.Username, A.RegDate FROM [dbo].[Answers] AS A INNER JOIN [dbo].[Users] U ON U.Id=A.UserId WHERE A.QueryId ={0} ORDER BY RegDate DESC", QueryId);

        con.Open();
        SqlCommand cmd = new SqlCommand(queryans, con);
        SqlDataReader dr = cmd.ExecuteReader();
        AnswersList.DataSource = dr;
        AnswersList.DataBind();
        con.Close();

    }

    private void GetQuery(int QueryId)
    {
        String queryque = String.Format("SELECT Q.Text, Q.Hashtag, C.Name, U.Username, Q.RegDate, AC.Counter FROM [dbo].[Queries] AS Q INNER JOIN [dbo].[Categories] C ON C.Id= Q.CategoryId INNER JOIN [dbo].[Users] U ON U.Id=Q.UserId LEFT JOIN [dbo].[AnswersCount] AC ON AC.QueryId=Q.Id WHERE Q.Id={0}", QueryId);

        con.Open();
        SqlCommand cmd2 = new SqlCommand(queryque, con);      
        SqlDataReader dr2 = cmd2.ExecuteReader();
        QueryView.DataSource = dr2;
        QueryView.DataBind();
        con.Close();

        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter adap = new SqlDataAdapter(cmd2);
        adap.Fill(dt);
        String result = dt.Rows[0]["Text"].ToString();
        Page.Title = result;
        con.Close();

        //get answer counter
        String queryans = String.Format("SELECT * FROM [dbo].[AnswersCount] AS AC WHERE AC.QueryId={0}", QueryId);

        con.Open();
        SqlCommand cmd3 = new SqlCommand(queryans, con);
        SqlDataReader dr3 = cmd2.ExecuteReader();
        CounterView.DataSource = dr3;
        CounterView.DataBind();
        con.Close();
    }



    protected void BtnAnswerSubmit_Click(object sender, EventArgs e)
    {

        object userid = Session["UserId"];
        if (userid != null)
        {
            string q = txtAnswer.Text.Replace("'", "''"); //apostrophe problem fixed
            if (q.Contains("sex") || q.Contains("seks") || q.Contains("prezervatif") || q.Contains("yarrak") || q.Contains("malafat") || q.Contains("pezevenk") || q.Contains("sikis") || q.Contains("sikiş") || q.Contains("condom") || q.Contains("kondom") || q.Contains(" am ") || q.Contains(" amcik ") || q.Contains(" sik ") || q.Contains(" amina ") || q.Contains(" amına ") || q.Contains("amk") || q.Contains("Amk") || q.Contains("veled-i zina") || q.Contains("orospu") || q.Contains("yavsak") || q.Contains("yavşak") || q.Contains("ibne") || q.Contains("göt") || q.Contains("fuck") || q.Contains("siktir"))
            {
                lblLoginError.Text = "So nasty, but not for this environment.";
                Panel1.Visible = true;
            }

            else
            {
                String query = "INSERT INTO [dbo].[Answers](Text, QueryId, UserId) values('" + q + "' , '" + Queryglb + "' , '" + userid + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        else
        {
            lblLoginError.Text = "You must Log In or Sign Up to answer this.";
            Panel1.Visible = true;
        }
    }

    protected void BtnFollowup_Add(object sender, EventArgs e)
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "INSERT INTO [dbo].[FollowUp](UserId, QueryId) values('" + userid + "', '" + Queryglb + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            PanelUp.Visible = false;
            PanelDown.Visible = true;
        }

        else
        {
            lblLoginError.Text = "You must Log In or Sign Up to follow up this.";
            Panel1.Visible = true;
        }
    }

    protected void BtnFollowup_Delete(object sender, EventArgs e)
    {
        object userid = Session["UserId"];
        if (userid != null)
        {
            String query = "DELETE FROM [dbo].[FollowUp] WHERE UserId=@userid AND QueryId=@queryid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@queryid", Queryglb);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

    }
}