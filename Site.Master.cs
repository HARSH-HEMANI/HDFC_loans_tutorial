
using System;
using System.Web.UI;

namespace HDFC_loans_tutorial1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] != null)
            {
                lblUser.Text = Server.HtmlEncode(
                    Convert.ToString(Session["UserEmail"]));
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/login.aspx");
        }
    }
}
