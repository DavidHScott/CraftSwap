Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True; MultipleActiveResultSets=True;")
    Dim sqlGetPwdCommand As New SqlCommand("Select Password from Users where Email like @Email", sqlCon)
    Dim sqlGetUsernameCommand As New SqlCommand("Select UserID from Users where Email like @Email", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.Page.User.Identity.IsAuthenticated Then
            FormsAuthentication.SignOut()
        End If
    End Sub

    Function ValidateUser(UsersEmail As String, usersPassword As String) As Boolean

        Dim pass$ = Nothing

        Try
            sqlCon.Open()

            sqlGetPwdCommand.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))
            sqlGetPwdCommand.Parameters("@Email").Value = UsersEmail

            pass = sqlGetPwdCommand.ExecuteScalar()

            sqlCon.Close()
        Catch ex As Exception
            SendError()
        End Try

        Return (String.Compare(pass, usersPassword, False) = 0)

    End Function

    Protected Sub btnSubmitForm_Click(sender As Object, e As EventArgs) Handles btnSubmitForm.Click
        If ValidateUser(txtEmail.Text, txtPass.Text) Then
            Dim username$

            sqlCon.Open()
            sqlGetUsernameCommand.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))
            sqlGetUsernameCommand.Parameters("@Email").Value = txtEmail.Text
            username = sqlGetUsernameCommand.ExecuteScalar()
            sqlCon.Close()

            FormsAuthentication.RedirectFromLoginPage(username, True)
        Else
            SendError()
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Function SendError()
    End Function

End Class