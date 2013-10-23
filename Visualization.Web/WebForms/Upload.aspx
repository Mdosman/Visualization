<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="WebForms_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Page</title>

    <script src="../Scripts/jQuery/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../Scripts/jQuery/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

    <script src="../Scripts/Default.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100px;height:100px">
    </div>
    <div align="center">
        <asp:Button ID="btnUpload" Text="Upload Images & Messages" runat="server" OnClick="btnUpload_Click" /><br />
    </div>
    <div style="width:100px;height:20px">
    </div>
    <div align="center">
        <asp:Label ID="lblMessage" runat="server" Text="This button uploads both Images and Messages to the SQL Server."></asp:Label><br />
        <asp:Label ID="lblUploadMessagesFolder" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblUploadImagesFolder" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblMessage2" runat="server" Text="Message File should have the same name as the Folder name."></asp:Label><br />
        <asp:Label ID="lblMessage3" runat="server" Text="Each Image name should have the Foldername and ImageID."></asp:Label><br />
        
        <asp:LinkButton ID="lnkDefault" Text="Filter Options" PostBackUrl="~/WebForms/Default.aspx" runat="server"></asp:LinkButton><br />
        <asp:LinkButton ID="lnkTree" Text="Chart Page" PostBackUrl="~/WebForms/Chart.aspx" runat="server"></asp:LinkButton>
    
    </div>
    </form>
</body>
</html>
