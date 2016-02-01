<%@ Page Title="Trends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Trends.aspx.cs" Inherits="Trends" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div id="wrapper">

        <!-- Sidebar -->
        <div id="sidebar-wrapper">
            <ul class="sidebar-nav">
                <li class="sidebar-brand">
                    <center><a href="#"># Popüler Etiketler</a></center>
                </li>
               <asp:ListView id="HashtagList" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item"><a class="btn" href='<%# FormatUrlSearchHash( (String) Eval("Hashtag")) %>' role="button">#<asp:Label ID="Hashtag" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label>
                                <span class="badge pull-right"><asp:Label ID="HCounter" runat="server" Text='<%#Eval("HCounter")%>'></asp:Label> </span>
                                 </a></li>
                        </ItemTemplate>  
                     </asp:ListView>
             
                <li class="sidebar-brand">
                    <a href="#"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;Popüler Kategoriler</a>
                </li>

                <asp:ListView id="CategoryList" runat="server">
                <ItemTemplate>
                <li class="list-group-item">
                <a class="btn" href='<%# FormatUrlSearchCat( (String) Eval("Name")) %>' role="button"><span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Category" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span></a>
                </li>
                </ItemTemplate>  
                </asp:ListView>
            </ul>
        </div>
        <!-- /#sidebar-wrapper -->
    <br />
    <a href="#menu-toggle" class="btn btn-warning" id="menu-toggle"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span></a>
    <br /><br />
    <h2><span class="glyphicon glyphicon-fire" aria-hidden="true"></span>&nbsp;Kafaya Takılanlar</h2>
        <!-- Page Content -->
            <div class="container-fluid">
                <div class="row">
                        <div class="row">
                            <asp:ListView id="PopularQList" runat="server">
                                <ItemTemplate>

            <section class="col-md-12 your-class">
            <div class="quote">
              <div>
                <!--<img src="img/matt-berninger.jpg" class="quote-face" />-->
                <blockquote>
                  <p>
                      <a href='<%# FormatUrlUser( (int) Eval("UserId")) %>' role="button"><span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="Label1" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span></a>
                      <span class="label label-info"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Label2" runat="server" Text='<%#Eval("AnswerCount")%>'></asp:Label></span>
                      <span class="label label-success"><span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>&nbsp;<asp:Label ID="Label3" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span>
                      
                    <%--<p><asp:Label ID="Date" runat="server" Text='<%#Eval("RegDate")%>'></asp:Label>--%>
                    
                    <div class="text-left">
                      <a href='<%# FormatUrl( (int) Eval("Id")) %>' role="button"><h3><asp:Label ID="Label4" runat="server" Text='<%#Eval("Text") %>'></asp:Label></h3></a>
                  </div>
                      <p class="pull-right"><small>#<asp:Label ID="Label5" runat="server" Text='<%#Eval("Hashtag")%>'></asp:Label></small></p>
                   </p>
                </blockquote>
              </div>
            </div><hr />
          </section>
         </ItemTemplate>
    </asp:ListView>
                  </div>
                    </div>
            </div>
        <!-- /#page-content-wrapper -->

    </div>
    <!-- /#wrapper -->

    <!-- Menu Toggle Script -->
    <script>
        $("#menu-toggle").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
        });
    </script>


</asp:Content>
