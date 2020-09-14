<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Add-Product.aspx.vb" Inherits="CraftSwap.Add_Product"  MasterPageFile="~/Site.Master"%>


<asp:Content ID="AddProductContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
        <section class="center">

			<h1>Add a plan to Craftswap</h1>
			<div class="full">
				<p>Add the details of a plan you are willing to swap with other crafters. You get one token for every ten plans you upload to the site,
					and one token every time someone successfully swaps a token for one of your plans</p>
			</div>
			<% Response.Write(PopulateProgressBar()) %>
			
			<form runat="server">
		
				<div class="form-group">
					<label for="title">Title*</label><br />
					<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvTitle" ErrorMessage="Title is required" ControlToValidate="txtTitle" runat="server" Display="Dynamic" ForeColor="Red">

                    </asp:RequiredFieldValidator>
					
				</div>

				<div class="form-group">
					<label for="category">Category*</label><br />
					<asp:DropDownList ID="ddlCategory" runat="server">
						<asp:ListItem Value="beading">Beading</asp:ListItem>
						<asp:ListItem Value="crochet">Crochet</asp:ListItem>
						<asp:ListItem Value="knitting">Knitting</asp:ListItem>
						<asp:ListItem Value="macrame">Macrame</asp:ListItem>
						<asp:ListItem Value="papercraft">Papercraft</asp:ListItem>
						<asp:ListItem Value="quilting">Quilting</asp:ListItem>
						<asp:ListItem Value="sculpting">Sculpting &amp; Carving</asp:ListItem>
						<asp:ListItem Value="toymaking">Doll &amp; Toy Making</asp:ListItem>
						<asp:ListItem Value="upcycling">Upcycling</asp:ListItem>
						<asp:ListItem Value="weaving">Weaving</asp:ListItem>
						<asp:ListItem Value="woodworking">Woodworking</asp:ListItem>
						<asp:ListItem Value="xstitching">Cross Stitching</asp:ListItem>
					</asp:DropDownList>
				</div>

				<div class="form-group">
					<label for="author">Author*</label><br />
					<asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAuthor" ErrorMessage="Author is required" ControlToValidate="txtAuthor" runat="server" Display="Dynamic" ForeColor="Red"/>
				</div>

				<div class="form-group">
					<label for="publisher">Published By*</label><br />
					<asp:TextBox ID="txtPublisher" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvPublisher" ErrorMessage="Publisher is required" ControlToValidate="txtPublisher" runat="server" Display="Dynamic" ForeColor="Red" />
				</div>
				<div class="form-group">
					<label for="pages">Pages</label><br />
					<asp:TextBox ID="txtPages" runat="server"></asp:TextBox><br />
					<asp:RangeValidator ID="rvPages" ErrorMessage="Number of pages must be a positive integer ranging from 1 - 999" ControlToValidate="txtPages" runat="server"
						Display="Dynamic" ForeColor="Red" MinimumValue="1" MaximumValue="999"/>
				</div>

				<div class="form-group">
					<label for="publicationDate">Publication Date</label><br />
					<asp:TextBox ID="txtPublication" runat="server"></asp:TextBox><br />
					<asp:RegularExpressionValidator ID="revDate" ErrorMessage="Date must be in dd/mm/yyyy format" ControlToValidate="txtPublication"
						ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" runat="server" Display="Dynamic" ForeColor="Red" />
				</div>
		
				<div class="form-group">

					<div class="upload">
						<label class="uploadfile">Upload Cover Image*</label><br>

						<asp:FileUpload ID="fileImage" runat="server"/><br />
						<asp:CustomValidator ID="cvImageUpload" ControlToValidate="fileImage" runat="server" ErrorMessage="Invalid file extention"
							ForeColor="Red" Display="Dynamic" ClientValidationFunction="ValidateImage_Client"></asp:CustomValidator>

                        <asp:RequiredFieldValidator ID="rfvImageUpload" ControlToValidate="fileImage" runat="server"
							ErrorMessage="A .jpg image is required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
					</div>
				<div class="form-group">
					<label for="passwordconfirm">Condition*</label><br />
					<asp:DropDownList ID="ddlCondition" runat="server">
						<asp:ListItem Value="excellent">Excellent</asp:ListItem>
						<asp:ListItem Value="good">Good</asp:ListItem>
						<asp:ListItem Value="fair">Fair</asp:ListItem>
						<asp:ListItem Value="poor">Poor</asp:ListItem>
					</asp:DropDownList>
				</div>
				
				</div>
				<div class="form-group"><label for="description">description of pattern/contents*</label><br />
					<asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvDescription" ErrorMessage="Description is required" ControlToValidate="txtDescription" runat="server" Display="Dynamic"
						ForeColor="Red" />
				</div>
	
					<div class="form-group">
						<label for="swap-options">Contact Options:*</label>
						<asp:CheckBox ID="pickup" runat="server" Text="Pick Up"/>
						<asp:CheckBox ID="delivery" runat="server" Text="Delivery" /><br />
						<asp:CustomValidator ID="cvContactOptions" ErrorMessage="At least one contact option must be selected" runat="server"
							ClientValidationFunction="ValidateCheckboxes_Client" Display="Dynamic" ForeColor="Red"/>

					</div>
				
					<div>
						<asp:Button ID="btnAddProduct" runat="server" Text="Add Item" CssClass="list-button"/>
					</div>
					
			</form>


		</section>

		<script type="text/javascript">
			function ValidateImage_Client(sender, args) {
				args.IsValid = false;

				var fileName = args.Value;
                args.IsValid = (fileName.endsWith('.jpg'))
			}

            function ValidateCheckboxes_Client(sender, args) {
				args.IsValid = false;

                var Checkbox1 = document.getElementById("BodyContentPlaceHolder_pickup");
                var Checkbox2 = document.getElementById("BodyContentPlaceHolder_delivery");

				if (Checkbox1.checked == true || Checkbox2.checked == true) {
					args.IsValid = true;
                }

            }
		</script>

    </div>
</asp:Content>
