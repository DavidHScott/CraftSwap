<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Product.aspx.vb" Inherits="CraftSwap.Product" MasterPageFile="~/Site.Master" %>

<asp:Content ID="ProductContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
        <section class="product-view">
			<div class="left-column">
				<% Response.Write(PopulateLeftColumn) %>
				<br />
			</div>
			<div class="right-column">
				
				<% Response.Write(PopulatePlanInfo) %>

				<form runat="server">

					<div class='use-token'><asp:Button ID='btnSwapProduct' Text='Swap With Token' CssClass='list-button' runat='server' />
					<p>You have <% Response.Write(PopulateButton()) %> tokens available. <a href='user/buy-tokens.aspx'>Click here</a> to purchase more.</p>
				</div>

				</form>
				<div class="swap-info">
					
					<% Response.Write(PopulateUserDetails) %>

				</div>
			</div>

		</section>
    </div>
</asp:Content>
