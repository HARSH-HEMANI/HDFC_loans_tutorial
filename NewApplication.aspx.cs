
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace HDFC_loans_tutorial1
{
    public partial class NewApplication : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            int accountNo;
            decimal amount;
            DateTime issueDate;

            // Validate input values in C#
            if (!int.TryParse(txtAccountNo.Text.Trim(), out accountNo)
                || accountNo <= 0)
            {
                ShowMessage("Enter a valid saving account number.", false);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out amount)
                || amount < 50000 || amount > 2000000)
            {
                ShowMessage(
                    "Loan amount must be between 50000 and 2000000.",
                    false);
                return;
            }

            if (!DateTime.TryParse(txtIssueDate.Text, out issueDate)
                || issueDate.Date > DateTime.Today)
            {
                ShowMessage("Enter a valid issue date.", false);
                return;
            }

            if (txtHolderName.Text.Trim().Length > 50
                || txtProfession.Text.Trim().Length > 50
                || txtEmail.Text.Trim().Length > 50
                || txtContact.Text.Trim().Length > 50
                || txtAddress.Text.Trim().Length > 50
                || txtRemarks.Text.Trim().Length > 50)
            {
                ShowMessage("Text fields must not exceed 50 characters.", false);
                return;
            }

            string cs = ConfigurationManager
                .ConnectionStrings["HDFCConnection"]
                .ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Insert or update account details
                        string accountQuery = @"
IF EXISTS (
    SELECT 1 FROM dbo.Account
    WHERE SavingActNo = @AccountNo
)
BEGIN
    UPDATE dbo.Account
    SET ActHolderName = @Name,
        Profession = @Profession,
        Email = @Email,
        ContactNo = @Contact
    WHERE SavingActNo = @AccountNo
END
ELSE
BEGIN
    INSERT INTO dbo.Account
        (SavingActNo, ActHolderName, Profession, Email, ContactNo)
    VALUES
        (@AccountNo, @Name, @Profession, @Email, @Contact)
END";

                        using (SqlCommand cmd =
                            new SqlCommand(accountQuery, con, transaction))
                        {
                            cmd.Parameters.Add("@AccountNo",
                                SqlDbType.Int).Value = accountNo;

                            cmd.Parameters.Add("@Name",
                                SqlDbType.VarChar, 50).Value =
                                txtHolderName.Text.Trim();

                            cmd.Parameters.Add("@Profession",
                                SqlDbType.VarChar, 50).Value =
                                txtProfession.Text.Trim();

                            cmd.Parameters.Add("@Email",
                                SqlDbType.VarChar, 50).Value =
                                txtEmail.Text.Trim();

                            cmd.Parameters.Add("@Contact",
                                SqlDbType.VarChar, 50).Value =
                                txtContact.Text.Trim();

                            cmd.ExecuteNonQuery();
                        }

                        // Insert the loan application
                        string loanQuery = @"
INSERT INTO dbo.Loan
    (AccountNo, LoanCategory, LoanType, IssueDate,
     Amount, CurrentAddress, LoanRemarks)
VALUES
    (@AccountNo, @Category, @Type, @IssueDate,
     @Amount, @Address, @Remarks)";

                        using (SqlCommand cmd =
                            new SqlCommand(loanQuery, con, transaction))
                        {
                            cmd.Parameters.Add("@AccountNo",
                                SqlDbType.Int).Value = accountNo;

                            cmd.Parameters.Add("@Category",
                                SqlDbType.VarChar, 50).Value =
                                ddlCategory.SelectedValue;

                            cmd.Parameters.Add("@Type",
                                SqlDbType.VarChar, 50).Value =
                                ddlType.SelectedValue;

                            cmd.Parameters.Add("@IssueDate",
                                SqlDbType.Date).Value = issueDate.Date;

                            cmd.Parameters.Add("@Amount",
                                SqlDbType.Decimal).Value = amount;

                            cmd.Parameters.Add("@Address",
                                SqlDbType.VarChar, 50).Value =
                                txtAddress.Text.Trim();

                            cmd.Parameters.Add("@Remarks",
                                SqlDbType.VarChar, 50).Value =
                                txtRemarks.Text.Trim();

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        ShowMessage(
                            "Record inserted successfully.", true);

                        ClearForm();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        ShowMessage(
                            "Application could not be saved. Check the database and entered details.",
                            false);
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtAccountNo.Text = "";
            txtHolderName.Text = "";
            txtProfession.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtIssueDate.Text = "";
            txtAmount.Text = "";
            txtAddress.Text = "";
            txtRemarks.Text = "";
        }

        private void ShowMessage(string message, bool success)
        {
            lblMessage.Text = message;
            lblMessage.CssClass =
                success ? "msg success" : "msg error";
        }
    }
}
