<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="USettings.aspx.cs" Inherits="USettings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
    <div class="row">
    	<div class="col-md-3">
            <div class="list-group" id="sidebar">
            	<a href="#" class="list-group-item">Upload Profile Picture</a>
 				<a href="#" class="list-group-item">Change User Info</a>
 				<a href="#" class="list-group-item">User Preferences</a>
            </div>
        </div>
        <div class="col-md-9">
          <h2>Upload Profile Picture</h2>
          <p>Coming soon...</p>
          <hr class="col-md-12">
          
          <h2>Change User Info</h2>
          <p>Coming soon...
          </p><hr class="col-md-12">
                 
          <h2>User Preferences</h2>
          <p>Coming soon...</p>
           <button class="btn" contenteditable="false">Change Preferences</button>
        </div>
        <div class="span9"></div>
    </div>
</div>

</asp:Content>