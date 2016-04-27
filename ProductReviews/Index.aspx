<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProductReviews.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    <div class="jumbotron centralize">
	<h1>Welcome to Product Review Portal</h1>

    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Sign Up" />
    </form>   
</div>
</asp:Content>
