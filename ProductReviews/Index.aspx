<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProductReviews.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Title</title>
    <link rel="stylesheet" href="/static/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/static/css/additions.css" />
    <script src="/static/js/jquery-1.11.2.js"></script>
    <script src="/static/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="jumbotron centralize">
	<h1>Welcome to Product Review Portal</h1>
    <p>
		<asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Sign Up" />
	</p>       
</div>
    </form>
</body>
</html>
