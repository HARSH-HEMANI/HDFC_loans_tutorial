
using System;
using System.Web.UI;

namespace HDFC_loans_tutorial1
{
    public partial class home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
}
