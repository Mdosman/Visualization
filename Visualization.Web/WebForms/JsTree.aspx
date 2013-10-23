<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JsTree.aspx.cs" Inherits="WebForms_JsTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head id="HEAD1" runat="server">
		<title>Manage Checklist</title>
		<base target="_self" />
		<script src="../Scripts/jstree/_lib/jquery.js" type="text/javascript"></script>
		<script src="../Scripts/jstree/jquery.jstree.js" type="text/javascript"></script>
		<script src="../Scripts/jstree/_lib/jquery.cookie.js" type="text/javascript"></script>		
		<script src="../Scripts/jstree/_lib/jquery.hotkeys.js" type="text/javascript"></script>
        <script type="text/javascript" src="../Scripts/jstree/jquery.json-2.2.js"></script>
        <script type="text/javascript" src="../Scripts/jstree/json2xml.js"></script>
        
		<style type="text/css">
	html, body { margin:0; padding:0; }
	body, td, th, pre, code, select, option, input, textarea { font-family:verdana,arial,sans-serif; font-size:10px; }
	.demo, .demo input, .jstree-dnd-helper, #vakata-contextmenu { font-size:10px; font-family:Verdana; }
	#container { width:850px; margin:10px auto; overflow:hidden; position:relative; }
	#demo { width:auto; height:600px; overflow:auto; border:1px solid gray; }

	#text { margin-top:1px; }

	#alog { font-size:9px !important; margin:5px; border:1px solid silver; }
	</style>
	</head>
	<body>
	  <form id="form1" runat="server">
	  <asp:ScriptManager ID="scriptMgr" runat="server"></asp:ScriptManager>
	  <asp:UpdatePanel ID="updatePanel1" runat="server"  >
	  <ContentTemplate>
	  <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClientClick="GetCheckedItems();" OnClick="btnFilter_Click"/>
	  
        <asp:TextBox ID="txtMessages" runat="server" Width="1200px" TextMode="MultiLine"
            Rows="4"></asp:TextBox>
                    
	  <asp:HiddenField ID="hdnIsPostBack" runat="server" Value="0" />
	  <div>
	     <table align="center">
          <tr>
            <td align="left">
                <asp:Label ID="lblMsgCount" runat="server" Text=""></asp:Label>
            </td>
            <td width="550px"></td> 
            <td>
                <asp:LinkButton ID="lnkUpload" Text="Upload Page" PostBackUrl="~/WebForms/Upload.aspx" runat="server" CssClass="Label"></asp:LinkButton>
            </td>
            <td width="50px"></td>            
            <td>
                <asp:LinkButton ID="lnkDefault" Text="Back to MainPage" PostBackUrl="~/WebForms/Default.aspx" runat="server"></asp:LinkButton>
            </td>
          </tr>
       </table>
	  </div>
	  </ContentTemplate>
	  </asp:UpdatePanel>
	  <div id="container">
            <div id="divFiltersXml" runat="server" visible="False">
            </div>
            <!-- the tree container (notice NOT an UL node) -->
            <div id="demo" class="demo">
            </div>
            <!-- JavaScript neccessary for the tree -->
                <script type="text/javascript">

                    $(function() {
//                        $("#demo").bind("loaded.jstree", function(e, data) {
//                            window.setTimeout(GetCheckedItems, 100);
//                        });
                
                var isPostBack = $("#hdnIsPostBack").val();
                if ( isPostBack == 0 ) {
	                $("#demo")
		                .jstree({ 
			                // the list of plugins to include
			                "plugins" : [ "themes", "json_data", "checkbox" ],
			                // Plugin configuration

			                // I usually configure the plugin that handles the data first - in this case JSON as it is most common
			                "json_data" : { 
				                // I chose an ajax enabled tree - again - as this is most common, and maybe a bit more complex
				                // All the options are the same as jQuery's except for `data` which CAN (not should) be a function
				                "ajax" : {
					                // the URL to fetch the data
					                "url" : "../HttpHandler/MessageHandler.ashx",
					                // this function is executed in the instance's scope (this refers to the tree instance)
					                // the parameter is the node being loaded (may be -1, 0, or undefined when loading the root nodes)
					                "data" : function (n) { 
						                // the result is fed to the AJAX request `data` option
						                return { 
							                "operation" : "get_children", 
							                "nodeType" : n.attr ? n.attr("some-other-attribute") : "Message",
							                "id" : n.attr ? n.attr("id") : 0,
							                "attributeType": n.attr ? n.attr("attributeType") : 0,
                                            "ts": Math.round(new Date().getTime() / 1000)
						                }; 
					                }

				                }
				             }
		                });		                
		             }
                });
                
                function GetCheckedItems() {

                    var events = "";
                    var eventAttributes = "";
                    var eventAttributeSubTypes = "";

                    var eventsArray = [];
                    var eventAttributesArray = [];
                    var eventAttributeSubTypesArray = [];
                    
                    var i = 0; var j=0; var k=0;
                    $('#demo .jstree-checked').each(function() {
                        var node = $(this);
                        var nodeType = node.attr("some-other-attribute");
                        var id = node.attr("id");
                        var attributeType = node.attr("attributeType");
                        if (nodeType == "Event") {
                            events += "'" + id + "', ";
                            eventsArray[i] = id;
                            i++;
                        }
                        else if (nodeType == "EventAttributeType") {
                            var blnFound = false;
                            for (var lp = 0; lp < eventsArray.length; lp++) {
                                if (id == eventsArray[lp]) {
                                    blnFound = true;
                                }
                            }
                            if (!blnFound) {
                                var attributeType = node.attr("attributeType");
                                eventAttributes += id + ":" + attributeType + ", ";
                                eventAttributesArray[j] = attributeType;
                                j++;
                            }
                        }
                        else if (nodeType == "EventAttributeSubType") {
                            var blnFound1 = false;
                            for (var lp = 0; lp < eventsArray.length; lp++) {
                                if (id == eventsArray[lp]) {
                                    blnFound1 = true;
                                }
                            }
                            for (var lp1 = 0; lp1 < eventAttributesArray.length; lp1++) {
                                if (attributeType == eventAttributesArray[lp1]) {
                                    blnFound1 = true;
                                }
                            }
                            if (!blnFound1) {
                                var attributeType = node.attr("attributeType");
                                var attributeSubType = node.attr("attributeSubType");
                                eventAttributeSubTypes += id + ":" + attributeType + ":" + attributeSubType + ", ";
                                eventAttributeSubTypesArray[k] = attributeSubType;
                                k++;
                            }
                        }
                    });

                    $("#txtEventList").val(events);
                    $("#txtEventAttributeTypeList").val(eventAttributes);
                    $("#txtEventAttributeSubTypeList").val(eventAttributeSubTypes);
                }
                </script>
		</div>
      <asp:TextBox ID="txtEventList" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox> 
      <asp:TextBox ID="txtEventAttributeTypeList" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
      <asp:TextBox ID="txtEventAttributeSubTypeList" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>
      <asp:TextBox ID="txtChecklistID" runat="server" Style="position: absolute; left: -500px;" ></asp:TextBox>
      <asp:TextBox ID="txtXml" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox> 
      <asp:TextBox ID="txtJSON" runat="server" Style="position: absolute; left: -500px;"></asp:TextBox>               
	 </form>
	</body>
</html>