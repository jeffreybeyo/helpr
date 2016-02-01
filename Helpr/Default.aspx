<%@ Page Title="Ya öğren, ya terket!" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--DATA SOURCE LIST QUERY -->
    <asp:SqlDataSource ID="ListQueriesSql" runat="server" ConnectionString="<%$ ConnectionStrings:HelperConnectionString %>"
        SelectCommand="SELECT TOP 100 Q.Id, Q.UserId, Q.Text,Q.Hashtag,C.Name,U.Username,[dbo].[GetPostedOnDate](Q.RegDate) AS RegDate, AC.Counter FROM [dbo].[Queries] AS Q
LEFT JOIN [dbo].[Users] U ON Q.UserId=U.Id
LEFT JOIN [dbo].[Categories] C ON C.Id=Q.CategoryId
LEFT JOIN [dbo].[AnswersCount] AC ON Q.Id=AC.QueryId
ORDER BY Q.RegDate DESC"></asp:SqlDataSource>
<!--DATA SOURCE LIST CATEGORY -->
    <asp:SqlDataSource ID="ListCategorySql" runat="server" ConnectionString="<%$ ConnectionStrings:HelperConnectionString %>"
        SelectCommand="SELECT Id, Name FROM [dbo].[Categories] ORDER BY Name"></asp:SqlDataSource>

        <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><center><asp:Label ID="lblLoginError" runat="server"></asp:Label></center></asp:Panel> 
<!--ADD QUERY -->  
    <br />
    <div>
    <center><button type="button" class="btn btn-warning btn-lg btn-block" data-toggle="modal" data-target="#myModal"><span class="glyphicon glyphicon-check" aria-hidden="true"></span>&nbsp;Çak bi Soralet!</button></center>
</div>
<div id="myModal" class="modal fade" role="dialog">
      <div class="modal-dialog">
	<div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
         <center><h5 class="modal-title">Yeni Soralet oluştur</h5></center>
        </div>
		<div class="modal-body">
			
            <!-- content goes here -->
                <center>
                <asp:TextBox ID="AddQuerytxt" TextMode="MultiLine" Cols="20" Rows="3" class="form-control" placeholder="Neyi merak ettin?" runat="server"></asp:TextBox>
            </center>
            </div>
		<div class="modal-footer">
            
              <div class="form-group col-md-4">
                <asp:TextBox ID="AddHashtagtxt" class="form-control" placeholder="etiket" runat="server"></asp:TextBox>
              </div>
                <div class="form-group col-md-4">
                <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" DataSourceID="ListCategorySql" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                </div>

			<div class="btn-group" role="group" aria-label="group button">
				<div class="btn-group" role="group">
					<asp:Button ID="Button1" class="btn btn-block btn-warning" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Çak bi Soralet!&nbsp;&nbsp;&nbsp;&nbsp;" OnClick="BtnQuerySubmit_Click"/>
                    <br /><br />
                   <b><asp:RequiredFieldValidator ID="RfvQuery" runat="server" Display="Dynamic" ErrorMessage="<span class='glyphicon glyphicon-remove-sign' aria-hidden='true'></span> Soru boş kalamaz." ControlToValidate="AddQuerytxt"></asp:RequiredFieldValidator></b>
				</div>
			</div>
		</div>
	</div>
  </div>
</div>

    <br /><br />
<!--LIST QUERIES -->

    <asp:ListView id="QueryList" runat="server">
    <ItemTemplate>
        <section class="col-md-4 your-class">
            <div class="quote">
              <div>
                <!--<img src="img/matt-berninger.jpg" class="quote-face" />-->
                <blockquote>
                  <p>
                      <a href='<%# FormatUrlUser( (int) Eval("UserId")) %>' role="button"><span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span></a>
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

</asp:Content>
