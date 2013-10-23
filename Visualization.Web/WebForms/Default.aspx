<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebForms_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="diagFilters">
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
                    <ajaxToolkit:TabContainer runat="server" ID="filterTabs" Width="770" ScrollBars="Auto"
                        Height="320">
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
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtFilteredMessages" runat="server" Width="970px" TextMode="MultiLine"
                        Rows="6"></asp:TextBox>
                    <asp:Label ID="lblFIlteredMessagesCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="border: 2px solid #a1a1a2; height: 800px; width: 1240px">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMessages" runat="server" Text="Messages"></asp:Label>
                </td>
                <td colspan="10">
                    <asp:TextBox ID="txtMessages" runat="server" Width="1120px" TextMode="MultiLine"
                        Rows="3"></asp:TextBox>
                    <asp:Label ID="lblMsgCount" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCam" runat="server">Cam</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCam" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td width="10px">
                </td>
                <td align="right">
                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="120px"></asp:TextBox>
                    <img src="../images/calender.png" />
                </td>
                <td width="10px">
                </td>
                <td align="right">
                    <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="lnkFilters" Text="Filters" OnClientClick="return false;" runat="server"></asp:LinkButton>
                    <%--<asp:LinkButton ID="lnkClearFilters" Text="Filters" OnClientClick="return false;" runat="server"></asp:LinkButton>--%>
                </td>
                <td>
                    <asp:LinkButton ID="lnkUpload" Text="Upload" PostBackUrl="~/WebForms/Upload.aspx" runat="server"></asp:LinkButton>
                </td>
            </tr>  
        </table>
        <table>
         <tr>
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="displaytabls" >
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelMap" HeaderText="Map">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                         <asp:TextBox ID="TextBox1" runat="server" Width="1200px" TextMode="MultiLine"
                                            Rows="40"></asp:TextBox>                                       
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelGraph" HeaderText="Graph">
                        <ContentTemplate>
                             <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                         <asp:TextBox ID="TextBox4" runat="server" Width="1200px" TextMode="MultiLine"
                                            Rows="40"></asp:TextBox>                                       
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
   <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click"  />
    </form>
</body>
</html>
