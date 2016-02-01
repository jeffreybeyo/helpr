<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row row-padded">
    <center><h2> <asp:Label ID="SearchStringlbl" runat="server"></asp:Label> </h2></center>
    </div> <!-- row -->
    <br />
<!--LIST SEARCH QUERIES -->
    
        <div class="row">
            <center>
   <asp:ListView id="SearchList" runat="server">
    <ItemTemplate>
        <section class="col-md-12">
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


<asp:ListView id="SearchUserList" runat="server">
<ItemTemplate>

    <div class="container">
    <div class="span3 well">
        <center>
        <a href="#aboutModal" data-toggle="modal" data-target="#myModal"><img src="http://iol13.linguistics-bg.com/wp-content/uploads/2014/08/default-avatar.png" name="aboutme" width="140" height="140" class="img-circle"></a>
        <h3><a href='<%# FormatUrlUser( (int) Eval("UserId")) %>' role="button"><span class="label label-warning"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;<asp:Label ID="QueryUserlbl" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span></a></h3>
        <em>Daha fazlası için profil resmine tıkla</em>
		</center>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title" id="myModalLabel">Kişi kartı</h4>
                    </div>
                <div class="modal-body">
                    <center>
                    <img src="http://iol13.linguistics-bg.com/wp-content/uploads/2014/08/default-avatar.png" name="soralet" width="140" height="140" border="0" class="img-circle"></a>
                    <h3 class="media-heading"><a href='<%# FormatUrlUser( (int) Eval("UserId")) %>' role="button"><asp:Label ID="Label1" runat="server" Text='<%#Eval("Username")%>'></asp:Label></span></a></h3>
                    
                        <span class="label label-warning"><span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Label3" runat="server" Text='<%#Eval("QueryCount")%>'></asp:Label></span>
                        <span class="label label-info"><span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>&nbsp;<asp:Label ID="Label2" runat="server" Text='<%#Eval("AnswerCount")%>'></asp:Label></span>
                    </center>
                    <hr>
                    <center>
                    <p class="text-left"><strong>Son paylaştığı Soralet: </strong><br>
                       <a href='<%# FormatUrl( (int) Eval("Id")) %>' role="button"><asp:Label ID="QueryTextlbl" runat="server" Text='<%#Eval("Text") %>'></asp:Label></a></p>
                    <br>
                    </center>
                </div>
                <div class="modal-footer">
                    <center>
                    <button type="button" class="btn btn-warning" data-dismiss="modal">Kapat</button>
                    </center>
                </div>
            </div>
        </div>
    </div>
</div>
        
</ItemTemplate>
</asp:ListView>
</center>
</div>

</asp:Content>