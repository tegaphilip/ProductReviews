<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ProductReviews.Product" %>

<%@ Import Namespace="ProductReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <script src="static/js/datatables.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="static/css/jquery.dataTables.min.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#datatable").DataTable();
        });

    </script>
    <% 
        Dictionary<String, String> product = (Dictionary<String, String>)Session["product"];
        List<String[]> projects = (List<String[]>)Session["reviews"]; 
    %>
    <h4>Product</h4>
    <div class="panel panel-primary">

        <div class="panel-heading">
            Product Info
        </div>
        <table>
            <tr>
                <td>
                    <h2><%=product["name"] %></h2>
                </td>
            </tr>
            <tr>
                <td><% Response.Write("<img src='" + product["image_url"] + "' />"); %></td>
            </tr>

        </table>

        <div class="panel-heading">
            Reviews on this product
        </div>

        <%
            string created = Request.Params["created"];
            if (created == "1")
            {
                Response.Write("<p class='alert-success'>Your review was saved successfully</p>");
            }
            else if (created == "0")
            {
                Response.Write("<p class='alert-danger'>Review was not saved. <br>Reason: " + Util.Base64Decode(Request.Params["message"]));
            }
        %>

        <table id="datatable" class="display" style="width: 100%;" border="1">
            <thead>
                <tr>
                    <th>Reviewer</th>
                    <th>Comment</th>
                    <th>Rating</th>
                    <th>Date Created</th>
                </tr>
            </thead>

            <tbody>
                <%
                    if (projects != null && projects.Count != 0)
                    {

                        for (int i = 0; i < projects.Count; i++) // Loop with for.
                        {
                %>
                <tr>
                    <td><% Response.Write(projects[i][6] + " " + projects[i][7]); %></td>
                    <td><% Response.Write(projects[i][3]); %></td>
                    <td><% Response.Write(projects[i][4]); %></td>
                    <td><% Response.Write(projects[i][5]); %></td>
                </tr>
                <%           
                        }

                    }
                    else
                    {
                %>
                <tr>
                    <td colspan="4">There are currently no reviews for this products</td>
                </tr>
                <% 
                        }
                %>
            </tbody>
        </table>
        <form runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                runat="server" ControlToValidate="comment" ForeColor="Red"
                ErrorMessage="Comment is Required" Display="Dynamic">
            </asp:RequiredFieldValidator>
            <div class="form-group">
                <label for="comment">Add your Review:</label>
                <asp:TextBox ID="comment" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                runat="server" ControlToValidate="rating" ForeColor="Red"
                ErrorMessage="Please select a value" Display="Dynamic">
            </asp:RequiredFieldValidator>
            <div class="form-group">
                <label for="rating">Select a Rating:</label>
                <asp:DropDownList ID="rating" runat="server" CssClass="form-control">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
            </div>


            <div class="form-group">
                <asp:Button ID="SaveComment" runat="server" CssClass="btn btn-default" Text="Save" OnClick="SaveComment_Click" />
            </div>
        </form>
        <!-- panel body -->
    </div>
    <!-- panel default -->

</asp:Content>
