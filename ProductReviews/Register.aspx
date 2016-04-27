<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ProductReviews.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <h4>Registration</h4>
    <div class="panel panel-primary">
        <div class="panel-heading">
            Create an Account
		
        </div>
        <div class="panel-body">
            <form id="form1" runat="server">
                
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
                

                <div class="form-group">
                    <label for="email">Email:</label>
                    <asp:TextBox ID="email" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="password">Password:</label>
                    <asp:TextBox ID="password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="confirm_password">Confirm Password:</label>
                    <asp:TextBox ID="confirm_password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="RegisterButton" runat="server" CssClass="btn btn-default" Text="Sign Up" />
                </div>
            </form>
        </div>

        <!-- panel body -->
    </div>
    <!-- panel default -->
</asp:Content>
