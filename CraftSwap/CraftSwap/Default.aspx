<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="CraftSwap.WebForm1" MasterPageFile="~/Site.Master" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    
    <div class="body-content">

		<section class="recent">
			<h2>Recently Added Plans</h2>

			<div class="horizontal-wrapper">
				
				<% Response.Write(PopulateRecentContent()) %>
				
			</div>
        </section>
				
				<% Response.Write(PopulateCategoryContent("knitting")) %>
        
				<% Response.Write(PopulateCategoryContent("crochet")) %>
        
				<% Response.Write(PopulateCategoryContent("macrame")) %>
        
				<% Response.Write(PopulateCategoryContent("papercraft")) %>
        
				<% Response.Write(PopulateCategoryContent("quilting")) %>
        
				<% Response.Write(PopulateCategoryContent("toymaking")) %>
        
				<% Response.Write(PopulateCategoryContent("sculpting")) %>
        
				<% Response.Write(PopulateCategoryContent("xstitching")) %>
        
				<% Response.Write(PopulateCategoryContent("beading")) %>
        
				<% Response.Write(PopulateCategoryContent("weaving")) %>
        
				<% Response.Write(PopulateCategoryContent("woodworking")) %>
        
				<% Response.Write(PopulateCategoryContent("upcycling")) %>


    </div>

</asp:Content>
