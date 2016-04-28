<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ProductReviews.Products" %>

<%@ Import Namespace="ProductReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <script src="static/js/datatables.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="static/css/jquery.dataTables.min.css">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#datatable").DataTable();
        });

</script>
    <h4>Projects</h4>
    <div class="panel panel-primary">

        <div class="panel-heading">
            Products on this portal
        </div>

        <table id="datatable" class="display" style="width: 100%;" border="1">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Image</th>
                    <th>Date Created</th>
                    <th># of Reviews</th>
                    <th>View Reviews</th>
                </tr>
            </thead>

            <tbody>
                <%
                    List<String[]> projects = Products.getProducts();

                    if (projects != null && projects.Count != 0)
                    {

                        for (int i = 0; i < projects.Count; i++) // Loop with for.
                        {
                %>
                <tr>
                    <td><% Response.Write(projects[i][1]); %></td>
                    <td><% Response.Write("<img width='200' height='200' src='" + projects[i][2] + "' />"); %></td>
                    <td><% Response.Write(projects[i][3]); %></td>
                    <td><% Response.Write(projects[i][4]); %></td>
                    <td><a href="Product.aspx?Id=<% Response.Write(projects[i][0]); %>">View</a></td>
                </tr>
                <%           
                            }

                        }
                        else
                        {
					%>
                <tr>
                    <td colspan="4">There are currently no products</td>
                </tr>
                <% 
                        }
					%>
            </tbody>
        </table>

        <!-- panel body -->
    </div>
    <!-- panel default -->

</asp:Content>

