<%@ Page Title="Profile" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="UProfile.aspx.cs" Inherits="UProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label></asp:Panel> 

        <!-- Main jumbotron for a primary marketing message or call to action -->
    <div class="jumbotron">
        <h1 class="display-3">Hello, jeffreybeyo!</h1>
        <p>Welcome to your unique profile. Here, you can easily manage your queries and account data.</p>
        <p><a class="btn btn-primary btn-lg" href="#" role="button">Edit your profile &raquo;</a></p>
      </div>

      <!-- Example row of columns -->
      <div class="row">
       <asp:ListView id="UserQueryList" runat="server">
        <ItemTemplate>
            <div class="col-md-4">
               
            <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
             <p class="pull-right">
                <asp:LinkButton ID="btnRemoveQuery" runat="server" OnClick="DeleteQuery_Click" CssClass="glyphicon glyphicon-remove gi-5x" CommandArgument='<%#Eval("Id")%>' />
            </p>
                    <h4><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label> <small> - <asp:Label ID="Date" runat="server" Text='<%#Eval("RegDate")%>'></asp:Label></small></h4>
            
            <p><span class="badge"><asp:Label ID="Counterlbl" runat="server" Text='<%#Eval("Counter")%>'></asp:Label></span> answers. <a class="btn" href='<%# FormatUrl( (int) Eval("Id")) %>' role="button">View details &raquo;</a></p>    
        <hr />
            </div>
       </ItemTemplate>
      </asp:ListView>
</div>

</asp:Content>