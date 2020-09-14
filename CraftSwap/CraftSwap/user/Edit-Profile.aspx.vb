Imports System.Data
Imports System.Data.SqlClient

Public Class Edit_Profile
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlUpdatePickup As New SqlCommand("UPDATE Users SET Pickup = @Pickup WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateDropoff As New SqlCommand("UPDATE Users SET Dropoff = @Dropoff WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateUsername As New SqlCommand("UPDATE Users SET Username = @Username WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateEmail As New SqlCommand("UPDATE Users SET Email = @Email WHERE UserID = @UserID", sqlCon)

    Dim sqlUpdatePassword As New SqlCommand("UPDATE Users SET Password = @Password WHERE UserID = @UserID AND Password like @OldPassword", sqlCon)

    Dim sqlGetUsername As New SqlCommand("SELECT Username FROM Users WHERE UserID = @UserID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlUpdatePickup.Parameters.Add(New SqlParameter("@Pickup", SqlDbType.Text))
        sqlUpdatePickup.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlUpdateDropoff.Parameters.Add(New SqlParameter("@Dropoff", SqlDbType.Text))
        sqlUpdateDropoff.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlUpdateUsername.Parameters.Add(New SqlParameter("@Username", SqlDbType.Text))
        sqlUpdateUsername.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlUpdateEmail.Parameters.Add(New SqlParameter("@Email", SqlDbType.Text))
        sqlUpdateEmail.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlUpdatePassword.Parameters.Add(New SqlParameter("@Password", SqlDbType.Text))
        sqlUpdatePassword.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlUpdatePassword.Parameters.Add(New SqlParameter("@OldPassword", SqlDbType.Text))

        sqlGetUsername.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
    End Sub

    Function ReturnUsername()
        sqlCon.Open()

        sqlGetUsername.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        Dim username = sqlGetUsername.ExecuteScalar()

        sqlCon.Close()

        Return username
    End Function

    Protected Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click

        sqlCon.Open()

        'Change Addresses
        If txtPickupAddress.Text IsNot "" Then
            sqlUpdatePickup.Parameters("@Pickup").Value = txtPickupAddress.Text
            sqlUpdatePickup.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlUpdatePickup.ExecuteNonQuery()
        End If

        If txtDropoffAddress.Text IsNot "" Then
            sqlUpdateDropoff.Parameters("@Dropoff").Value = txtDropoffAddress.Text
            sqlUpdateDropoff.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlUpdateDropoff.ExecuteNonQuery()
        End If

        'Change Username
        If txtNewUsername.Text IsNot "" Then
            sqlUpdateUsername.Parameters("@Username").Value = txtNewUsername.Text
            sqlUpdateUsername.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlUpdateUsername.ExecuteNonQuery()
        End If

        'Change Email
        If txtNewEmail.Text IsNot "" Then
            sqlUpdateEmail.Parameters("@Email").Value = txtNewEmail.Text
            sqlUpdateEmail.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlUpdateEmail.ExecuteNonQuery()
        End If

        'Change Password
        If txtNewPassword.Text IsNot "" And txtOldPassword.Text IsNot "" Then
            sqlUpdatePassword.Parameters("@Password").Value = txtNewPassword.Text
            sqlUpdatePassword.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
            sqlUpdatePassword.Parameters("@OldPassword").Value = txtOldPassword.Text

            sqlUpdatePassword.ExecuteNonQuery()
        End If

        sqlCon.Close()

        Response.Redirect("Profile.aspx")
    End Sub
End Class