<%@ Page Language="C#" Title="FollowUps" AutoEventWireup="true" CodeFile="FollowUps.aspx.cs" MasterPageFile="~/Site.Master" Inherits="FollowUps" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label></asp:Panel> 
<br />
<div class="row">
    <span class="label label-danger"><span class="glyphicon glyphicon-flag" aria-hidden="true"></span>Last Flagged</span>
                        
    <asp:ListView id="FollowList" runat="server">
        <ItemTemplate>
            
            <ul class="media-list">
            <li class="media">
                <div class="media-left">
                    
                </div>
                <asp:HyperLink ID="HyperLink" runat="server" class="thumbnail" role="alert" NavigateUrl='<%# FormatUrl( (int) Eval("QueryId")) %>'>

                <div class="media-body">
                <h4 class="media-heading"><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label></h4>
                <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("QSender")%>'></asp:Label></span>
                <span class="label label-info"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Counterlbl" runat="server" Text='<%#Eval("ACount")%>'></asp:Label></span>
                 
                <span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Categorylbl" runat="server" Text='<%#Eval("CName")%>'></asp:Label></span>
                &nbsp; #<asp:Label ID="Hashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label>
                </div>
                </asp:HyperLink>
            </li>
            </ul>
            
         </ItemTemplate>
    </asp:ListView>
        
</div>
</asp:Content>