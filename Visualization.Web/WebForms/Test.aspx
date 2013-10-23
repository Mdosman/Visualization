<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="WebForms_Test" %>

<!DOCTYPE html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Nested 3-Levels Demo</title>
    <link href="../Styles/layout-default-latest.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /*
	 * GENERAL COSMETICS 
	 */body
        {
            font-size: 90%; /* IE only */
            padding: 15px;
            margin: 0;
        }
        #layout_container, #layout_container div
        {
            padding: 15px;
            overflow: auto;
        }
        div#layout_container
        {
            height: 850px;
            overflow: visible;
        }
        .center
        {
            text-align: center;
        }
        /*
	 * ADD BORDERS & COLORS TO LAYOUT ELEMENTS
	 *//* use 'generic classes' to add default backgrounds & borders */.ui-layout-pane
        {
            background: #FFF;
            border: 0.5px dotted #999;
        }
        .ui-layout-resizer
        {
            background: #EEE !important;
            border: 0.5px solid #999;
        }
        .ui-layout-toggler
        {
            background: #999 !important;
        }
        /* override '.middle-center div' rule *//* give specific elements their own colors */#layout_container
        {
            background: #999;
        }
        .demo, .demo input, .jstree-dnd-helper, #vakata-contextmenu,select, option, input,a,span, textarea { font-size:10px; font-family:Verdana; }
    </style>
</head>
<body>
    <link href="../Styles/BarGraphStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAuyiTAfNDyoRQybx_jy0FyAMiPVj1E92A&sensor=true">
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../Scripts/jQuery/jquery-latest.js" />
            <asp:ScriptReference Path="../Scripts/jQuery/jquery-ui-latest.js" />
            <asp:ScriptReference Path="../Scripts/jQuery/jquery.layout-latest.js" />
            <asp:ScriptReference Path="../Scripts/Test.js" />
            <asp:ScriptReference Path="../Scripts/jstree/jquery.jstree.js" />
            <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.cookie.js" />
            <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.hotkeys.js" />
            <asp:ScriptReference Path="../Scripts/jBarGraph.js" />
            <asp:ScriptReference Path="../Scripts/MarkerClusterer.js" />
        </Scripts>
    </asp:ScriptManager>
    <%--<style type="text/css">
	html, body { margin:0; padding:0; }
	body, td, th, pre, code, select, option, input, textarea { font-family:verdana,arial,sans-serif; font-size:10px; }
	.demo, .demo input, .jstree-dnd-helper, #vakata-contextmenu { font-size:10px; font-family:Verdana; }
	
	#text { margin-top:1px; }

	#alog { font-size:9px !important; margin:5px; border:1px solid silver; }
	</style> --%>
    <%--<div id="layout_container">--%><!-- Outer Layout Container -->
    <div class="outer-north center">
        <asp:TextBox ID="txtMessages" runat="server" Width="100%" TextMode="MultiLine" Rows="4"
            Style="font-size: 10px; font-family: Verdana;"></asp:TextBox>
        <asp:HiddenField ID="hdnIsPostBack" runat="server" Value="0" />
        <asp:TextBox ID="txtLatLon" Style="position: absolute; left: -500px" runat="server"></asp:TextBox>
        <table align="center">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMsgCount" runat="server" Text=""></asp:Label>
                </td>
                <td width="550px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkUpload" Text="Upload Page" PostBackUrl="~/WebForms/Upload.aspx"
                        runat="server" CssClass="Label"></asp:LinkButton>
                </td>
                <td width="50px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkDefault" Text="Back to MainPage" PostBackUrl="~/WebForms/Default.aspx"
                        runat="server"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="outer-center">
        <!-- Middle Layout Container -->
        <div id="tabs">
            <ul>
                <li><a href="#tabMap">Map</a></li>
                <li><a href="#tabBar">Bar</a></li>
                <li><a href="#tabGraph">Graph</a></li>
            </ul>
            <div id="tabMap">
                <div id="map-canvas" style="width: 1150px; height: 720px">
                </div>
            </div>
            <div id="tabBar">
                <div id="divBar" style="width: 1150px; height: 720px">
                </div>
            </div>
            <div id="tabGraph">
                <div id="divGraph" style="width: 1150px; height: 720px">
                </div>
            </div>
        </div>
    </div>
    <div class="outer-east">
        <div class="east-center">
            <!-- Inner-North Layout Container -->
            <div id="container">
                <!-- the tree container (notice NOT an UL node) -->
                <div id="demo" class="demo">
                </div>
                <!-- JavaScript neccessary for the tree -->
            </div>
            <asp:TextBox ID="txtEventList" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
            <asp:TextBox ID="txtEventAttributeTypeList" runat="server" Style="position: absolute;
                left: -500px;"></asp:TextBox>
            <asp:TextBox ID="txtEventAttributeSubTypeList" runat="server" Style="position: absolute;
                left: -500px;"></asp:TextBox>
            <asp:TextBox ID="txtChecklistID" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
            <asp:TextBox ID="txtXml" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
            <asp:TextBox ID="txtJSON" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
        </div>
        <div class="east-south">
            <!-- Inner-South Layout Container -->
            East South
        </div>
    </div>
    <%-- </div>--%>
    <%--<asp:Button ID="btnFilter" runat="server" Text="Filter" OnClientClick="GetCheckedItems();"
        OnClick="btnFilter_Click" />--%>
        
    </form>
</body>
</html>
