<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tabs.aspx.cs" Inherits="WebForms_Tabs" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" /> 

	<title>Nested 3-Levels Demo</title> 

	<link type="text/css" rel="stylesheet" href="../lib/css/layout-default-latest.css" />
    <link href="../Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/data.json.js" type="text/javascript"></script>
    <script src="../Scripts/MarkerClusterer.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAuyiTAfNDyoRQybx_jy0FyAMiPVj1E92A&sensor=true">
    </script>
 <form id="form1" runat="server">
	  <asp:ScriptManager ID="scriptMgr" runat="server">
	    <Scripts>	  	  
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery-latest.js" />
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery-ui-latest.js" />
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery.layout-latest.js" />
	  <asp:ScriptReference Path= "../Scripts/Tabs.js" />  
	  		
	  <asp:ScriptReference Path="../Scripts/jstree/jquery.jstree.js" />
	  <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.cookie.js" />
	  <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.hotkeys.js" />
	  
	  </Scripts>
	  </asp:ScriptManager>	 	  
     <div id="tabs">
        <ul>
            <li><a href="#tabMap">Map</a></li>
            <li><a href="#tabGraph">Graph</a></li>
            <li><a href="#tabBar">Bar</a></li>
        </ul>
        <div id="tabMap">
            <div id="map-canvas" style="width: 500px; height: 600px"> </div>
        </div>
        <div id="tabGraph">
            <asp:TextBox ID="txt2" runat="server" Text="Graph goes here" TextMode="MultiLine" width="95%" Height="200" />
        </div>
        <div id="tabBar">
            <asp:TextBox ID="txt3" runat="server" Text="Bar Chart here" TextMode="MultiLine" width="95%" Height="200" />
        </div>
    </div>
</form>
</body>
</html>