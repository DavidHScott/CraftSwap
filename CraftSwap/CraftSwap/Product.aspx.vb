Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class Product
    Inherits System.Web.UI.Page

    Dim sqlCon As New SqlConnection("Data Source=.;Initial Catalog=CraftSwap;Integrated Security=True")
    Dim sqlFromProdCommand As New SqlCommand("Select * from Products Where ProductID=@ProductID", sqlCon)
    Dim sqlFromUserCommand As New SqlCommand("Select * from Users Where UserID=@UserID", sqlCon)
    Dim sqlFromCategoryCommand As New SqlCommand("Select * from Categories Where CategoryID like @CategoryID", sqlCon)

    Dim sqlGetTokens As New SqlCommand("Select Tokens from Users Where UserID = @UserID", sqlCon)

    Dim sqlRemoveToken As New SqlCommand("UPDATE Users SET Tokens = (Tokens - 1) WHERE UserID = @UserID", sqlCon)
    Dim sqlNewSaleLog As New SqlCommand("INSERT into SalesLog (UserID, ProductID, Status) VALUES (@UserID, @ProductID, 'Pending')", sqlCon)
    Dim sqlSetAsPending As New SqlCommand("UPDATE Products SET Avail = 'Pending' Where ProductID = @ProductID", sqlCon)

    'The variables needed to populate the page
    Dim listType$ = ""
    Dim productID$ = ""
    Dim categoryID$ = ""
    Dim userID$ = ""

    Dim productTitle$ = ""
    Dim creator$ = ""
    Dim publisher$ = ""
    Dim category$ = ""
    Dim imageName$ = ""
    Dim swapOptions$ = ""
    Dim description$ = ""

    Dim username$ = ""
    Dim available
    Dim successful
    Dim incomplete

    Dim pages
    Dim publication
    Dim condition$ = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Get the bits of URL encoding (A suprise tool that will help us later)
        productID = Request.QueryString("prod")
        listType = Request.QueryString("list")

        sqlFromUserCommand.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        'Set the variables
        GetProductData()
        'Properly set the sitepath nodes
        UpdateSitePath()


        sqlGetTokens.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
    End Sub

    Function PopulateButton()
        Dim content = 0

        If User.Identity.IsAuthenticated Then
            sqlGetTokens.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlCon.Open()

            content = sqlGetTokens.ExecuteScalar()

            sqlCon.Close()
        End If

        Return content

    End Function

    'Set the variables with the relavent information from the database(TODO).
    Function GetProductData()

        sqlFromProdCommand.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
        sqlFromProdCommand.Parameters("@ProductID").Value = productID

        sqlCon.Open()

        Dim reader As SqlDataReader = sqlFromProdCommand.ExecuteReader()

        Do While reader.Read()

            'ADD IF FOR IF PRODUCT NOT AVAILABLE

            productTitle = reader.GetString(1)
            categoryID = reader.GetString(2)
            userID = reader.GetInt32(3)
            creator = reader.GetString(4)
            publisher = reader.GetString(5)
            swapOptions = reader.GetString(6)
            description = reader.GetString(7)
            pages = reader.GetValue(8)
            publication = Convert.ToString(reader.GetValue(9))
            condition = reader.GetString(10)

            imageName = "/img/plan_" & reader.GetInt32(0) & ".jpg"

        Loop

        reader.Close()

        sqlFromUserCommand.Parameters("@UserID").Value = userID

        reader = sqlFromUserCommand.ExecuteReader()

        Do While reader.Read()
            username = reader.GetString(1)
            available = reader.GetInt32(2)
            successful = reader.GetInt32(3)
            incomplete = reader.GetInt32(4)
        Loop

        reader.Close()

        sqlFromCategoryCommand.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Text))
        sqlFromCategoryCommand.Parameters("@CategoryID").Value = categoryID

        reader = sqlFromCategoryCommand.ExecuteReader()

        Do While reader.Read()
            category = reader.GetString(1)
        Loop

        reader.Close()
        sqlCon.Close()

    End Function

    'Populate the leftmost column of the project page with information. This includes the image and category.
    Function PopulateLeftColumn$()
        Dim columnContent$ = "<div><img src='" + imageName + "' class='product-img' alt='" + productTitle + "'></div><div class='product-meta'>
					<span class='posted-In'>Category: <a href='Product-List.aspx?list=cat&catid=" + categoryID + "'>" + category + "</a></span></div>"
        Return columnContent
    End Function

    'Populate the plan information section of the project page. This includes the name, original creator, and other relavent information
    Function PopulatePlanInfo$()
        Dim content$ = "<h1 class='center'>" + productTitle + "</h1><span class='authorship'>By " + creator + "</span>
				<span class='publisher'>Published by " + publisher + "</span><span class='swap-options'>Available for: " + swapOptions + "</span>
				<p class='description'>" + description + "</p>"

        Return content
    End Function

    'Populate the user information section of the project page. This includes the user's name, the number of items the user has,
    'and the number of successful and incomplete swaps
    Function PopulateUserDetails$()
        Dim content = "<p>CraftSwapper Name: <a href='Product-List.aspx?list=user&userid=" + userID + "'>" + username + "</a></p>
					<p>Number of items available to swap: " + available.ToString() + "</p>
					<p>Number of successful swaps: " + successful.ToString() + "</p>
					<p>Number of incomplete swaps: " + incomplete.ToString() + "</p>
				</div>
				<div class='product-details'>
					<h3>Item Details:</h3>
					<p>Number of pages: " + pages.ToString() + "</p>
					<p>Publication Date: " + publication.ToString() + "</p>
					<p>Condition: " + condition + "</p>"

        Return content
    End Function

    'As the product pages are accessed dynamically using URL encoding, the breadcrumbs created by the sitemappath need to be
    'editted to reflect the specific page
    Function UpdateSitePath()
        Dim newSiteNodeTitle$
        Dim listSiteNodeTitle$
        Dim listSiteNodeURL$ = "Product-List.aspx"

        'Set the previous node to the user that uploaded the plan if the product was accessed through a user
        If listType = "user" Then
            listSiteNodeTitle = username
            listSiteNodeURL += "?list=user&userid=" + userID
        Else
            'If the product is accessed any other way, link to the category
            listSiteNodeTitle = category
            listSiteNodeURL += "?list=cat&catid=" + categoryID
        End If

        'Set the current node to the product title
        newSiteNodeTitle = productTitle

        'Set the nodes to not be read only
        SiteMap.CurrentNode.ReadOnly = False
        SiteMap.CurrentNode.ParentNode.ReadOnly = False

        'Set the nodes to the new values
        SiteMap.CurrentNode.Title = newSiteNodeTitle
        SiteMap.CurrentNode.ParentNode.Title = listSiteNodeTitle
        SiteMap.CurrentNode.ParentNode.Url = listSiteNodeURL

        'Set the nodes back to read only (no clue if I actually have to do this, just wanted to make sure I didn't break anything)
        SiteMap.CurrentNode.ReadOnly = True
        SiteMap.CurrentNode.ParentNode.ReadOnly = True

    End Function

    Protected Sub btnSwapProduct_Click(sender As Object, e As EventArgs) Handles btnSwapProduct.Click
        If User.Identity.IsAuthenticated Then

            sqlGetTokens.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

            sqlCon.Open()

            If userID = Integer.Parse(User.Identity.Name) Then

                sqlCon.Close()

                Return
            End If

            If sqlGetTokens.ExecuteScalar() > 0 Then

                sqlRemoveToken.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
                sqlNewSaleLog.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))
                sqlNewSaleLog.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
                sqlSetAsPending.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int))

                sqlRemoveToken.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

                sqlNewSaleLog.Parameters("@ProductID").Value = productID
                sqlNewSaleLog.Parameters("@UserID").Value = Integer.Parse(User.Identity.Name)

                sqlSetAsPending.Parameters("@ProductID").Value = productID

                'Remove token from user
                sqlRemoveToken.ExecuteNonQuery()
                'Add new sale log (as pending)
                sqlNewSaleLog.ExecuteNonQuery()
                'Set Avail as pending
                sqlSetAsPending.ExecuteNonQuery()

                sqlCon.Close()

                'SendEmail()

                Response.Redirect("Default.aspx")

            Else
                sqlCon.Close()

                Response.Redirect("user/buy-tokens.aspx")
            End If
        Else
            FormsAuthentication.RedirectToLoginPage()
        End If
    End Sub

    ' I could not find a way to make this work without the site being hosted on a proper server with a domain name, and a designated email address.
    ' This is just here to show the intended functionality.
    Function SendEmail()
        Dim strFrom = "info@craftswap.com"

        Dim strTo
        Dim pickupAddress
        Dim deliveryAddress
        Dim usersName

        Dim reader As SqlDataReader

        sqlFromUserCommand.Parameters("@UserID").Value = userID

        sqlCon.Open()
        reader = sqlFromUserCommand.ExecuteReader()
        While reader.Read
            strTo = reader.GetString(5)
            pickupAddress = reader.GetValue(9)
            deliveryAddress = reader.GetValue(10)
            usersName = reader.GetString(1)
        End While

        reader.Close()
        sqlCon.Close()

        Dim mailMsg As New MailMessage(New MailAddress(strFrom), New MailAddress(strTo))
        mailMsg.BodyEncoding = Encoding.Default
        mailMsg.Subject = "User " & usersName & " wants to swap your " & productTitle & " for a Token!"
        mailMsg.Body = "Hello, " & username & "! The user " & usersName & " wants to swap your pattern, " & productTitle & ". These are the addresses they have listed:
                    Pickup Address: " & pickupAddress & " Drop Off Address: " & deliveryAddress & ". If you need to communicate with this user before you swap your pattern, 
                    their email address is " & strTo & ". Remember, after the swap occurs, go to your profile, and confirm the swap to receive your Token!"
        mailMsg.Priority = MailPriority.High
        mailMsg.IsBodyHtml = True

        Dim SmtpMail As New SmtpClient
        Dim basicAuthenticationInfo As New System.Net.NetworkCredential("info@craftswap.com", "password")

        SmtpMail.Host = "info@craftswap.com"
        SmtpMail.UseDefaultCredentials = False
        SmtpMail.Credentials = basicAuthenticationInfo

        SmtpMail.Send(mailMsg)

    End Function

End Class