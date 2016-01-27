<%@ Page Title="Profile" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="UProfile.aspx.cs" Inherits="UProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" Visible="false" runat="server" class="alert alert-danger" role="alert"><asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label></asp:Panel> 

        <div class="card hovercard">
        <div class="card-background">
            <img class="card-bkimg" alt="" src="https://media.licdn.com/mpr/mpr/shrinknp_200_200/p/3/005/01e/185/2954028.jpg">
        </div>
            
        <div class="useravatar">
            <img alt="" src="https://media.licdn.com/mpr/mpr/shrinknp_200_200/p/3/005/01e/185/2954028.jpg">
        </div>
        <div class="card-info"> <span class="card-title"><asp:Label ID="lblUsername" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>

        </div>
    </div>
    <div class="btn-pref btn-group btn-group-justified btn-group-lg" role="group" aria-label="...">
        <div class="btn-group" role="group">
            <button type="button" id="queries" class="btn btn-primary" href="#tab1" data-toggle="tab"><span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span>&nbsp;<b><asp:Label ID="UQueryCount" runat="server"></asp:Label></b>
                <div class="hidden-xs">Queries</div>
            </button>
        </div>
        <div class="btn-group" role="group">
            <button type="button" id="answers" class="btn btn-default" href="#tab2" data-toggle="tab"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<b><asp:Label ID="UAnswerCount" runat="server"></asp:Label></b>
                <div class="hidden-xs">Answers</div>
            </button>
        </div>
    </div>

        <div class="well">
      <div class="tab-content">
          <!-- TAB QUERIES -->
        <div class="tab-pane fade in active" id="tab1">
            <div class="row">
       <asp:ListView id="UserQueryList" runat="server">
        <ItemTemplate>
            <div class="col-md-4">
               
            <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
             <p class="pull-right">
                <asp:LinkButton ID="btnRemoveQuery" runat="server" OnClick="DeleteQuery_Click" OnClientClick="return confirm('Are you sure?')" CssClass="glyphicon glyphicon-remove gi-5x" CommandArgument='<%#Eval("Id")%>' />
            </p>
                    <h4><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label> <small> - <asp:Label ID="Date" runat="server" Text='<%#Eval("RegDate")%>'></asp:Label></small></h4>
            
            <p><span class="badge"><asp:Label ID="Counterlbl" runat="server" Text='<%#Eval("Counter")%>'></asp:Label></span> answers. <a class="btn" href='<%# FormatUrl( (int) Eval("Id")) %>' role="button">View details &raquo;</a></p>    
        <hr />
            </div>
       </ItemTemplate>
      </asp:ListView>
</div>
 </div>
    <!-- TAB QUERIES END-->


        <!-- TAB ANSWERS-->
        <div class="tab-pane fade in" id="tab2">
                     <div class="row">
       <asp:ListView id="UserAnswerList" runat="server">
        <ItemTemplate>
            <div class="col-md-12">
               
            <span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
             <%--<p class="pull-right">
                <asp:LinkButton ID="btnRemoveQuery" runat="server" OnClick="DeleteQuery_Click" OnClientClick="return confirm('Are you sure?')" CssClass="glyphicon glyphicon-remove gi-5x" CommandArgument='<%#Eval("Id")%>' />
            </p>--%>
              <a href='<%# FormatUrl( (int) Eval("Id")) %>' role="button"> <i>"<asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("QText") %> '></asp:Label>"</i></a>
                <br />   
                <b><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span><asp:Label ID="AnswerTextlbl" runat="server" Text='<%#Eval("AText") %>'></asp:Label></b>
        <hr />
            </div>
       </ItemTemplate>
      </asp:ListView>
</div>
</div>

          <!-- TAB ANSWERS END-->

      </div>
    </div>

</asp:Content>