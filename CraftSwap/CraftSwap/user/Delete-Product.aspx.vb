Imports System.Data
Imports System.Data.SqlClient

Public Class Delete_Product
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")
    Dim sqlDeleteProduct As New SqlCommand("UPDATE Products SET Avail = 'deleted' where ProductID=@ProductID AND Avail like 'Available'", sqlCon)

    Dim productID = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlDeleteProduct.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))

        productID = Request.QueryString("prod")

        sqlDeleteProduct.Parameters("@ProductID").Value = productID

        sqlCon.Open()
        sqlDeleteProduct.ExecuteNonQuery()
        sqlCon.Close()

        Response.Redirect("Profile.aspx")

    End Sub

End Class