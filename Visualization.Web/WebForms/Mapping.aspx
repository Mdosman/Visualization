<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mapping.aspx.cs" Inherits="WebForms_Mapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Load for Mapping" 
        onclick="Button1_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Process" />
&nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="Export to Excel" />
    <br />
    <br />
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="MsgID" HeaderText="MsgID" SortExpression="MsgID" />
                <asp:BoundField DataField="FolderName" HeaderText="FolderName" 
                    SortExpression="FolderName" />
                <asp:BoundField DataField="MessageID" HeaderText="MessageID" 
                    SortExpression="MessageID" />
                <asp:BoundField DataField="SourceID" HeaderText="SourceID" 
                    SortExpression="SourceID" />
                <asp:BoundField DataField="Event" HeaderText="Event" SortExpression="Event" />
                <asp:BoundField DataField="EventID" HeaderText="EventID" 
                    SortExpression="EventID" />
                <asp:BoundField DataField="EA1" HeaderText="EA1" SortExpression="EA1" />
                <asp:BoundField DataField="EA2" HeaderText="EA2" SortExpression="EA2" />
                <asp:BoundField DataField="EA3" HeaderText="EA3" SortExpression="EA3" />
                <asp:BoundField DataField="EA4" HeaderText="EA4" SortExpression="EA4" />
                <asp:BoundField DataField="EA5" HeaderText="EA5" SortExpression="EA5" />
                <asp:BoundField DataField="EA6" HeaderText="EA6" SortExpression="EA6" />
                <asp:BoundField DataField="GroupID" HeaderText="GroupID" 
                    SortExpression="GroupID" />
            </Columns>
        </asp:GridView>
    
    </div>

    </form>
</body>
</html>
