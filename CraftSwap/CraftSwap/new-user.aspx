<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="new-user.aspx.vb" Inherits="CraftSwap.WebForm2" MasterPageFile="~/Site.Master" %>

<asp:Content ID="AboutUsContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
		<section class="signup center">

			<h1>Create an Account</h1>
			<form id="SignupForm" runat="server">
				<div class="form-group">
					<label for="username">Username</label><br/>
					<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvUsername" ErrorMessage="Username is required" ControlToValidate="txtUsername" runat="server" Display="Dynamic" ForeColor="Red" />
				</div>

				<div class="form-group">
					<label for="email-address">Email Address</label><br/>
					<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="Email is required" ControlToValidate="txtEmail" runat="server" Display="Dynamic" ForeColor="Red" /><br />
					<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="This is not a valid email address" ControlToValidate="txtEmail"
							ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
							runat="server" Display="Dynamic" ForeColor="Red"/>
				</div>

				<div class="form-group">
					<label for="password">Password</label><br/>
					<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvPassword" ErrorMessage="Password is required" ControlToValidate="txtPassword" runat="server" Display="Dynamic" ForeColor="Red" />

				</div>
				<div class="form-group">
					<label for="passwordconfirm">Confirm password</label><br/>
					<asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvConfirmPassword" ErrorMessage="Password confirmation is required" ControlToValidate="txtConfirmPassword" runat="server"
						Display="Dynamic" ForeColor="Red" />
					<asp:CompareValidator ID="cvPasswords" ErrorMessage="Please confirm your password correctly" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
						Operator="Equal" runat="server" Display="Dynamic" ForeColor="Red" />
				</div>

				<div class="form-group">
					<label for="address">Address</label><br />
					<asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvAddress" ErrorMessage="Address is required" ControlToValidate="txtAddress" runat="server" Display="Dynamic" ForeColor="Red" />
				</div>

				<div class="form-group ">
					<div class="gdpr_text">By signing up you agree to our <a href="Privacy-Policy.aspx">Privacy Policy</a>. </div><br/>


					<div>
						<asp:Button ID="btnSignup" runat="server" Text="Agree & Sign up" CssClass="list-button" />
					</div>
				</div>

			</form>
			<div class="gdpr_text">Already have an account? <a href="Login.aspx">Login</a></div>


		</section>
	</div>
</asp:Content>