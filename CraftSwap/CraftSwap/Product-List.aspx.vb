Imports System.Data
Imports System.Data.SqlClient

Public Class Category
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True; MultipleActiveResultSets=True;")

    Dim sqlFromUserCommand As New SqlCommand("Select * from Users Where UserID=@UserID", sqlCon)
    Dim sqlFromCategoryCommand As New SqlCommand("Select * from Categories Where CategoryID like @CategoryID", sqlCon)

    Dim listType$ = ""
    Dim listTitleName$ = ""
    Dim categoryID$ = ""
    Dim userID = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Set up the SQL connection
        sqlCon.Open()
        Dim reader As SqlDataReader

        'Get the encoded string from the URL
        listType = Request.QueryString("list")

        'Get the type of list (category or user)
        If listType = "cat" Then
            categoryID = Request.QueryString("catid")

            sqlFromCategoryCommand.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
            sqlFromCategoryCommand.Parameters("@CategoryID").Value = categoryID

            reader = sqlFromCategoryCommand.ExecuteReader()
        ElseIf listType = "user" Then
            userID = Request.QueryString("userid")

            sqlFromUserCommand.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
            sqlFromUserCommand.Parameters("@UserID").Value = userID

            reader = sqlFromUserCommand.ExecuteReader()
        End If

        While reader.Read()
            listTitleName = reader.GetString(1)
        End While

        reader.Close()
        sqlCon.Close()

        UpdateSitePath()
    End Sub

    'Set the list header to the category or the username the user is trying to access
    Function GetListTypeHeading$()
        Dim listHeading$ = "<h2>" & listTitleName & "</h2>"

        Return listHeading
    End Function

    'Populate the list with projects
    Function PopulatePage$()
        Dim bodyContent$ = ""
        Dim count = 0

        sqlCon.Open()
        Dim reader As SqlDataReader

        If listType = "cat" Then
            Dim sqlFromProdCommend As New SqlCommand("Select * from Products Where CategoryID like @CategoryID And Avail like'Available'", sqlCon)
            sqlFromProdCommend.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
            sqlFromProdCommend.Parameters("@CategoryID").Value = categoryID

            reader = sqlFromProdCommend.ExecuteReader()
        ElseIf listType = "user" Then
            Dim sqlFromProdCommend As New SqlCommand("Select * from Products Where UserID=@UserID And Avail like 'Available'", sqlCon)
            sqlFromProdCommend.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
            sqlFromProdCommend.Parameters("@UserID").Value = userID

            reader = sqlFromProdCommend.ExecuteReader()
        End If

        While reader.Read()
            bodyContent += "<div class='list-item'>

                    <div class='list-img'><img src='img/plan_" & reader.GetInt32(0) & ".jpg' alt='" & reader.GetString(1) & "' class='thumbnail'></div>
                    <div class='list-description'>
                        <h4>" & reader.GetString(1) & "</h4>
                        <p>" & reader.GetString(7) & "</p>
                        
                        <ul>
                            <li><a href='Product-list.aspx?list=user&userid=" & reader.GetInt32(3) & "'>" & GetUsername(reader.GetInt32(3)) & "</a></li>
                            <li>Condition: " & reader.GetString(10) & "</li>
                            <li>Availability: " & reader.GetString(6) & "</li>
                        </ul>
                    </div>
                    <div class='list-details'>
                        <a href='Product.aspx?prod=" & reader.GetInt32(0)

            If listType = "user" Then
                bodyContent += "&list=user"
            End If

            bodyContent += "' class='info-button'>More Details</a>
                        <a href='#' class='list-button'>Swap with Token</a>
                    </div>
                </div>"

            count += 1
        End While

        reader.Close()

        If count > 0 Then
            Return bodyContent
        Else
            Return "There are no project listings in this category. How about you <a href='user/Add-Product.aspx'>add one!</a>"
        End If


    End Function

    Function GetUsername$(id As String)
        Dim username$ = ""

        Dim userReader As SqlDataReader

        Dim sqlFromUserCommand2 As New SqlCommand("Select * from Users Where UserID=@UserID", sqlCon)
        sqlFromUserCommand2.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        sqlFromUserCommand2.Parameters("@UserID").Value = id

        userReader = sqlFromUserCommand2.ExecuteReader()

        While userReader.Read()
            username = userReader.GetString(1)
        End While

        userReader.Close()

        Return username
    End Function

    Function GetCategory$(id As String)
        Dim category$ = ""

        Dim reader As SqlDataReader

        sqlFromCategoryCommand.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
        sqlFromCategoryCommand.Parameters("@CategoryID").Value = id

        reader = sqlFromCategoryCommand.ExecuteReader()

        While reader.Read()
            category = reader.GetString(1)
        End While

        reader.Close()

        Return category
    End Function

    'As the project lists are accessed dynamically through URL encoding, the sitemappath nodes have to be updated accordingly
    Function UpdateSitePath()
        'Set the node to not be read only
        SiteMap.CurrentNode.ReadOnly = False

        'Set the node to the new value
        SiteMap.CurrentNode.Title = listTitleName

        'Set the nodes back to read only
        SiteMap.CurrentNode.ReadOnly = True

    End Function


End Class