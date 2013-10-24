<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VSM.aspx.cs" Inherits="Webforms_VSM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Label ID="Label1" runat="server" Text="File Path (.txt)"></asp:Label>
            <br />
            <asp:TextBox ID="txtFilePath" runat="server" Width="765px"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Query"></asp:Label>
            <br />
            <asp:TextBox ID="txtQuery" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Documents"></asp:Label>
            <br />
            <asp:TextBox ID="txtDocs" runat="server" Height="201px" TextMode="MultiLine" Width="775px"></asp:TextBox>
            <br />
            <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" />
            <br />   
            <asp:GridView ID="gvData" runat="server" Caption="VSM" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <br />         
    </div>
    </form>
</body>
</html>
