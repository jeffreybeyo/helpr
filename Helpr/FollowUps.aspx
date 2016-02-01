<%@ Page Language="C#" Title="FollowUps" AutoEventWireup="true" CodeFile="FollowUps.aspx.cs" MasterPageFile="~/Site.Master" Inherits="FollowUps" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label></asp:Panel> 
<br />
<div class="row">
    <span class="label label-danger"><span class="glyphicon glyphicon-flag" aria-hidden="true"></span>&nbsp; Son Takiplerim</span>
                        
    <asp:ListView id="FollowList" runat="server">
        <ItemTemplate>
            
        <section class="col-md-12 your-class">
            <div class="quote">
                <!--<img src="img/matt-berninger.jpg" class="quote-face" />-->
                <blockquote>
                  <p>                      
                    <%--<p><asp:Label ID="Date" runat="server" Text='<%#Eval("RegDate")%>'></asp:Label>--%>
                    
                    <div class="text-left">
                      <a href='<%# FormatUrl( (int) Eval("QueryId")) %>' role="button"><h4><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label></h4></a>
                  </div>
                       <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("QSender")%>'></asp:Label></span>
                      <span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Categorylbl" runat="server" Text='<%#Eval("CName")%>'></asp:Label></span>
                   </p>
                </blockquote>
            </div><hr />
          </section>
            
         </ItemTemplate>
    </asp:ListView>
        
</div>
</asp:Content>