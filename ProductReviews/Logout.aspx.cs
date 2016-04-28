using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id"] = null;
            Session["email"] = null;
            Session["first_name"] = null;
            Session["last_name"] = null;
            Session["date_registered"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}