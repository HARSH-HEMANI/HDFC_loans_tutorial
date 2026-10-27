
<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="login.aspx.cs"
    Inherits="HDFC_loans_tutorial1.login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>User Login - HDFC Loans</title>
    <link href="content/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="wrap" style="max-width:520px;">

            <div class="header">
                <h1>User Login</h1>
            </div>

            <div class="content">

                <div class="formrow">
                    <label>Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" />
                    <asp:RequiredFieldValidator
                        ID="rfvEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Email is required."
                        CssClass="error"
                        Display="Dynamic" />
                </div>

                <div class="formrow">
                    <label>Password:</label>
                    <asp:TextBox ID="txtPassword"
                        runat="server"
                        TextMode="Password" />
                    <asp:RequiredFieldValidator
                        ID="rfvPassword"
                        runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="Password is required."
                        CssClass="error"
                        Display="Dynamic" />
                </div>

                <asp:Button ID="btnLogin"
                    runat="server"
                    Text="Sign In"
                    CssClass="btn"
                    OnClick="btnLogin_Click" />

                <asp:Label ID="lblMessage"
                    runat="server"
                    CssClass="msg error" />

            </div>
        </div>

    </form>
</body>
</html>
