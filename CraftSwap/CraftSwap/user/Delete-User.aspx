<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Delete-User.aspx.vb" Inherits="CraftSwap.Delete_User" MasterPageFile="~/Site.Master" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="BodyContentPlaceHolder" Runat="Server">
    <div class="body-content">
        <section class="center">
        <form runat="server">
            <div class="form-group">
                <label for="delete">Are you sure you would like to delete your account?</label><br />
                <asp:Button ID="btnDeleteAccount" Text="I am sure" runat="server" CssClass="list-button"/>
            </div>
        </form>
            </section>
    </div>
</asp:Content>