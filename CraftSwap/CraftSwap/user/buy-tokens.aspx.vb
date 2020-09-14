Imports System.Data
Imports System.Data.SqlClient

Public Class buy_tokens
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlUpdateTokens As New SqlCommand("UPDATE Users SET Tokens = (Tokens + 5) WHERE UserID = @UserID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlUpdateTokens.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
    End Sub

    Protected Sub btnBuyTokens_Click(sender As Object, e As EventArgs) Handles btnBuyTokens.Click
        sqlUpdateTokens.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlCon.Open()
        sqlUpdateTokens.ExecuteNonQuery()
        sqlCon.Close()
    End Sub
End Class