 <%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Profile.aspx.vb" Inherits="CraftSwap.Profile" MasterPageFile="~/Site.Master" %>

<asp:Content ID="AboutUsContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<div class="body-content">
		
		

		<section class="center">

			<h1>Welcome to your profile, <% Response.Write(ReturnUsername()) %></h1>

			<div class="left-column">
					<p>In order to facilitate successful swaps, you will need to offer a pick-up and/or drop-off address and instructions. Addresses will not be shown on the site, but will be emailed to members who choose to swap with you.</p>
					
					<% Response.Write(PopulateLeftCol()) %>
			</div>

			<div class="right-column">

				<% Response.Write(PopulateRightCol()) %>

				<br />

				
				<div class="form-group">
					<br />
					<div class="checkbox-regular">
						
						<a href="Edit-Profile.aspx" class="list-button">Edit Profile</a><br />
					</div>

				</div>

			</div>
			<h1>CraftSwap Site History</h1>
			
			<form runat="server">

				<% Response.Write(PopulateSiteHistory()) %>

			</form>

			<a href="Add-Product.aspx" class="list-button">New Product</a>

		</section>
	</div>
</asp:Content>
