<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageSavedFilteredMessages.aspx.cs" Inherits="WebForms_ManageSavedFilteredMessages" %>
<!doctype html>
<html>
<head>
  <meta charset="utf-8">
    <title>Saved Filtered Messages</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <link href="../Scripts/JQGrid/redmond-jquery-ui-custom.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/JQGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/JQGrid/ui.multiselect.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/JQGrid/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../Scripts/JQGrid/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/JQGrid/grid.locale-en.js" type="text/javascript"></script>
    
    <script type="text/javascript">
	    $.jgrid.no_legacy_api = true;
	    $.jgrid.useJSON = true;
    </script>

    <script src="../Scripts/JQGrid/ui.multiselect.js" type="text/javascript"></script>
    <script src="../Scripts/JQGrid/jquery.jqgrid.js" type="text/javascript"></script>
    <script src="../Scripts/JQGrid/jquery.contextmenu.js" type="text/javascript"></script>


    <script type="text/javascript">
    $(document).ready(function () {
        jQuery("#list").jqGrid({
   	        url:'../HttpHandler/MessageHandler.ashx?operation=jqGridSavedFilteredMessages',
	        datatype: "json",
	        colNames: ['ID', 'Name', 'FilteredMessage', 'Description'],
   	        colModel:[
   		        { name: 'ID', index: 'ID', key: true, width: 25 },
   		        { name: 'Name', index: 'Name', width: 50 },
   		        { name: 'FilteredMessage', index: 'FilteredMessage', width: 750 },
   		        { name: 'Description', index: 'Description', width: 200, editable: true, editoptions: { size: 500 } }
   	        ],
   	        rowNum:10,
   	        pager: '#pager',
   	        pgbuttons: true,
            viewrecords: true,
            height: 430,
            editurl: '../HttpHandler/MessageHandler.ashx?operation=jqGridSavedFilteredMessages',
	        caption: "Frequent Searches"
        });
        jQuery("#list").jqGrid('navGrid',"#pager",{edit:true,add:false,del:true}, { width: 700 });
        //jQuery("#list").jqGrid('inlineNav',"#pager");
    });
    </script>
    
      
</head>
<body>
<form runat="server">
    <div style="float:left">
        <table id="list"><tbody><tr><td/></tr></tbody></table>
        <div id="pager"></div>
    </div>
    
</form>
</body>
</html>
