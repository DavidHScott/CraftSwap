Imports System.Data
Imports System.Data.SqlClient

Public Class Delete_User
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlDeleteUser As New SqlCommand("DELETE FROM Users WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateSalesLog As New SqlCommand("UPDATE SalesLog SET UserID = 6 WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateProducts As New SqlCommand("UPDATE Products SET UserID = 6, Avail = 'deleted' WHERE UserID = @UserID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlDeleteUser.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlUpdateSalesLog.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlUpdateProducts.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

    End Sub

    Protected Sub btnDeleteAccount_Click(sender As Object, e As EventArgs) Handles btnDeleteAccount.Click
        sqlDeleteUser.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlUpdateSalesLog.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlUpdateProducts.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlCon.Open()

        sqlUpdateProducts.ExecuteNonQuery()
        sqlUpdateSalesLog.ExecuteNonQuery()
        sqlDeleteUser.ExecuteNonQuery()

        sqlCon.Close()

        FormsAuthentication.SignOut()
        Response.Redirect("~/Default.aspx")
    End Sub
End Class