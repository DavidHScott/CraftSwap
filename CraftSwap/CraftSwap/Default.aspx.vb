Imports System.Data
Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlFromProdCommandWhereCategory As New SqlCommand("Select * from Products Where CategoryID like @CategoryID And Avail like 'Available' Order By ProductID
            desc Offset 0 rows Fetch first 15 rows only", sqlCon)
    Dim sqlFromProdCommand As New SqlCommand("Select * from Products Where Avail like 'Available' Order By ProductID desc Offset 0 rows Fetch first 15 rows only", sqlCon)
    Dim sqlGetCategory As New SqlCommand("SELECT CategoryName FROM Categories WHERE CategoryID like @CategoryID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlFromProdCommandWhereCategory.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
        sqlGetCategory.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
    End Sub

    Function PopulateRecentContent()

        Dim content = ""

        sqlCon.Open()
        Dim reader As SqlDataReader

        reader = sqlFromProdCommand.ExecuteReader()

        While reader.Read()
            content += "<div class='plan-thumb'>
					<a href='Product.aspx?prod=" & reader.GetInt32(0) & "' class='card'>
						<div>
							<img src='img/plan_" & reader.GetInt32(0) & ".jpg' alt='" & reader.GetString(1) & "' class='thumbnail'/>

						</div>
					</a>
				</div>"
        End While

        reader.Close()
        sqlCon.Close()

        Return content

    End Function

    Function PopulateCategoryContent(categoryID As String)

        sqlGetCategory.Parameters("@CategoryID").Value = categoryID
        sqlFromProdCommandWhereCategory.Parameters("@CategoryID").Value = categoryID

        sqlCon.Open()

        Dim count = 0
        Dim content = "<section class='category-wrap'>
			<h2>" & sqlGetCategory.ExecuteScalar() & " Plans</h2><div class='horizontal-wrapper'>"


        Dim reader As SqlDataReader

        reader = sqlFromProdCommandWhereCategory.ExecuteReader()

        While reader.Read()
            content += "<div class='plan-thumb'>
					<a href='Product.aspx?prod=" & reader.GetInt32(0) & "' class='card'>
						<div>
							<img src='img/plan_" & reader.GetInt32(0) & ".jpg' alt='" & reader.GetString(1) & "' class='thumbnail'/>

						</div>
					</a>
				</div>"

            count += 1
        End While

        reader.Close()
        sqlCon.Close()

        content += "</div></section>"

        If count > 0 Then
            Return content
        Else
            Return ""
        End If

    End Function

End Class