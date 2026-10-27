
<%@ Page Title="Remove Application"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="RemoveApplication.aspx.cs"
    Inherits="HDFC_loans_tutorial1.RemoveApplication" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>DISCARD LOAN APPLICATION</h2>

    <div class="formrow">
        <label>Loan No:</label>
        <asp:TextBox ID="txtLoanNo" runat="server" />

        <asp:Button ID="btnGet"
            runat="server"
            Text="Get Loan Details"
            CssClass="btn"
            OnClick="btnGet_Click" />

        <asp:RequiredFieldValidator
            ID="rfvLoanNo"
            runat="server"
            ControlToValidate="txtLoanNo"
            ValidationGroup="GetLoan"
            ErrorMessage="Loan number is required."
            CssClass="error" />
    </div>

    <div class="formrow">
        <label>Saving Account No:</label>
        <asp:Label ID="lblAccountNo" runat="server" />
    </div>

    <div class="formrow">
        <label>Account Holder Name:</label>
        <asp:Label ID="lblHolderName" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Category:</label>
        <asp:Label ID="lblCategory" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Type:</label>
        <asp:Label ID="lblType" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Issue Date:</label>
        <asp:Label ID="lblIssueDate" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Amount:</label>
        <asp:Label ID="lblAmount" runat="server" />
    </div>

    <div class="formrow">
        <label>Current Address:</label>
        <asp:Label ID="lblAddress" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Remarks:</label>
        <asp:Label ID="lblRemarks" runat="server" />
    </div>

    <asp:Button ID="btnDelete"
        runat="server"
        Text="Discard Application"
        CssClass="btn"
        Enabled="false"
        OnClick="btnDelete_Click" />

    <asp:Label ID="lblMessage"
        runat="server"
        CssClass="msg" />

</asp:Content>
