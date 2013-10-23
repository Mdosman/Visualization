<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageSavedQuery.aspx.cs" Inherits="WebForms_ManageSavedQuery" %>
<!doctype html>
<html>
<head>
  <meta charset="utf-8">
    <title>Frequent Searches</title>
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
   	        url:'../HttpHandler/MessageHandler.ashx?operation=jqGrid',
	        datatype: "json",
   	        colNames:['ID','QueryName','QueryDescription'],
   	        colModel:[
   		        {name:'ID',index:'ID',key:true, width:25},
   		        {name:'QueryName',index:'QueryName', width:175},
   		        {name:'QueryDescription',index:'QueryDescription', width:500, editable:true, editoptions:{size:100}}			
   	        ],
   	        rowNum:10,
   	        pager: '#pager',
   	        pgbuttons: true,
            viewrecords: true,
            height: 230,
	        editurl: '../HttpHandler/MessageHandler.ashx?operation=jqGrid',
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
