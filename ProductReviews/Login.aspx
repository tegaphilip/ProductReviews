<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProductReviews.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Login
		
        </div>
        <div class="panel-body">
            <form id="form1" runat="server">
                <div class="form-group">
                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="password">Password:</label>
                    <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="SignIn" runat="server" CssClass="btn btn-default" Text="Login" OnClick="SignIn_Click" />
                    <asp:Button ID="Register" runat="server" CssClass="btn btn-default pull-right" Text="Sign Up" />
                </div>
            </form>
        </div>

        <!-- panel body -->
    </div>
</asp:Content>
