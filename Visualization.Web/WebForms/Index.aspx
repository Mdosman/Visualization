<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="WebForms_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index Page</title>
    <%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />--%>
    <link href="../Styles/jquery-ui-1.8.13.custom.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>

    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script src="../Scripts/jQuery/jquery.dynDateTime.min.js" type="text/javascript"></script>

    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>

    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Default.js" type="text/javascript"></script>
    <link href="../Styles/Base.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
      $(function() {
          $( "#accordion" ).accordion();
      });  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="accordion">
        <h3>Section 1</h3>
        <div>
           <table>
            <tr>
                <td colspan="11">
                    <asp:TextBox ID="txtMessages" runat="server" Width="1100px" TextMode="MultiLine"
                        Rows="3"></asp:TextBox>
                    <asp:Label ID="lblMsgCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCam" runat="server" CssClass="Label">Cam</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCam" runat="server" Width="70px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td width="10px">
                </td>
                <td align="right">
                    <asp:Label ID="lblDate" runat="server" CssClass="Label">Date</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="110px" CssClass="TextBox"></asp:TextBox>
                    <img src="../images/calender.png" />
                </td>
                <td width="10px">
                </td>
                <td align="right">
                    <asp:Label ID="lblLocation" runat="server" CssClass="Label">Location</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server" Width="120px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="lnkFilters" Text="Filters" OnClientClick="return false;" runat="server" CssClass="Label"></asp:LinkButton>
                    <%--<asp:LinkButton ID="lnkClearFilters" Text="Filters" OnClientClick="return false;" runat="server"></asp:LinkButton>--%>
                </td>
                <td>
                    <asp:LinkButton ID="lnkUpload" Text="Upload" PostBackUrl="~/WebForms/Upload.aspx" runat="server" CssClass="Label"></asp:LinkButton>
                </td>
            </tr>  
        </table>
        </div>
        <h3>
            Section 2</h3>
        <div>
            <table>
            <tr>
                <td>
                    <asp:CheckBox ID="chkVehicle" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblVehicle" runat="server">Vehicle</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkHuman" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblHuman" runat="server">Human</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkObject" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblObject" runat="server">Object</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkGroupPattern" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblGroupPattern" runat="server">Group Pattern</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkGroupActivity" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblGroupActivity" runat="server">Group Activity</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkSound" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblSound" runat="server">Sound</asp:Label>
                </td>
            </tr>
             <tr>
                <td colspan="6">
                    <ajaxToolkit:TabContainer runat="server" ID="filterTabs" Width="1000" ScrollBars="Auto"
                        Height="150">
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelVehicle" HeaderText="Vehicle">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleColor" GroupingText="Vehicle Color" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleColor" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleType" GroupingText="Vehicle Type" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleType" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleSpeed" GroupingText="Vehicle Speed" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleSpeed" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleState" GroupingText="Vehicle State" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleState" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleMovingDirection" GroupingText="Moving Direction" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleMovingDirection" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlVehicleInteraction" GroupingText="Vehicle Interaction" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListVehicleInteraction" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelHuman" HeaderText="Human">
                            <ContentTemplate>
                                 <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlHumanClothColor" GroupingText="Cloth Color" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListHumanClothColor" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlHumanPosture" GroupingText="Human Posture" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListHumanPosture" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlHumanKinematics" GroupingText="Human Kinematics" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListHumanKinematics" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlSocialRole" GroupingText="Social Role" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListHumanSocialRole" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelObject" HeaderText="Object">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlObjectColor" GroupingText="Object Color" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListObjectColor" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlObjectType" GroupingText="Object Type" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListObjectType" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlObjectShape" GroupingText="Object Shape" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListObjectShape" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                      
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlObjectSize" GroupingText="Object Size" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListObjectSize" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                       
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlObjectState" GroupingText="Object State" runat="server" Width="750">
                                                <table>
                                                      <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListObjectState" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>                                          
                                            </asp:Panel>
                                        </td>                                                        
                                      </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelGroupPattern" HeaderText="Group Pattern">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlGroupFormation" GroupingText="Group Formation" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListGroupFormation" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                   
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlGroupStates" GroupingText="Group States" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListGroupStates" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelGroupActivity" HeaderText="Group Activity" >
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlGroupActivityType" GroupingText="Activity Type" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListGroupActivityType" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                    
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlActivityStatus" GroupingText="Activity Status" runat="server" Width="750">
                                                <table>
                                                      <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListActivityStatus" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                                                                 
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlActivityStatusReason" GroupingText="Status Reason" runat="server" Width="750">
                                                <table>
                                                      <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListActivityStatusReason" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel runat="server" ID="tabPanelSound" HeaderText="Sound">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlInteractionSound" GroupingText="Interaction Sound" runat="server" Width="750">
                                                <table>
                                                    <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListInteractionSound" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>                                                   
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlEnvironmentalSound" GroupingText="Environmental Sound" runat="server" Width="750">
                                                <table>
                                                     <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="chkListEnvironmentalSound" runat="server"  RepeatDirection="Horizontal" ></asp:CheckBoxList>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
        </table>
        </div>
        <h3>
            Section 3</h3>
        <div>
        
             <asp:Panel ID="Panel1" GroupingText="Event Types" runat="server" Width="550" align="center">
                <asp:CheckBoxList ID="chkEventTypes" runat="server" AutoPostBack="true" CssClass="Label"
                 RepeatDirection="Horizontal" OnSelectedIndexChanged="EventTypeCheckedChange">                
                </asp:CheckBoxList>
                
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
             </asp:Panel>
        </div>
        <h3>
            Section 4</h3>
        <div>
            <ajaxToolkit:TabContainer runat="server" ID="tabContainer4" Width="1000" ScrollBars="Auto"
                        Height="150">
            </ajaxToolkit:TabContainer>
        </div>
    </div>
    </form>
</body>
</html>
