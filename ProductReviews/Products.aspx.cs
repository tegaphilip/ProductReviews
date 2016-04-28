using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductReviews
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public static List<String[]> getProducts()
        {
            DBConn db = new DBConn();
            return db.getProductsAndReviews();
        }
    }
}