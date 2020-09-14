Imports System.Data
Imports System.Data.SqlClient

Public Class Complete_Swap
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")
    Dim sqlCompleteSwap As New SqlCommand("UPDATE Products SET Avail = 'Swapped' where ProductID=@ProductID AND Avail like 'Pending'", sqlCon)
    Dim sqlUpdateLog As New SqlCommand("UPDATE SalesLog SET Status = 'Swapped', Time = @NewTime where ProductID=@ProductID AND Status like 'Pending'", sqlCon)
    Dim sqlGiveUserToken As New SqlCommand("UPDATE Users SET Tokens = (Tokens + 1), SuccessfulSwaps = (SuccessfulSwaps + 1) Where UserID = @UserID", sqlCon)

    Dim productID = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlCompleteSwap.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
        sqlGiveUserToken.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlUpdateLog.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
        sqlUpdateLog.Parameters.Add(New SqlParameter("@NewTime", SqlDbType.DateTime))

        productID = Request.QueryString("prod")

        sqlCompleteSwap.Parameters("@ProductID").Value = productID
        sqlUpdateLog.Parameters("@ProductID").Value = productID
        sqlUpdateLog.Parameters("@NewTime").Value = DateTime.Now
        sqlGiveUserToken.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlCon.Open()

        sqlCompleteSwap.ExecuteNonQuery()
        sqlUpdateLog.ExecuteNonQuery()
        sqlGiveUserToken.ExecuteNonQuery()

        sqlCon.Close()

        Response.Redirect("Profile.aspx")
    End Sub

End Class