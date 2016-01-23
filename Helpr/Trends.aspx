<%@ Page Title="Trends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Trends.aspx.cs" Inherits="Trends" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

            <!--left side-->
            <div class="col-xs-6">
                <div class="panel panel-warning">
                <div class="panel-heading"><center><h3>Most Used Hashtags</h3></center></div>
                <div class="panel-body">
                <div class="row">
                    <ul class="list-group">
                    <asp:ListView id="HashtagList" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink" runat="server" class="thumbnail"><li class="list-group-item"><span class="badge"> <asp:Label ID="HCounter" runat="server" Text='<%#Eval("HCounter")%>'></asp:Label> </span> 
                                  #<asp:Label ID="Hashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label>
                                 </li>
                            </asp:HyperLink>
                        </ItemTemplate>  
                     </asp:ListView>
                    </ul>
                  </div>
                </div>
               </div>
                </div>
                
            <!--right side-->
             <div class="col-xs-6">
                <div class="panel panel-warning">
                <div class="panel-heading"><center><h3>Popular Queries</h3></center></div>
                <div class="panel-body">
                <div class="row">
                    <asp:ListView id="PopularQList" runat="server">
                        <ItemTemplate>
                             <div class="col-sm-12">
                               <asp:HyperLink ID="HyperLink" runat="server" class="thumbnail" NavigateUrl='<%# FormatUrl( (int) Eval("Id")) %>'>
                                 <p><span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span>
                                    <span class="label label-info"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Counterlbl" runat="server" Text='<%#Eval("AnswerCount")%>'></asp:Label></span>
                                    <span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Categorylbl" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span>
                                 </p>
                      <h3><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label></h3>
                    <p class="text-right">#<asp:Label ID="Hashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label></p>
                </asp:HyperLink>
            </div>
         </ItemTemplate>
    </asp:ListView>
                  </div>
                </div>
               </div>
                </div>
                

        
    </div>
</asp:Content>
