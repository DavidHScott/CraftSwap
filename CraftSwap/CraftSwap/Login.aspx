<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="CraftSwap.Login"  MasterPageFile="~/Site.Master"%>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
        <section class="signup center">

            <h1>Login</h1>

            <form runat="server">

				<div class="auto-style1">
					<label for="email-address">Email Address
                    </label><br/>
					<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="Email is required" ControlToValidate="txtEmail" runat="server" Display="Dynamic" ForeColor="Red" /><br />
					<asp:RegularExpressionValidator ID="revEmail" ErrorMessage="This is not a valid email address" ControlToValidate="txtEmail"
							ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
							runat="server" Display="Dynamic" ForeColor="Red"/>
                    
				</div>
				<div class="form-group">
					<label for="password">Password</label><br/>
					
					<asp:TextBox ID="txtPass" TextMode="Password" runat="server"></asp:TextBox><br />
					<asp:RequiredFieldValidator ID="rfvPassword" ErrorMessage="Password is required" ControlToValidate="txtPass" runat="server" Display="Dynamic" ForeColor="Red" />
                    
				</div>


				<div class="form-group ">
					<asp:Button ID="btnSubmitForm" Text="Login" runat="server" CssClass="list-button" />
				</div>
			</form>

			<div class="gdpr_text">Don't already have an account? <a href="new-user.aspx">Sign Up</a></div>

        </section>
    </div>
</asp:Content>

