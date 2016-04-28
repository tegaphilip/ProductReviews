using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            string mail = email.Text;
            string pass = password.Text;
            DBConn db = new DBConn();

            Dictionary<String, String> userData = db.getUserByEmail(mail);

            if (userData == null)
            {
                Response.Redirect("Login.aspx?error=true");
            }
            else
            {
                if (!Util.checkPasword(pass, userData["password"]))
                {
                    Response.Redirect("Login.aspx?error=true");
                }
                else
                {
                    Session["id"] = userData["id"];
                    Session["email"] = userData["email"];
                    Session["first_name"] = userData["first_name"];
                    Session["last_name"] = userData["last_name"];
                    Session["date_registered"] = userData["date_registered"];

                    Response.Redirect("Products.aspx");
                }
            }
        }
    }
}