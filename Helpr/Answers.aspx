<%@ Page Title="Answers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeFile="Answers.aspx.cs" Inherits="Answers" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="jumbotron">
    <asp:ListView id="QueryView" runat="server">
     <ItemTemplate>
         <h4><span class="label label-success   "><asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span>
        #<asp:Label ID="lblHashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label>
  <h1><asp:Label ID="lblQuery" runat="server" Text='<%#Eval("Text")%>'></asp:Label><small>&nbsp;-&nbsp;<asp:Label ID="QUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></small></h1>
<div class="input-group input-group-lg">
  <span class="input-group-addon" id="sizing-addon1"><span class="badge"><asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter")%>'></asp:Label></span> answers already</span>
  <input type="text" class="form-control" placeholder="Share your opinion" aria-describedby="sizing-addon1"><button class="btn btn-default btn-lg" type="button">Go!</button>
</div>
</ItemTemplate>
</asp:ListView>
</div>

<asp:ListView id="AnswersList" runat="server">
        <ItemTemplate>
<div class="panel panel-default">
    <div class="panel-body">
        <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="lblUsername" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
        <asp:Label ID="Answerlbl" runat="server" Text='<%#Eval("Text")%>'></asp:Label>
    </div>
</div>
         </ItemTemplate>
</asp:ListView>
    
</asp:Content>
