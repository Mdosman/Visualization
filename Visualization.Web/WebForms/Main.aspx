<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="WebForms_Main" %>

<!DOCTYPE html>
<html>
<head>
<title>Nested Sidebars Layout</title>

<link href="../Styles/layout-default-latest.css" rel="stylesheet" type="text/css" />
		<%--<script src="../Scripts/jstree/_lib/jquery.js" type="text/javascript"></script>
		<script src="../Scripts/jstree/jquery.jstree.js" type="text/javascript"></script>
		<script src="../Scripts/jstree/_lib/jquery.cookie.js" type="text/javascript"></script>		
		<script src="../Scripts/jstree/_lib/jquery.hotkeys.js" type="text/javascript"></script>
        <script type="text/javascript" src="../Scripts/jstree/jquery.json-2.2.js"></script>
        <script type="text/javascript" src="../Scripts/jstree/json2xml.js"></script>--%>

</head>
	<body>
	  <form id="form1" runat="server">
	  <asp:ScriptManager ID="scriptMgr" runat="server">
	  <Scripts>	
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery-latest.js" />
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery-ui-latest.js" />
	  <asp:ScriptReference Path="../Scripts/jQuery/jquery.layout-latest.js" />  
	  <asp:ScriptReference Path= "../Scripts/Main.js" />
	  </Scripts>
	  </asp:ScriptManager>	 
	  <asp:UpdatePanel ID="updPanel" runat="server">
	  <ContentTemplate>	  
        <div id="layout_container"><!-- Outer Layout Container -->
            <div class="outer-north center">
		        North <br /><br />(Outer Layout - North Pane)
	        </div>
	        <div class="outer-west center">
		        West <br /><br />(Outer Layout - West Pane)
	        </div>

	        <div class="outer-center"><!-- Middle Layout Container -->

		        <div class="middle-center"><!-- Inner-North Layout Container -->
			        <div class="north-center border">North Center <br /><br />(Inner-Center Layout - Center Pane)</div>
			        <div class="north-west border">North West     <br /><br />(Inner-Center Layout - West Pane)</div>
		        </div>

		        <div class="middle-south"><!-- Inner-South Layout Container -->
			        <div class="south-center border">South Center <br /><br />(Inner-South Layout - Center Pane)</div>
			        <div class="south-west border">South West     <br /><br />(Inner-South Layout - West Pane)</div>
		        </div>

	        </div>

        </div>
      </ContentTemplate>
      </asp:UpdatePanel>

    </form>
</body>
</html>