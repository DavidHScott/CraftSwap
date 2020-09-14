<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="buy-tokens.aspx.vb" Inherits="CraftSwap.buy_tokens" MasterPageFile="~/Site.Master" %>

<asp:Content ID="AboutUsContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
	<div class="body-content">
		<section class="signup center">

			<div class="left-column">
				<h1>Purchase Tokens</h1>

				<p>Short on tokens? <br>Purchase 5 tokens for $10. <br>Tokens are non-refundable and have no monetary value outside of this site.</p>
				<br>
				<p>You currently have:<br>
				0<br>
				CraftSwap Tokens</p>
				<br>
				<table class="shop-table">

					<thead>
						<tr>
							<th class="product">Product</th>
							<th class="product">Quantity</th>
							<th class="product">Price</th>
				
						</tr>
					</thead>
					<tbody>
						<tr class="cart_item">

							<td class="product">CraftSwap Tokens</td>

							<td class="product">5</td>

							<td class="product">$10.00</td>
						</tr>
					</tbody>
				</table>
			</div>
			<div class="right-column">
				<form runat="server">

					<div class="form-group">
						<label for="buyname" class="payment-label">Name</label>
						<asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" ErrorMessage="Name is required" ControlToValidate="txtName" runat="server" Display="Dynamic" ForeColor="Red" />
					</div>

					<div class="form-group">
						<label for="email-address" class="payment-label">Address</label>
						<asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvAddress" ErrorMessage="Address is required" ControlToValidate="txtAddress" runat="server" Display="Dynamic" ForeColor="Red" />
					</div>
					<div class="form-group">
						<label for="municipality" class="payment-label">City/Town</label>
						<asp:TextBox ID="txtMunicipality" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvMunicipality" ErrorMessage="Municipality is required" ControlToValidate="txtMunicipality" runat="server" Display="Dynamic" ForeColor="Red" />
					</div>
					<div class="form-group">
						<label for="postcode" class="payment-label">Postal Code</label>
						<asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvPostcode" ErrorMessage="Postal Code is required" ControlToValidate="txtPostcode" runat="server" Display="Dynamic"
							ForeColor="Red" /><br />
						<asp:RegularExpressionValidator ID="revPostcode" ErrorMessage="Not a valid Canadian postal code" ControlToValidate="txtPostcode" ValidationExpression="([A-Z]\d){3}"
							runat="server" Display="Dynamic" ForeColor="Red" />
					</div>
					<div class="form-group">
						<label for="cardnum" class="payment-label">Credit Card Number</label>
						<asp:TextBox ID="txtCardNum" runat="server"></asp:TextBox><br />
                        <asp:CustomValidator ID="cuvCardNum" ErrorMessage="Not a valid credit card number" ControlToValidate="txtCardNum"
							ClientValidationFunction="ValidateCreditCardNumber_Client" runat="server" Display="Dynamic" ForeColor="Red" /><br />
						<asp:RequiredFieldValidator ID="rfvCardNum" ErrorMessage="Credit card number is required" ControlToValidate="txtCardNum" runat="server" Display="Dynamic"
							ForeColor="Red" />
						

					</div>
					<div class="form-group">
						<label for="cardnum" class="payment-label">Expiry Date</label>
						<asp:TextBox ID="txtExpiry" runat="server" placeholder="MM/YY"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvExpiry" ErrorMessage="Credit card expiry date is required" ControlToValidate="txtExpiry" runat="server" Display="Dynamic"
							ForeColor="Red" /><br />
						<asp:RegularExpressionValidator ID="revExpiry" ErrorMessage="Not a valid expiry date" ControlToValidate="txtExpiry"
							ValidationExpression="^((0[1-9])|(1[0-2]))\/(\d{2})$" runat="server" Display="Dynamic" ForeColor="Red" />
					</div>
					<div class="form-group">
						<label for="cardnum" class="payment-label">CVV</label>
						<asp:TextBox ID="txtCVV" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvCVV" ErrorMessage="Credit card CCV is required" ControlToValidate="txtCVV" runat="server" Display="Dynamic" ForeColor="Red" />
						<asp:RegularExpressionValidator ID="regCVV" ErrorMessage="Not a valid CVV" ControlToValidate="txtCVV" ValidationExpression="/^\d{3}$/" runat="server"
							Display="Dynamic" ForeColor="Red" />
					</div>

					<div class="form-group ">
						<div class="gdpr_text">By submitting this order you agree to our <a href="Privacy-Policy.aspx">Privacy Policy</a>. </div><br />


						<div>
							<asp:Button ID="btnBuyTokens" Text="Purchase Tokens" CssClass="list-button" runat="server" />
						</div>
					</div>

				</form>

				<script type="text/javascript">
                    function ValidateCreditCardNumber_Client(sender, args) {
                        args.IsValid = false;

                        var cardNum = args.Value;
                        var cardLength = cardNum.length;

                        var nCheck = 0
                        var bEven = false;

                        for (var i = cardLength - 1; i >= 0; i--) {
                            var digit = cardNum.charAt(i)
                            var nDigit = parseInt(digit, 10);

                            if (bEven && (nDigit *= 2) > 9) nDigit -= 9;

                            nCheck += nDigit;
                            bEven = !bEven;
                        }

                        if ((nCheck % 10) == 0) {
                            args.IsValid = true
                        }
                    }
				</script>

			</div>
		</section>
	</div>
</asp:Content>
