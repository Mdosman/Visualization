<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarChart.aspx.cs" Inherits="WebForms_BarChart" %>

<!DOCTYPE html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Chart</title>
    <link href="../Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
<%--    <link href="../Styles/960.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/text.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smoothness/ui.css" rel="stylesheet" type="text/css" />--%>
    
    <script type="text/javascript" src="../Scripts/jQuery/jquery-latest.js" />
    <script type="text/javascript" src="../Scripts/jQuery/jquery-ui-latest.js" />
    <script type="text/javascript" src="../Scripts/BarChart.js" />
    <script type="text/javascript" src="../Scripts/jBarGraph.js" />
    <script type="text/javascript" src="../Scripts/highcharts/highcharts.js"/>
    <script type="text/javascript" src="../Scripts/highcharts/highstock.js"/>
    <!-- Additional files for the Highslide popup effect -->
    
    <%--<script type="text/javascript" src="../Scripts/highslide-full.min.js"></script>
    <script type="text/javascript" src="../Scripts/highslide.config.js" charset="utf-8"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divBar" style="width: 1150px; height: 720px">
        </div>
    </div> 
    <asp:TextBox ID="txtBarGraphData" style="position: absolute;left: -500px " runat="server" ></asp:TextBox>
    </form>
</body>
</html>
