using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class Product : System.Web.UI.Page
    {
        public Dictionary<String, String> productInfo;
        public List<String[]> reviews;
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String sid = Request.Params["id"];

                if (sid == null)
                {
                    throw new Exception();
                }
                this.id = Int32.Parse(sid);

                DBConn db = new DBConn();

                productInfo = db.getProductInfoByID(id);

                if (productInfo == null)
                {
                   throw new Exception();
                }
                else
                {
                    // this is horrible programming and I know it but I don't care right now
                    Session["product"] = productInfo;
                    reviews = db.getProductReviews(this.id);
                    Session["reviews"] = reviews;
                }
            } 
            catch (Exception ex)
            {
                Response.Redirect("404.aspx");
            }
        }

        protected void SaveComment_Click(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Login.aspx?redirect_error=You need to log in to make a review");
            }
            else
            {
                String commentAdded = comment.Text;
                int point = Int32.Parse(rating.Text);

                DBConn db = new DBConn();
                if (db.AddReview(this.id, Int32.Parse(Session["id"].ToString()), commentAdded, point))
                {
                    Response.Redirect("Product.aspx?id="+this.id+"&created=1");
                }
                else
                {
                    Response.Redirect("Product.aspx?id=" + this.id + "&created=0&message=" + Util.Base64Encode(db.getErrorMessage()));
                }
            }
        }
    }
}