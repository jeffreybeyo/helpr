<%@ Page Title="Satisfy Your Curiosity" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!--DATA SOURCE LIST QUERY -->
    <asp:SqlDataSource ID="ListQueriesSql" runat="server" ConnectionString="<%$ ConnectionStrings:HelperConnectionString %>"
        SelectCommand="SELECT TOP 100 Q.Id,Q.Text,Q.Hashtag,C.Name,U.Username,[dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q
LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id
LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId
LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId
ORDER BY Q.RegDate DESC"></asp:SqlDataSource>
<!--DATA SOURCE LIST CATEGORY -->
    <asp:SqlDataSource ID="ListCategorySql" runat="server" ConnectionString="<%$ ConnectionStrings:HelperConnectionString %>"
        SelectCommand="SELECT Id, Name FROM [dbo].[Categories] ORDER BY Name"></asp:SqlDataSource>

        <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><center><asp:Label ID="lblLoginError" runat="server"></asp:Label></center></asp:Panel> 
<!--ADD QUERY -->
    <div class="alert alert-warning text-center" role="alert">
        
<div class="row your-class">
  <div class="col-lg-4">
    <div class="input-group input-group-lg">
       <span class="input-group-addon" id="sizing-addon1" aria-label="Query">Query&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
      <asp:TextBox ID="AddQuerytxt" class="form-control" placeholder="So...?" runat="server"></asp:TextBox>
    </div><!-- /input-group -->
  </div><!-- /.col-lg-4 -->

  <div class="col-lg-4">
    <div class="input-group input-group-lg">
       <span class="input-group-addon" id="sizing-addon3">Category</span>
        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" DataSourceID="ListCategorySql" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
    </div><!-- /input-group -->
  </div><!-- /.col-lg-4 -->

      <div class="col-lg-4">
    <div class="input-group input-group-lg">
      <span class="input-group-addon" id="sizing-addon4">#&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
        <asp:TextBox ID="AddHashtagtxt" class="form-control" placeholder="hashtag" runat="server"></asp:TextBox>
    </div><!-- /input-group -->  
  </div><!-- /.col-lg-4 -->

    <div class="col-lg-2">
    <div class="input-group input-group-lg">
     <asp:Button ID="Button1" class="btn btn-default btn-lg" runat="server" Text="Go!" OnClick="BtnQuerySubmit_Click" />
    </div><!-- /input-group -->
  </div><!-- /.col-lg-2 -->
</div><!-- /.row -->
        </div>


<b><asp:RequiredFieldValidator ID="RfvQuery" runat="server" Display="Dynamic" ErrorMessage="<span class='glyphicon glyphicon-remove-sign' aria-hidden='true'></span> Please fill all the inputs" ControlToValidate="AddQuerytxt"></asp:RequiredFieldValidator></b>

    <br />
<!--LIST QUERIES -->
<div class="row">
    <asp:ListView id="QueryList" runat="server">
    <ItemTemplate>
        <section class="col-md-4 your-class">
            <div class="quote">
              <div>
                <!--<img src="img/matt-berninger.jpg" class="quote-face" />-->
                <blockquote>
                  <p>
                      <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
                      <span class="label label-info"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Counterlbl" runat="server" Text='<%#Eval("Counter")%>'></asp:Label></span>
                      <span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Categorylbl" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span>
                      
                    <%--<p><asp:Label ID="Date" runat="server" Text='<%#Eval("RegDate")%>'></asp:Label>--%>
                    
                    <div class="text-center">
                      <a href='<%# FormatUrl( (int) Eval("Id")) %>' role="button"><h3><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label></h3></a>
                  </div>
                      <p class="pull-right"><small>#<asp:Label ID="Hashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label></small></p>
                   </p>
                </blockquote>
              </div>
            </div><hr />
          </section>
        
         </ItemTemplate>
    </asp:ListView>
</div>
</asp:Content>
