
<%@ Page Title="Home"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="home.aspx.cs"
    Inherits="HDFC_loans_tutorial1.home" %>

<asp:Content
    ID="HomeContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>Welcome to HDFC Loans</h2>

    <p>
        Welcome to HDFC Loans — the next step to fulfilling
        your dreams and financial needs.
    </p>

    <p>
        You can use this application to perform the following
        operations:
    </p>

    <ul>
        <li>Submit a new loan application.</li>
        <li>Update an existing loan application.</li>
        <li>Remove a loan application.</li>
        <li>View all loan applications.</li>
    </ul>

    <p>
        Select an option from the navigation menu above
        to continue.
    </p>

</asp:Content>
