using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;
public partial class Answers : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelperConnectionString"].ConnectionString);
    int Queryglb;
    String glbQueryText;
    String glbQueryUser;
    String glbQueryUserMail;
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
            lblLoginError.Text = "Bu soruyu takip etmek için Giriş Yap veya Kayıt Ol.";
            lblLoginError.Visible = true;
        }


    }


    private void GetAnswers(int QueryId)
    {
        if(QueryId!=null)
        {
        String queryans = String.Format("SELECT A.Text, U.Username, A.UserId, A.RegDate FROM [dbo].[Answers] AS A INNER JOIN [dbo].[Users] U ON U.Id=A.UserId WHERE A.QueryId ={0} ORDER BY RegDate DESC", QueryId);

        con.Open();
        SqlCommand cmd = new SqlCommand(queryans, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            AnswersList.DataSource = dr;
            AnswersList.DataBind();
        }
        con.Close();
        }

        else
        {
            Page.Response.Redirect("Default.aspx", true);
        }

    }

    private void GetQuery(int QueryId)
    {
        String queryque = String.Format("SELECT Q.Text, Q.Hashtag, C.Name, U.Username, U.Email, Q.RegDate, AC.Counter FROM [dbo].[Queries] AS Q INNER JOIN [dbo].[Categories] C ON C.Id= Q.CategoryId INNER JOIN [dbo].[Users] U ON U.Id=Q.UserId LEFT JOIN [dbo].[AnswersCount] AC ON AC.QueryId=Q.Id WHERE Q.Id={0}", QueryId);

        con.Open();
        SqlCommand cmd2 = new SqlCommand(queryque, con);      
        SqlDataReader dr2 = cmd2.ExecuteReader();
        QueryView.DataSource = dr2;
        QueryView.DataBind();
        con.Close();

        //page title, querytext
        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter adap = new SqlDataAdapter(cmd2);
        adap.Fill(dt);
        String result = dt.Rows[0]["Text"].ToString();
        Page.Title = result;
        glbQueryText = result;
        con.Close();

        //query user
        con.Open();
        DataTable dt2 = new DataTable();
        SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
        adap.Fill(dt);
        String result2 = dt.Rows[0]["Username"].ToString();
        Page.Title = result2;
        glbQueryUser = result2;
        con.Close();

        //query user mail
        con.Open();
        DataTable dt3 = new DataTable();
        SqlDataAdapter adap3 = new SqlDataAdapter(cmd2);
        adap.Fill(dt);
        String result3 = dt.Rows[0]["Email"].ToString();
        glbQueryUserMail = result3;
        Page.Title = result2;
        glbQueryUser = result2;
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
        object username = Session["Username"];
        if (userid != null)
        {
            string q = txtAnswer.Text.Replace("'", "''"); //apostrophe problem fixed
            if (q.Contains("sex") || q.Contains("seks") || q.Contains("prezervatif") || q.Contains("yarrak") || q.Contains("malafat") || q.Contains("pezevenk") || q.Contains("sikis") || q.Contains("sikiş") || q.Contains("condom") || q.Contains("kondom") || q.Contains(" am ") || q.Contains(" amcik ") || q.Contains(" sik ") || q.Contains(" amina ") || q.Contains(" amına ") || q.Contains("amk") || q.Contains("Amk") || q.Contains("veled-i zina") || q.Contains("orospu") || q.Contains("yavsak") || q.Contains("yavşak") || q.Contains("ibne") || q.Contains("göt") || q.Contains("fuck") || q.Contains("siktir"))
            {
                lblLoginError.Text = "Çok ilginç... Ama buraya uygun değil.";
                Panel1.Visible = true;
            }

            else
            {
                String query = "INSERT INTO [dbo].[Answers](Text, QueryId, UserId) values('" + q + "' , '" + Queryglb + "' , '" + userid + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                String body = PopulateBody( glbQueryUser.ToString(), username.ToString(), Page.Request.Url.ToString());
                SendMail(glbQueryUserMail.ToString(), "Soralet'ine bir cevap var!", body);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        else
        {
            lblLoginError.Text = "Bu soruyu cevaplamak için Giriş Yap veya Kayıt Ol.";
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
            lblLoginError.Text = "Bu soruyu takip etmek için Giriş Yap veya Kayıt Ol.";
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


    private string PopulateBody(string QUsername, string AUsername, string url)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailHtmlBody.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{AUsername}", AUsername);
        body = body.Replace("{QUsername}", QUsername);
        body = body.Replace("{Url}", url);
        return body;
    }

    public static void SendMail(string recepientEmail, string subject, string body)
    {
        MailMessage email = new MailMessage();
        email.From = new MailAddress("team@soralet.com");
        email.To.Add(new MailAddress(recepientEmail));
        email.Subject = "Soralet'ine cevap geldi!";
        email.IsBodyHtml = true;
        email.Body = body;
        SmtpClient smtp = new SmtpClient();
        smtp.Credentials = new NetworkCredential("team@soralet.com", "123456");
        smtp.Port = 587;
        smtp.Host = "mail.soralet.com";
        smtp.Send(email);
    }



    protected string FormatUrlUser(int Id)
    {
        return "OProfile.aspx?UserId=" + Id;

    }

}