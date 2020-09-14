Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class Add_Product
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlInsertNewProduct As New SqlCommand("INSERT into Products (Name, CategoryID, UserID, Author, Publisher, SwapOptions, Description, Pages, Publication, Condition, Avail)
                                    VALUES (@Name, @CategoryID, @UserID, @Author, @Publisher, @SwapOptions, @Description, @Pages, @Publication, @Condition, 'Available')", sqlCon)

    Dim sqlSelectRecentProduct As New SqlCommand("SELECT ProductID FROM Products WHERE UserID = @UserID Order By ProductID desc", sqlCon)

    Dim sqlUpdateUploadedPlans As New SqlCommand("UPDATE Users SET UploadedPlans = (UploadedPlans + 1) WHERE UserID = @UserID", sqlCon)
    Dim sqlUpdateTokenTenths As New SqlCommand("UPDATE Users SET TokensTenths = (TokensTenths + 1) WHERE UserID = @UserID", sqlCon)

    Dim sqlGetTokenTenths As New SqlCommand("SELECT TokensTenths FROM Users WHERE UserID = @UserID", sqlCon)
    Dim sqlIncrementToken As New SqlCommand("UPDATE Users SET Tokens = (Tokens + 1), TokensTenths = 0 WHERE UserID = @UserID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Name", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Author", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Publisher", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@SwapOptions", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Description", SqlDbType.Text))
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Pages", SqlDbType.Int))
        sqlInsertNewProduct.Parameters("@Pages").IsNullable = True
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Publication", SqlDbType.NChar))
        sqlInsertNewProduct.Parameters("@Publication").IsNullable = True
        sqlInsertNewProduct.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Text))

        sqlSelectRecentProduct.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlUpdateUploadedPlans.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlUpdateTokenTenths.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlGetTokenTenths.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlIncrementToken.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

    End Sub

    Function PopulateProgressBar()
        sqlGetTokenTenths.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        Dim content = ""

        sqlCon.Open()
        Dim tokenTenths = sqlGetTokenTenths.ExecuteScalar()
        sqlCon.Close()

        content += "<div class='progress'><p>Add another " & 10 - tokenTenths & " patterns to get a token to spend!</p><progress id='tokens' value='" & tokenTenths &
            "' max='10'> </progress></div>"

        Return content

    End Function

    Protected Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click
        sqlInsertNewProduct.Parameters("@Name").Value = txtTitle.Text
        sqlInsertNewProduct.Parameters("@CategoryID").Value = ddlCategory.SelectedValue
        sqlInsertNewProduct.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlInsertNewProduct.Parameters("@Author").Value = txtAuthor.Text
        sqlInsertNewProduct.Parameters("@Publisher").Value = txtPublisher.Text
        sqlInsertNewProduct.Parameters("@Description").Value = txtDescription.Text
        sqlInsertNewProduct.Parameters("@Condition").Value = ddlCondition.SelectedValue

        Dim options = ""

        If pickup.Checked = True Then
            options += "Pickup"
            If delivery.Checked = True Then
                options += ", Delivery"
            End If
        Else
            options += "Delivery"
        End If

        sqlInsertNewProduct.Parameters("@SwapOptions").Value = options


        If txtPages.Text IsNot "" Then
            sqlInsertNewProduct.Parameters("@Pages").Value = Integer.Parse(txtPages.Text)
        Else
            sqlInsertNewProduct.Parameters("@Pages").Value = DBNull.Value
        End If

        Dim publication = Nothing

        If txtPublication.Text IsNot "" Then
            sqlInsertNewProduct.Parameters("@Publication").Value = txtPublication.Text
        Else
            sqlInsertNewProduct.Parameters("@Publication").Value = DBNull.Value
        End If


        sqlCon.Open()

        sqlInsertNewProduct.ExecuteNonQuery()


        sqlSelectRecentProduct.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        Dim newProductID = sqlSelectRecentProduct.ExecuteScalar()

        Dim fileName = Path.Combine(Server.MapPath("~/img"), "plan_" & newProductID & ".jpg")
        fileImage.SaveAs(fileName)


        sqlUpdateUploadedPlans.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlUpdateTokenTenths.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlGetTokenTenths.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlIncrementToken.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlUpdateUploadedPlans.ExecuteNonQuery()
        sqlUpdateTokenTenths.ExecuteNonQuery()

        If sqlGetTokenTenths.ExecuteScalar() = 10 Then
            sqlIncrementToken.ExecuteNonQuery()
        End If

        sqlCon.Close()

        Response.Redirect("~/Product.aspx?prod=" & newProductID)

    End Sub
End Class