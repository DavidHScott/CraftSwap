Imports System.Data
Imports System.Data.SqlClient

Public Class Profile
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")

    Dim sqlGetAvailableProd As New SqlCommand("Select * from Products where UserID = @UserID And Avail like 'Available'", sqlCon)
    Dim sqlGetPendingProd As New SqlCommand("Select * from Products where UserID = @UserID And Avail like 'Pending'", sqlCon)
    Dim sqlGetSwappedProd As New SqlCommand("Select Products.*, SalesLog.* from Products, SalesLog where Products.UserID =
                                            @UserID And Products.Avail like 'Swapped' And Products.ProductID = SalesLog.ProductID", sqlCon)

    Dim sqlGetUser As New SqlCommand("Select * from Users where UserID like @UserID", sqlCon)
    Dim sqlGetUsername As New SqlCommand("SELECT Username FROM Users WHERE UserID = @UserID", sqlCon)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlGetAvailableProd.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlGetPendingProd.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlGetSwappedProd.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        sqlGetUser.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlGetUser.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        sqlGetUsername.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
    End Sub

    Function ReturnUsername()
        sqlCon.Open()

        sqlGetUsername.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        Dim username = sqlGetUsername.ExecuteScalar()

        sqlCon.Close()

        Return username
    End Function

    Function PopulateLeftCol()
        Dim content = "<p>Pick-Up Address: "

        sqlCon.Open()

        Dim reader As SqlDataReader
        reader = sqlGetUser.ExecuteReader()

        While reader.Read()
            content += reader.GetValue(9) & "</p><p>Drop Off Address: " & reader.GetValue(10) & "</p>"
        End While

        reader.Close()
        sqlCon.Close()

        Return content
    End Function

    Function PopulateRightCol()
        Dim content = "<div class='form-group'><p>Email: "

        sqlCon.Open()

        Dim reader As SqlDataReader
        reader = sqlGetUser.ExecuteReader()

        While reader.Read()
            content += reader.GetString(5) & "</p></div><br><br><div class='form-group'>
					<p>You currently have " & reader.GetInt32(6) & " tokens available to swap</p><p>You have made " &
                    reader.GetInt32(3) & " successful swaps</p><p>You have " & reader.GetInt32(4) & " swaps marked as unsuccessful</p></div>"
        End While

        reader.Close()
        sqlCon.Close()

        Return content

    End Function

    Function PopulateSiteHistory()

        Dim content = "<h3>Uploaded Patterns</h3>
			<div class='query-list profile'>"

        sqlCon.Open()
        Dim reader As SqlDataReader

        sqlGetAvailableProd.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlGetPendingProd.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)
        sqlGetSwappedProd.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

        reader = sqlGetAvailableProd.ExecuteReader()

        While reader.Read()
            content += "<div class='list-item'>
					<div class='list-img'><img src='../img/plan_" & reader.GetInt32(0) & ".jpg' alt='" + reader.GetString(1) + "' class='micro'></div>
					<div class='list-description'>
						<p>Condition: " + reader.GetString(10) + "<br>Availability: " + reader.GetString(6) + "</p>
					</div>
				<div class='list-details'><a href='Delete-Product.aspx?prod=" & reader.GetInt32(0) & "'>Remove Item</a><br></div>
				</div>"
        End While

        reader.Close()

        content += "</div>
			
			<h3>Pending Swaps</h3>

			<div class='query-list profile'>"

        reader = sqlGetPendingProd.ExecuteReader()

        While reader.Read()

            Dim button1 As Button = New Button

            button1.Visible = True
            button1.CssClass = "list-button"
            button1.ClientIDMode = ClientIDMode.Static


            content += "<div class='list-item'>
					<div class='list-img'><img src='../img/plan_" & reader.GetInt32(0) & ".jpg' alt='" + reader.GetString(1) + "' class='micro'></div>
					<div class='list-description'>
                        <p>Condition: " + reader.GetString(10) + "<br>Availability: " + reader.GetString(6) + "</p>
                    </div>
					<div class='list-details'>
                    <a href='Complete-Swap.aspx?prod=" & reader.GetInt32(0) & "' class='list-button'>Complete Swap</a>
                    <a href='Cancel-Swap.aspx?prod=" & reader.GetInt32(0) & "' class='list-button'>Cancel Swap</a></div>
				</div>"
        End While

        reader.Close()

        content += "</div>
			
			<h3>Completed Swaps</h3>

			<div class='query-list profile'>"

        reader = sqlGetSwappedProd.ExecuteReader()

        While reader.Read()

            content += "<div class='list-item'>
					<div class='list-img'><img src='../img/plan_" & reader.GetInt32(0) & ".jpg' alt='" + reader.GetString(1) + "' class='micro'></div>
					<div class='list-description'>
						<p>Swap date: " & reader.GetDateTime(14).ToString("yyyy-MM-dd HH:mm:ss") & "</p>
					</div>
				
				</div>"
        End While

        content += "</div>"

        reader.Close()
        sqlCon.Close()

        Return content

    End Function


End Class