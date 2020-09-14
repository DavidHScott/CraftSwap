Imports System.Data
Imports System.Data.SqlClient

Public Class Cancel_Swap
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlCancelSwap As New SqlCommand("UPDATE Products SET Avail = 'Available' where ProductID=@ProductID AND Avail like 'Pending'", sqlCon)
    Dim sqlGetLog As New SqlCommand("SELECT * from SalesLog Where ProductID=@ProductID AND Status like 'Pending'", sqlCon)
    Dim sqlReturnToken As New SqlCommand("UPDATE Users SET Tokens = (Tokens + 1), IncompleteSwaps = (IncompleteSwaps + 1) Where UserID=@UserID", sqlCon)
    Dim sqlRemoveLog As New SqlCommand("Delete From SalesLog where ID=@ID", sqlCon)

    Dim productID = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlCancelSwap.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
        sqlGetLog.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
        sqlReturnToken.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlRemoveLog.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int))

        productID = Request.QueryString("prod")

        sqlCancelSwap.Parameters("@ProductID").Value = productID
        sqlGetLog.Parameters("@ProductID").Value = productID

        Dim reader As SqlDataReader

        sqlCon.Open()

        reader = sqlGetLog.ExecuteReader()

        While reader.Read
            sqlReturnToken.Parameters("@UserID").Value = reader.GetInt32(0)
            sqlRemoveLog.Parameters("@ID").Value = reader.GetInt32(4)
        End While

        reader.Close()


        sqlCancelSwap.ExecuteNonQuery()
        sqlReturnToken.ExecuteNonQuery()
        sqlRemoveLog.ExecuteNonQuery()

        sqlCon.Close()

        Response.Redirect("Profile.aspx")

    End Sub

End Class