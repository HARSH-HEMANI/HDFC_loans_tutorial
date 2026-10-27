
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace HDFC_loans_tutorial1
{
    public partial class ViewAll : Page
    {
        private string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["HDFCConnection"]
                    .ConnectionString;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadApplications();
            }
        }

        private void LoadApplications()
        {
            string query = @"
                SELECT L.LoanNo,
                       L.AccountNo,
                       A.ActHolderName,
                       A.Profession,
                       A.Email,
                       A.ContactNo,
                       L.LoanCategory,
                       L.LoanType,
                       L.IssueDate,
                       L.Amount,
                       L.CurrentAddress,
                       L.LoanRemarks
                FROM dbo.Loan L
                LEFT JOIN dbo.Account A
                    ON L.AccountNo = A.SavingActNo
                ORDER BY L.LoanNo";

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            using (SqlDataAdapter adapter =
                   new SqlDataAdapter(query, con))
            {
                DataTable table = new DataTable();

                adapter.Fill(table);

                gvLoans.DataSource = table;
                gvLoans.DataBind();
            }
        }

        protected void btnTotal_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) FROM dbo.Loan";

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            using (SqlCommand cmd =
                   new SqlCommand(query, con))
            {
                con.Open();

                int total = Convert.ToInt32(
                    cmd.ExecuteScalar());

                lblTotal.Text =
                    "Total number of applications: " + total;
            }
        }
    }
}
