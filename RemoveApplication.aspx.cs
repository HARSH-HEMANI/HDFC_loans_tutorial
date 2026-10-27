
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace HDFC_loans_tutorial1
{
    public partial class RemoveApplication : Page
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
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            Session.Remove("LoanToRemove");

            int loanNo;

            if (!int.TryParse(txtLoanNo.Text.Trim(), out loanNo)
                || loanNo <= 0)
            {
                ShowMessage("Enter a valid loan number.", false);
                return;
            }

            string query = @"
                SELECT L.LoanNo, L.AccountNo,
                       A.ActHolderName, L.LoanCategory,
                       L.LoanType, L.IssueDate,
                       L.Amount, L.CurrentAddress,
                       L.LoanRemarks
                FROM dbo.Loan L
                LEFT JOIN dbo.Account A
                    ON L.AccountNo = A.SavingActNo
                WHERE L.LoanNo = @LoanNo";

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@LoanNo",
                    SqlDbType.Int).Value = loanNo;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowMessage("Loan application not found.", false);
                        return;
                    }

                    lblAccountNo.Text =
                        GetValue(reader, "AccountNo");

                    lblHolderName.Text =
                        GetValue(reader, "ActHolderName");

                    lblCategory.Text =
                        GetValue(reader, "LoanCategory");

                    lblType.Text =
                        GetValue(reader, "LoanType");

                    lblIssueDate.Text =
                        GetValue(reader, "IssueDate");

                    lblAmount.Text =
                        GetValue(reader, "Amount");

                    lblAddress.Text =
                        GetValue(reader, "CurrentAddress");

                    lblRemarks.Text =
                        GetValue(reader, "LoanRemarks");

                    Session["LoanToRemove"] = loanNo;
                }
            }

            btnDelete.Enabled = true;
            ShowMessage(
                "Loan details loaded. You can now remove this application.",
                true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Session["LoanToRemove"] == null)
            {
                ShowMessage(
                    "Retrieve a loan before removing it.", false);
                return;
            }

            int loanNo = Convert.ToInt32(Session["LoanToRemove"]);

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM dbo.Loan WHERE LoanNo = @LoanNo", con))
            {
                cmd.Parameters.Add("@LoanNo",
                    SqlDbType.Int).Value = loanNo;

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                Session.Remove("LoanToRemove");
                btnDelete.Enabled = false;

                if (rowsAffected > 0)
                {
                    ClearDetails();
                    ShowMessage(
                        "Record removed successfully.", true);
                }
                else
                {
                    ShowMessage(
                        "Loan application no longer exists.", false);
                }
            }
        }

        private string GetValue(
            SqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return "";

            return Convert.ToString(reader[column]);
        }

        private void ClearDetails()
        {
            lblAccountNo.Text = "";
            lblHolderName.Text = "";
            lblCategory.Text = "";
            lblType.Text = "";
            lblIssueDate.Text = "";
            lblAmount.Text = "";
            lblAddress.Text = "";
            lblRemarks.Text = "";
        }

        private void ShowMessage(string message, bool success)
        {
            lblMessage.Text = message;
            lblMessage.CssClass =
                success ? "msg success" : "msg error";
        }
    }
}
