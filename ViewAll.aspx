
<%@ Page Title="View All Applications"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ViewAll.aspx.cs"
    Inherits="HDFC_loans_tutorial1.ViewAll" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <h2>VIEW ALL LOAN APPLICATIONS</h2>

    <asp:GridView ID="gvLoans"
        runat="server"
        AutoGenerateColumns="true"
        EmptyDataText="No loan applications found."
        BorderWidth="1"
        CellPadding="5"
        GridLines="Both"
        Width="100%">
    </asp:GridView>

    <br />

    <asp:Button ID="btnTotal"
        runat="server"
        Text="Get Total"
        CssClass="btn"
        OnClick="btnTotal_Click" />

    <asp:Label ID="lblTotal"
        runat="server"
        CssClass="msg" />

    <asp:Label ID="lblMessage"
        runat="server"
        CssClass="msg error" />

</asp:Content>
