<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit-Profile.aspx.vb" Inherits="CraftSwap.Edit_Profile" MasterPageFile="~/Site.Master"%>

<asp:Content ID="AboutUsContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
		<section class="center">

			<h1>Welcome to your profile, <% Response.Write(ReturnUsername()) %></h1>
			<form runat="server">
				<div class="left-column">
				
					<p>In order to facilitate successful swaps, you will need to offer a pick-up and/or drop-off address and instructions.
						Addresses will not be shown on the site, but will be emailed to members who choose to swap with you.</p>
					<label for="pickup">Pickup Address</label><br />
					<asp:TextBox ID="txtPickupAddress" TextMode="MultiLine" runat="server"></asp:TextBox><br>
					<label for="dropoff">Delivery Address</label><br />
					<asp:TextBox ID="txtDropoffAddress" TextMode="MultiLine" runat="server"></asp:TextBox>

				</div>
				<div class="right-column">
					<div class="form-group">
						<label for="username">Name</label><br />
						<asp:TextBox ID="txtNewUsername" runat="server"></asp:TextBox>
					</div>

					<div class="form-group">
						<label for="email-address">Email Address</label><br />
						<asp:TextBox ID="txtNewEmail" runat="server"></asp:TextBox><br />
						<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="This is not a valid email address" ControlToValidate="txtNewEmail"
							ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
							runat="server" Display="Dynamic" ForeColor="Red"/>
					</div>

					<br />

					<div class="box">
						<p>New Password</p>
						<div class="form-group">
							<label for="password">Current Password</label><br />
							<asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server"></asp:TextBox>
						</div>
						<div class="form-group">
							<label for="password">New Password </label><br />
							<asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="form-group">

						<div class="checkbox-regular">
							<asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes"/><br>
							OR<br>
							<a href="Delete-User.aspx">Delete Profile</a>
						</div>

					</div>

				
				</div>

			</form>
		</section>
	</div>
</asp:Content>