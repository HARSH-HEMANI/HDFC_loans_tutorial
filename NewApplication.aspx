
<%@ Page Title="New Application"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="NewApplication.aspx.cs"
    Inherits="HDFC_loans_tutorial1.NewApplication" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>NEW LOAN APPLICATION</h2>

    <div class="formrow">
        <label>Saving Account No:</label>
        <asp:TextBox ID="txtAccountNo" runat="server" />
        <asp:RequiredFieldValidator
            ID="rfvAccountNo"
            runat="server"
            ControlToValidate="txtAccountNo"
            ErrorMessage="Account number is required."
            CssClass="error" />
    </div>

    <div class="formrow">
        <label>Account Holder Name:</label>
        <asp:TextBox ID="txtHolderName" runat="server" />
        <asp:RequiredFieldValidator
            ID="rfvHolderName"
            runat="server"
            ControlToValidate="txtHolderName"
            ErrorMessage="Holder name is required."
            CssClass="error" />
    </div>

    <div class="formrow">
        <label>Profession:</label>
        <asp:TextBox ID="txtProfession" runat="server" />
    </div>

    <div class="formrow">
        <label>Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" />
    </div>

    <div class="formrow">
        <label>Contact No:</label>
        <asp:TextBox ID="txtContact" runat="server" />
    </div>

    <div class="formrow">
        <label>Loan Category:</label>
        <asp:DropDownList ID="ddlCategory" runat="server">
            <asp:ListItem Text="Property Loan" Value="Property Loan" />
            <asp:ListItem Text="Personal Loan" Value="Personal Loan" />
            <asp:ListItem Text="Education Loan" Value="Education Loan" />
            <asp:ListItem Text="Vehicle Loan" Value="Vehicle Loan" />
        </asp:DropDownList>
    </div>

    <div class="formrow">
        <label>Loan Type:</label>
        <asp:DropDownList ID="ddlType" runat="server">
            <asp:ListItem Text="Home Loan" Value="Home Loan" />
            <asp:ListItem Text="Education Loan" Value="Education Loan" />
            <asp:ListItem Text="Personal Loan" Value="Personal Loan" />
            <asp:ListItem Text="Vehicle Loan" Value="Vehicle Loan" />
        </asp:DropDownList>
    </div>

    <div class="formrow">
        <label>Loan Issue Date:</label>
        <asp:TextBox ID="txtIssueDate"
            runat="server"
            TextMode="Date" />

        <asp:RequiredFieldValidator
            ID="rfvIssueDate"
            runat="server"
            ControlToValidate="txtIssueDate"
            ErrorMessage="Issue date is required."
            CssClass="error" />
    </div>

    <div class="formrow">
        <label>Loan Amount:</label>
        <asp:TextBox ID="txtAmount" runat="server" />

        <asp:RequiredFieldValidator
            ID="rfvAmount"
            runat="server"
            ControlToValidate="txtAmount"
            ErrorMessage="Amount is required."
            CssClass="error" />
    </div>

    <div class="formrow">
        <label>Current Address:</label>
        <asp:TextBox ID="txtAddress"
            runat="server"
            TextMode="MultiLine" />
    </div>

    <div class="formrow">
        <label>Loan Remarks:</label>
        <asp:TextBox ID="txtRemarks"
            runat="server"
            TextMode="MultiLine" />
    </div>

    <asp:Button ID="btnSubmit"
        runat="server"
        Text="Submit Application"
        CssClass="btn"
        OnClick="btnSubmit_Click" />

    <asp:Label ID="lblMessage"
        runat="server"
        CssClass="msg" />

</asp:Content>
