using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string em = email.Text;
            string pass = password.Text;
            string firstName = first_name.Text;
            string lastName = last_name.Text;

            DBConn db = new DBConn();
            if (db.RegisterUser(em, pass, firstName, lastName))
            {
                Response.Redirect("Register.aspx?created=1");
            }
            else
            {
                Response.Redirect("Register.aspx?created=0&message=" + Util.Base64Encode(db.getErrorMessage()));
            }
        }
    }
}