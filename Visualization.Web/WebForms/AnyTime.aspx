<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnyTime.aspx.cs" Inherits="WebForms_AnyTime" %>
<!doctype html>
<html>
<head>
  <meta charset="utf-8">
  <title>AnyTime Example</title>
    <link href="../Styles/AnyTime/anytime.min.css" rel="stylesheet" type="text/css" />
        <%--<script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>--%>
    <script src="../Scripts/jQuery/jquery-latest.js" type="text/javascript"></script>
    <script src="../Scripts/AnyTime/jquery-migrate-1.1.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/AnyTime/anytime.min.js" type="text/javascript"></script>    
</head>
<body>
<form runat="server">
    <table align="right">
            <tr>
                <td align="right">
                    Date(From)<asp:TextBox ID="txtEventDateFrom" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Date(To)  <asp:TextBox ID="txtEventDateTo" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            </table>
            <script type="text/javascript">AnyTime.picker('txtEventDateFrom');</script>
            <script type="text/javascript">AnyTime.picker('txtEventDateTo');</script>
</form>
</body>
</html>
