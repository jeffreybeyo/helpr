﻿using System;
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
    protected void Page_Load(object sender, EventArgs e)
    { 
        int QueryId = -1;

        if (Request.Params["QueryId"] != null && int.TryParse(Request.Params["QueryId"], out QueryId))
        {
            GetQuery(QueryId);
            GetAnswers(QueryId);
         }
        
    }


    private void GetAnswers(int QueryId)
    {
        String queryans = String.Format("SELECT A.Text, U.Username, A.RegDate FROM [dbo].[Answers] AS A INNER JOIN [dbo].[Users] U ON U.Id=A.UserId WHERE A.QueryId ={0}", QueryId);        
        
        con.Open();
        SqlCommand cmd = new SqlCommand(queryans, con);
        SqlDataReader dr = cmd.ExecuteReader();
        AnswersList.DataSource = dr;
        AnswersList.DataBind();
        con.Close();
    }

    private void GetQuery(int QueryId)
    {
       String queryque = String.Format("SELECT Q.Text, Q.Hashtag, C.Name, U.Username, Q.RegDate, AC.Counter FROM [dbo].[Queries] AS Q INNER JOIN [dbo].[Categories] C ON C.Id= Q.CategoryId INNER JOIN [dbo].[Users] U ON U.Id=Q.UserId LEFT JOIN	[dbo].[AnswersCount] AC ON AC.QueryId=Q.Id WHERE Q.Id={0}", QueryId);
      
        con.Open();
       SqlCommand cmd2 = new SqlCommand(queryque, con);
       SqlDataReader dr2 = cmd2.ExecuteReader();
       QueryView.DataSource = dr2;
       QueryView.DataBind();
       con.Close();
    }
}