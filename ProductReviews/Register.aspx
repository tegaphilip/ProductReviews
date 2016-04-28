<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ProductReviews.Register" %>
<%@ Import Namespace="ProductReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <h4>Registration</h4>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Create an Account
		
        </div>
        <div class="panel-body">
            <form id="form1" runat="server">

                <%
                    string created = Request.Params["created"];
                    if (created == "1")
                    {
                        Response.Write("<p class='alert-success'>User has successfully registered. <a href = '/Login.aspx'>Click here to login</a></p>");
                    }
                    else if (created == "0")
                    {
                        Response.Write("<p class='alert-danger'>User registration was not successful. <br>Reason: " + Util.Base64Decode(Request.Params["message"])+ "<br>Please try again</p>");
                    }
                %>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="first_name" ForeColor="Red"
                    ErrorMessage="First Name is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <div class="form-group">
                    <label for="first_name">First Name:</label>
                    <asp:TextBox ID="first_name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ControlToValidate="last_name" ForeColor="Red"
                    ErrorMessage="Last Name is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <div class="form-group">
                    <label for="last_name">Last Name:</label>
                    <asp:TextBox ID="last_name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ControlToValidate="email" ForeColor="Red"
                    ErrorMessage="Email is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail"
                    runat="server" ForeColor="Red" Display="Dynamic"
                    ErrorMessage="Email address is incorrect!"
                    ControlToValidate="email"
                    ValidationExpression="[a-zA-Z][a-zA-Z0-9\-\.]+[a-zA-Z]@[a-zA-Z][a-zA-Z0-9\-\.]+[a-zA-Z]+\.[a-zA-Z]{2,4}" />
                <div class="form-group">
                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ControlToValidate="password" ForeColor="Red"
                    ErrorMessage="Password Field is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <div class="form-group">
                    <label for="password">Password:</label>
                    <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ControlToValidate="confirm_password" ForeColor="Red"
                    ErrorMessage="Confirm Password Field is Required" Display="Dynamic">
                </asp:RequiredFieldValidator>

                <asp:CompareValidator ID="CompareValidatorPassword"
                    runat="server" ControlToCompare="password" ControlToValidate="confirm_password"
                    ValueToCompare="Text" ForeColor="Red" ErrorMessage="Passwords do not match!" />
                <div class="form-group">
                    <label for="confirm_password">Confirm Password:</label>
                    <asp:TextBox ID="confirm_password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="RegisterButton" runat="server" CssClass="btn btn-default" Text="Sign Up" OnClick="RegisterButton_Click" />
                </div>
            </form>
        </div>

        <!-- panel body -->
    </div>
    <!-- panel default -->
</asp:Content>
