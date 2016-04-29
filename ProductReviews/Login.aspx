<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProductReviews.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Login
		
        </div>
        <div class="panel-body">
            <form id="form1" runat="server">

                <%
                    string error_message = Request.Params["error"];
                    string redirect_error = Request.Params["redirect_error"];
                    if (redirect_error != null)
                    {
                        Response.Write("<p class='alert-danger'>" + redirect_error + "</p>");
                    }
                    else if (error_message != null)
                    {
                        Response.Write("<p class='alert-danger'>Invalid login details! <br>Please try again</p>");
                    }
                %>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="email" ForeColor="Red"
                    ErrorMessage="Email is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <div class="form-group">
                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ControlToValidate="password" ForeColor="Red"
                    ErrorMessage="Password is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <div class="form-group">
                    <label for="password">Password:</label>
                    <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="SignIn" runat="server" CssClass="btn btn-default" Text="Login" OnClick="SignIn_Click" />
                    <!--<asp:Button ID="Register" CssClass="btn btn-default pull-right" Text="Sign Up" />-->
                    <a href="Register.aspx" class="btn btn-default pull-right">Sign Up</a>
                </div>
            </form>
        </div>

        <!-- panel body -->
    </div>
</asp:Content>
