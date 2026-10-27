
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HDFC_loans_tutorial1
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] != null)
            {
                Response.Redirect("home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string connectionString =
                ConfigurationManager
                .ConnectionStrings["HDFCConnection"]
                .ConnectionString;

            using (SqlConnection con =
                   new SqlConnection(connectionString))
            {
                string query =
                    "SELECT COUNT(*) FROM dbo.Users " +
                    "WHERE UserEmail = @Email " +
                    "AND [Password] = @Password";

                using (SqlCommand cmd =
                       new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@Email",
                        SqlDbType.VarChar, 50).Value =
                        txtEmail.Text.Trim();

                    cmd.Parameters.Add("@Password",
                        SqlDbType.VarChar, 100).Value =
                        txtPassword.Text;

                    con.Open();

                    int result = Convert.ToInt32(
                        cmd.ExecuteScalar());

                    if (result == 1)
                    {
                        Session["UserEmail"] =
                            txtEmail.Text.Trim();

                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        lblMessage.Text =
                            "Invalid email or password.";
                    }
                }
            }
        }
    }
}
