Imports System.Web.Security

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.Compare(Request.Path.ToString, "/Login.aspx") Then
            If Me.Page.User.Identity.IsAuthenticated Then
                btnLogin.Text = "Sign out"
            Else
                btnLogin.Text = "Login/Sign up"
            End If
        Else
            btnLogin.Visible = False
        End If
    End Sub

    Protected Sub BtnSignOut_Click()
        FormsAuthentication.SignOut()
    End Sub

End Class