Imports System.Data
Imports System.Data.SqlClient

Public Class WebForm2
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlGetEmail As New SqlCommand("SELECT 1 from Users WHERE Email like @Email", sqlCon)
    Dim sqlGetUserID As New SqlCommand("SELECT UserID from Users WHERE Email like @Email", sqlCon)
    Dim sqlNewUser As New SqlCommand("INSERT into Users (Username, UploadedPlans, SuccessfulSwaps, IncompleteSwaps, Email, Tokens, TokensTenths, Password, Dropoff)
                            VALUES (@Username, 0, 0, 0, @Email, 0, 0, @Password, @Dropoff)", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlGetEmail.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))

        sqlNewUser.Parameters.Add(New SqlParameter("@Username", SqlDbType.Text))
        sqlNewUser.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))
        sqlNewUser.Parameters.Add(New SqlParameter("@Password", SqlDbType.Text))
        sqlNewUser.Parameters.Add(New SqlParameter("@Dropoff", SqlDbType.Text))
    End Sub

    Protected Sub btnSignup_Click(sender As Object, e As EventArgs) Handles btnSignup.Click
        sqlCon.Open()

        Debug.WriteLine(txtEmail.Text)

        sqlGetEmail.Parameters("@Email").Value = txtEmail.Text
        Dim getEmail = sqlGetEmail.ExecuteScalar()

        If getEmail IsNot Nothing Then
            MsgBox("There is an account already using this email address.")

            Return
        Else
            sqlNewUser.Parameters("@Username").Value = txtUsername.Text
            sqlNewUser.Parameters("@Email").Value = txtEmail.Text
            sqlNewUser.Parameters("@Password").Value = txtPassword.Text
            sqlNewUser.Parameters("@Dropoff").Value = txtAddress.Text

            sqlNewUser.ExecuteNonQuery()

            'Keep the user logged in

            sqlGetUserID.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))
            sqlGetUserID.Parameters("@Email").Value = txtEmail.Text

            Dim userID = sqlGetUserID.ExecuteScalar()

            FormsAuthentication.SetAuthCookie(userID, True)

            Response.Redirect("user/Profile.aspx")
        End If

    End Sub
End Class