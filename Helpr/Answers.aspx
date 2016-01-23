<%@ Page Title="Answers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeFile="Answers.aspx.cs" Inherits="Answers" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="jumbotron">
      <span class="text-muted pull-right">
                 <asp:Panel ID="PanelUp" runat="server" Visible="true">
                <asp:LinkButton ID="BtnFollowup" runat="server" CssClass="btn btn-default btn-sm" OnClick="BtnFollowup_Add" CausesValidation="False" Visible="true">
                <span aria-hidden="true" class="glyphicon glyphicon-flag"></span>Follow Up!
                </asp:LinkButton>
                 </asp:Panel>
                 <asp:Panel ID="PanelDown" runat="server" Visible="false">
                 <asp:LinkButton ID="BtnFollowdown" runat="server" CssClass="btn btn-danger btn-sm" OnClick="BtnFollowup_Delete" CausesValidation="False" Visible="true">
                <span aria-hidden="true" class="glyphicon glyphicon-flag"></span>Let Go...
                </asp:LinkButton>
                </asp:Panel>
            </span>
    <asp:ListView id="QueryView" runat="server" Visible="true">
     <ItemTemplate>
        <h4><span class="label label-success"><asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span>
        #<asp:Label ID="lblHashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label>
           
  <h1><asp:Label ID="lblQuery" runat="server" Text='<%#Eval("Text")%>'></asp:Label><small>&nbsp;-&nbsp;<asp:Label ID="QUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></small></h1>
</ItemTemplate>
</asp:ListView>
<div class="input-group input-group-lg">
<%--    <asp:Panel ID="Panel2" runat="server">--%>
  <span class="input-group-addon" id="sizing-addon1">
      <span class="badge"><asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter")%>'></asp:Label></span> answers already</span>
    <asp:TextBox ID="txtAnswer" runat="server" class="form-control" placeholder="Share your opinion" aria-describedby="sizing-addon1"></asp:TextBox>
    <asp:Button ID="BtnAddAnswer" class="btn btn-default btn-lg" runat="server" Text="Go!"  OnClick="BtnAnswerSubmit_Click" />
<%--       </asp:Panel>--%>
</div>

              <center><b><asp:RequiredFieldValidator ID="RfvAnswer" runat="server" Display="Dynamic" ErrorMessage="<span class='glyphicon glyphicon-remove-sign' aria-hidden='true'></span> Please fill all the inputs" ControlToValidate="txtAnswer"></asp:RequiredFieldValidator></b></center>
</div>
 <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label></asp:Panel> 
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
