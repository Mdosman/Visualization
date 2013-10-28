<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="WebForms_Chart" %>

<!DOCTYPE html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>PSS-VA</title>
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
        .demo, .demo input, .jstree-dnd-helper, #vakata-contextmenu, select, option, input, a, span, textarea
        {
            font-size: 10px;
            font-family: Verdana;
        }
    </style>
</head>
<body>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAuyiTAfNDyoRQybx_jy0FyAMiPVj1E92A&sensor=true">
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server">
        <Scripts>
            <asp:ScriptReference Path="../Scripts/jQuery/jquery-latest.js" />
            <asp:ScriptReference Path="../Scripts/jQuery/jquery-ui-latest.js" />
            <asp:ScriptReference Path="../Scripts/jQuery/jquery.layout-latest.js" />
            <asp:ScriptReference Path="../Scripts/Chart.js" />
            <asp:ScriptReference Path="../Scripts/jstree/jquery.jstree.js" />
            <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.cookie.js" />
            <asp:ScriptReference Path="../Scripts/jstree/_lib/jquery.hotkeys.js" />
            <asp:ScriptReference Path="../Scripts/jBarGraph.js" />
            <asp:ScriptReference Path="../Scripts/MarkerClusterer.js" />
            <asp:ScriptReference Path="../Scripts/highcharts/highcharts.js" />
            <asp:ScriptReference Path="../Scripts/highcharts/highstock.js" />
        </Scripts>
    </asp:ScriptManager>
    <!-- Additional files for the Highslide popup effect -->
    <div class="outer-north center">
        <asp:TextBox ID="txtMessages" runat="server" Width="100%" TextMode="MultiLine" Rows="8"
            Style="font-size: 10px; font-family: Verdana;"></asp:TextBox>
        <asp:HiddenField ID="hdnIsPostBack" runat="server" Value="0" />
        <asp:HiddenField ID="txtProcessId" runat="server" Value="0" />
        <asp:HiddenField ID="txtProcessSubTypeId" runat="server" Value="0" />
        <asp:TextBox ID="txtLatLon" Style="position: absolute; left: -500px" runat="server"></asp:TextBox>
        <table align="center">
            <tr>
                <td>
                    <asp:LinkButton ID="lnkDefault" Text="Filter Options" PostBackUrl="~/WebForms/Default.aspx"
                        runat="server"></asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkUpload" Text="Upload Page" PostBackUrl="~/WebForms/Upload.aspx"
                        runat="server" CssClass="Label"></asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkImages" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewImages(); return false;">Images</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkNodeGraph" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewNodeGraph(); return false;">NodeGraph</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkBarChart" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewBarChart(); return false;">BarChart</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkMappingPage" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewMappingPage(); return false;">Process</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkPCA" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewPCA(); return false;">PCA</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkLDA" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewLDA(); return false;">LDA</asp:LinkButton>
                </td>
                <td width="20px">
                </td>
                <td>
                    <asp:LinkButton ID="lnkVSM" runat="server" CssClass="Label" CausesValidation="false"
                        OnClientClick="viewVSM(); return false;">VSM</asp:LinkButton>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td align="left">
                    <asp:Label ID="lblParseResults" runat="server" Text="Parse the results further with comma seperated list."></asp:Label>
                </td>
                <td width="50px">
                </td>
                <td>
                    <asp:TextBox ID="txtParseInput" runat="server" Width="500px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td width="50px">
                </td>
                <td>
                    <input id="btnParseResults" type="button" value="Parse Results" onclick="ParseResults();" />
                </td>
                <td width="50px">
                </td>
                <td align="right">
                    <asp:Label ID="lblMsgCount" runat="server" Text="Count: 0"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="outer-center">
        <!-- Middle Layout Container -->
        <div id="map-canvas" style="width: 1150px; height: 750px">
        </div>
    </div>
    <div class="outer-east">
        <div class="east-center">
            <!-- Inner-North Layout Container -->
            <div>
                <fieldset>
                    <legend style="font-size: 10px; font-family: Verdana;">Process</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server">Process</asp:Label>
                                <asp:DropDownList ID="ddlProcess" runat="server" CssClass="TextBox">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server">Process SubType</asp:Label>
                                <asp:DropDownList ID="ddlProcessSubType" runat="server" CssClass="TextBox">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <br />
            <br />
            <div>
                <input id="txtQueryName" type="text" />
                <input id="btnSaveQuery" type="button" value="Save Query" onclick="SaveQuery();" />
            </div>
            <div id="container">
                <fieldset>
                    <!-- the tree container (notice NOT an UL node) -->
                    <div id="demo" class="demo">
                    </div>
                </fieldset>
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
            <fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSavedQuery" runat="server">Saved Query</asp:Label>
                            <asp:LinkButton ID="lnkManageSavedQueries" runat="server" CssClass="Label" CausesValidation="false"
                                OnClientClick="ManageSavedQueries(); return false;">Edit</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlSQLQuery" runat="server" CssClass="TextBox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server">Source</asp:Label>
                            <asp:DropDownList ID="ddlCam" runat="server" CssClass="TextBox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server">TML</asp:Label>
                            <asp:DropDownList ID="ddlTML" runat="server" CssClass="TextBox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="font-size: 10px; font-family: Verdana;">MM/DD/YY HH:MM:SS</legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server">Date(From)</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventDateFrom" runat="server" Width="110px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server">Date(To)</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEventDateTo" runat="server" Width="110px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            
            <br />
            <br />
            <div>
                <input id="txtSaveFilteredMessagesName" type="text" />
                <input id="Button1" type="button" value="Save Filtered Msgs" onclick="SaveFilteredMessages();" />
                <asp:LinkButton ID="lnkManageSavedFilteredMessages" runat="server" CssClass="Label" CausesValidation="false"
                    OnClientClick="ManageSavedFilteredMessages(); return false;">Edit</asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
