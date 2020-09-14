<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Product-List.aspx.vb" Inherits="CraftSwap.Category" MasterPageFile="~/Site.Master" %>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
        <section class="query-results">
			<% Response.Write(GetListTypeHeading$) %>

			<div class="query-list">
			
				<% Response.Write(PopulatePage) %>

			</div>
		</section>
    </div>
</asp:Content>

