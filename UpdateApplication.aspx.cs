
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HDFC_loans_tutorial1
{
    public partial class UpdateApplication : Page
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
            Page.Validate("GetLoan");

            if (!Page.IsValid)
                return;

            int loanNo;

            if (!int.TryParse(txtLoanNo.Text.Trim(), out loanNo)
                || loanNo <= 0)
            {
                ShowMessage("Enter a valid loan number.", false);
                return;
            }

            string query = @"
                SELECT L.LoanNo, L.AccountNo,
                       A.ActHolderName, A.Profession,
                       A.Email, A.ContactNo,
                       L.LoanCategory, L.LoanType,
                       L.IssueDate, L.Amount,
                       L.CurrentAddress, L.LoanRemarks
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
                        ViewState["LoadedLoanNo"] = null;
                        ShowMessage("Loan application not found.", false);
                        return;
                    }

                    txtAccountNo.Text =
                        GetValue(reader, "AccountNo");

                    txtHolderName.Text =
                        GetValue(reader, "ActHolderName");

                    txtProfession.Text =
                        GetValue(reader, "Profession");

                    txtEmail.Text =
                        GetValue(reader, "Email");

                    txtContact.Text =
                        GetValue(reader, "ContactNo");

                    SelectItem(ddlCategory,
                        GetValue(reader, "LoanCategory"));

                    SelectItem(ddlType,
                        GetValue(reader, "LoanType"));

                    txtIssueDate.Text =
                        reader["IssueDate"] == DBNull.Value
                        ? ""
                        : Convert.ToDateTime(
                            reader["IssueDate"]).ToString("yyyy-MM-dd");

                    txtAmount.Text =
                        GetValue(reader, "Amount");

                    txtAddress.Text =
                        GetValue(reader, "CurrentAddress");

                    txtRemarks.Text =
                        GetValue(reader, "LoanRemarks");

                    ViewState["LoadedLoanNo"] = loanNo;
                }
            }

            ShowMessage("Loan details loaded successfully.", true);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["LoadedLoanNo"] == null)
            {
                ShowMessage("Retrieve a loan before updating it.", false);
                return;
            }

            int accountNo;
            decimal amount;
            DateTime issueDate;

            if (!int.TryParse(txtAccountNo.Text.Trim(), out accountNo)
                || accountNo <= 0)
            {
                ShowMessage("Enter a valid account number.", false);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHolderName.Text))
            {
                ShowMessage("Account holder name is required.", false);
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

            int loanNo = Convert.ToInt32(ViewState["LoadedLoanNo"]);

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Insert or update account details.
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

                        // Update loan details.
                        string loanQuery = @"
UPDATE dbo.Loan
SET AccountNo = @AccountNo,
    LoanCategory = @Category,
    LoanType = @Type,
    IssueDate = @IssueDate,
    Amount = @Amount,
    CurrentAddress = @Address,
    LoanRemarks = @Remarks
WHERE LoanNo = @LoanNo";

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

                            SqlParameter amountParameter =
                                cmd.Parameters.Add(
                                    "@Amount", SqlDbType.Decimal);

                            amountParameter.Precision = 18;
                            amountParameter.Scale = 0;
                            amountParameter.Value = amount;

                            cmd.Parameters.Add("@Address",
                                SqlDbType.VarChar, 50).Value =
                                txtAddress.Text.Trim();

                            cmd.Parameters.Add("@Remarks",
                                SqlDbType.VarChar, 50).Value =
                                txtRemarks.Text.Trim();

                            cmd.Parameters.Add("@LoanNo",
                                SqlDbType.Int).Value = loanNo;

                            int rows = cmd.ExecuteNonQuery();

                            if (rows == 0)
                                throw new Exception("Loan not found.");
                        }

                        transaction.Commit();

                        ShowMessage("Record updated successfully.", true);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        ShowMessage(
                            "Update failed. Check the account number and database connection.",
                            false);
                    }
                }
            }
        }

        private string GetValue(SqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
                return "";

            return Convert.ToString(reader[column]);
        }

        private void SelectItem(DropDownList list, string value)
        {
            ListItem item = list.Items.FindByValue(value);

            if (item != null)
                list.SelectedValue = value;
        }

        private void ShowMessage(string message, bool success)
        {
            lblMessage.Text = message;
            lblMessage.CssClass =
                success ? "msg success" : "msg error";
        }
    }
}
