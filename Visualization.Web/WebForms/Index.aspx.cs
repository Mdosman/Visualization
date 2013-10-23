using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Data.OleDb;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Xml.Linq;
using System.Collections.Generic;
using AjaxControlToolkit;

public partial class WebForms_Index : System.Web.UI.Page
{
    int searchCount = 0; int imgCount = 0;
    string folderprop;
    string[] Images; string ImgDir;
    string[] Vehicle_Type = new string[6] { "None", "Sedan", "Semi-truck", "Truck", "Van", "Motorbike" };
    string[] color = new string[11] { "None", "Black", "White", "Red", "Blue", "Green", "Orange", "Yellow", "Brown", "Violet", "Grey" };
    string[] localSpace = new string[7] { "N/A", "Parking_Lot-1", "Parking_Lot-2", "Engineering_Entrance-1", "Engineering_Entrance-2", "Dock_Area", "Circle_Area" };
    string[] refSpace = new string[11] { "None", "Black", "White", "Red", "Blue", "Green", "Orange", "Yellow", "Brown", "Violet", "Grey" };
    string[] cnt = new string[10] { "None", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Others" };
    string[] Speed = new string[4] { "None", "Slow", "Normal", "Fast" };
    string[] V_c_state = new string[6] { "None", "Arriving", "Parking", "Departing", "Engine-On/Off", "Radio-On/Off" };
    string[] V_p_state = new string[8] {"North", "South", "East", "West", "North-East", "North-West", "South-East", "South-West" };
    string[] HVI = new string[7] { "None", "Door-Open", "Door-Close", "Trunk-Open", "Trunk-Close", "Hood-Open", "Hood-Close" };
    string[] snd = new string[6] { "None", "Engine-On", "Engine-Off", "Radio-On", "Radio-Off", "People-Sound" };
    string[] posture = new string[6] { "None", "Standing", "Bending", "Sitting", "Laying-down", "Waving" };
    string[] kinematic = new string[8] { "None", "Stationary", "Walking", "Running", "Jumping", "Crawling", "Pushing", "Pulling" };
    string[] Object_Type = new string[6] { "None", "Glass", "Plastic", "Metal", "Wooden", "Cardboard-Box" };
    string[] Siz = new string[4] { "None", "Small", "Medium", "Large" };
    string[] Object_Shape = new string[5] { "None", "Square", "Long", "Round", "Unknown" };
    string[] Object_State = new string[6] { "None", "Dropped", "Carried", "Taken-from-Vehicle", "Placed-in-Vehicle", "Left-Behind" };
    //string[] Object_State = new string[7] { "None", "Person Dropped", "Person Picked", "Person Carrying", "Taken from Vehicle", "Placed in the Vehicle", "Left Behind" };
    string[] Count = new string[10] { "None", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Others" };
    string[] G_form = new string[6] { "None", "Merging", "Scattering", "Uniting", "Arriving", "Marching" };
    string[] GA_Type = new string[6] { "None", "Loading", "Unloading", "Object-Exchange", "Vehicle-Exchange", "Vehicle-Changing" };
    string[] G_State = new string[6] { "None", "Standing", "Walking", "Running", "Jumping", "Talking" };
    string[] Event_type = new string[6] { "Unknown", "Restricted Zone", "Frequent Event", "State Transition", "Elevated Event", "Restricted Time" };
    string[] Event_Severity = new string[2] { "Normal", "Abnormal" };
    string[] hat = new string[7] { "None", "Sports_Cap", "Military_Hat", "Hard_Hat", "Hat", "Straw_Hats", "Others" };
    string[] Gender = new string[3] { "None", "Male", "Female" };
    string[] Skin_color = new string[11] { "None", "Black", "White", "Red", "Blue", "Green", "Orange", "Yellow", "Brown", "Violet", "Grey" };
    string[] msk = new string[4] { "None", "Mask", "Reading_Glass", "Sun_Glass" };
    string[] mus = new string[5] { "Leader", "Subordinate", "Driver", "Passenger", "Messenger" };
    string[] brd = new string[3] { "None", "Yes", "No" };
    string[] vsnd = new string[12] { "None", "Drived_By", "Arrived", "Engine_On", "Engine_Off", "Departed", "Door_Open", "Door_Close", "Hood_Open", "Hood_Close", "Trunk_Open", "Trunk_Close" };
    string[] hsnd = new string[8] { "None", "Talking", "Yelling", "Walking", "Running", "Knocking", "Talking_on-Phone", "Talking_on_Walkie-Talkie" };
    string[] osnd = new string[22] { "None", "Drived-By", "Arrived", "Engine_On", "Engine_Off", "Departed", "Door_Open", "Door_Close", "Hood_Open", "Hood_Close", "Trunk_Open", "Trunk_Close", "Radio", "Wooden_Box", "Cartoon_Box", "ToolBox", "Glass_Containers", "Plastic_Containers", "Metallic_Parts", "Metallic_Trash_Can", "Sparse_Gun_Shots", "Machine_Gun" };
    string[] esnd = new string[4] { "None", "Wind-Sound", "Thunderstorm-Sound", "Rain-Sound" };
    string[] vehid = new string[11] { "None", "VehId-1", "VehId-2", "VehId-3", "VehId-4", "VehId-5", "VehId-6", "VehId-7", "VehId-8", "VehId-9", "VehId-10" };
    string[] humid = new string[11] { "None", "HumId-1", "HumId-2", "HumId-3", "HumId-4", "HumId-5", "HumId-6", "HumId-7", "HumId-8", "HumId-9", "HumId-10" };
    string[] objid = new string[11] { "None", "ObjId-1", "ObjId-2", "ObjId-3", "ObjId-4", "ObjId-5", "ObjId-6", "ObjId-7", "ObjId-8", "ObjId-9", "ObjId-10" };
    string[] grpid = new string[11] { "None", "GrpId-1", "GrpId-2", "GrpId-3", "GrpId-4", "GrpId-5", "GrpId-6", "GrpId-7", "GrpId-8", "GrpId-9", "GrpId-10" };
    string[] Id = new string[7] { "None", "1", "2", "3", "4", "5", "6" };
    string[] Operations = new string[9] { "Vehicle", "Human", "Object", "Group_Pattern", "Group_Activity", "Event", "Biometric_Features", "Face-Features", "Sound" };
    string[] strMessages = new string[30] {
        "Cam-1, 01/27/10, 11:21:28, 35, 38S MB 43826 78793, 120 30, Vehicle, Sedan, Black, Normal, Parking, South-West, Trunk-Close, 87.5",
        "Cam-1, 01/27/10, 11:21:46, 38, 38S MB 43826 78793, 120 30, Vehicle, Semi-truck, Grey, Fast, Arriving, North-East, Trunk-Open, 90",
        "Cam-1, 01/27/10, 11:22:56, 46, 38S MB 43826 78793, 120 30, Human, Blue, Bending, Crawling, leader, None, None, 90",
        "Cam-1, 01/27/10, 11:23:07, 46, 38S MB 43826 78793, 120 30, Human, Grey, Waving, Walking, Passenger, None, None, 90",
        "Cam-1, 01/27/10, 11:23:28, 49, 38S MB 43826 78793, 120 30, Human, Black, none, Walking, Sedan, None, None, 90",
        "Cam-1, 01/27/10, 11:24:38, 50, 38S MB 43826 78793, 120 30, Group Pattern, Merging, Walking, 3, None, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:03, 51, 38S MB 43826 78793, 120 30, Group Pattern, Uniting, Standing, 3, 3, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:47, 54, 38S MB 43826 78793, 120 30, Group Pattern, Scattering, Running, 3, 3, 1, None, 87.5",
        "Cam-1, 01/27/10, 11:26:42, 67, 38S MB 43826 78793, 120 30, Group Activity, Object-Exchange, Abnormal, Elevated Event, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:27:10, 71, 38S MB 43826 78793, 120 30, Group Activity, Vehicle-Exchange, Normal, Restricted Time, 2, Sedan, Medium Weight, 90",
        "Cam-1, 01/27/10, 11:27:43, 81, 38S MB 43826 78793, 120 30, Group Activity, Vehicle-Changing, Abnormal, Frequent Event, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:28:37, 114, 38S MB 43826 78793, 120 30, Object, Black, Metal, Round, Large, Taken-from-Vehicle, Plastic, 90",
        "Cam-1, 01/27/10, 11:29:10, 148, 38S MB 43826 78793, 120 30, Object, Green, Wooden, Unknown, Medium, Carried, Light Weight, 90",
        "Cam-1, 01/27/10, 11:29:41, 186, 38S MB 43826 78793, 120 30, Object, Yellow, Cardboard-Box, Square, Small, Dropped, Metal, 90",
        "Cam-1, 01/27/10, 11:30:39, 264, 38S MB 43826 78793, 120 30, Vehicle, Van, White, Fast, Departing, North, Door-Close, 90", 
        "Cam-1, 01/27/10, 11:21:28, 35, 38S MB 43826 78793, 120 30, Vehicle, Truck, Red, Normal, Engine-On/Off, South, Hood-Close, 87.5",
        "Cam-1, 01/27/10, 11:21:46, 38, 38S MB 43826 78793, 120 30, Vehicle, Semi-truck, Brown, Slow, Radio-On/Off, West, Door-Close, 90",
        "Cam-1, 01/27/10, 11:22:56, 46, 38S MB 43826 78793, 120 30, Human, White, Sitting, Pulling, Passenger, None, None, 90",
        "Cam-1, 01/27/10, 11:23:07, 46, 38S MB 43826 78793, 120 30, Human, Green, Standing, Pushing, Driver, None, None, 90",
        "Cam-1, 01/27/10, 11:23:28, 49, 38S MB 43826 78793, 120 30, Human, Orange, Laying-down, Stationary, Subordinate, None, None, 90",
        "Cam-1, 01/27/10, 11:24:38, 50, 38S MB 43826 78793, 120 30, Group Pattern, Marching, Talking, 3, None, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:03, 51, 38S MB 43826 78793, 120 30, Sound, Engine-On, Wind-Sound, Standing, 3, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:47, 54, 38S MB 43826 78793, 120 30, Sound, People-Sound, Rain-Sound, Talking, 3, 1, None, 87.5",
        "Cam-1, 01/27/10, 11:26:42, 67, 38S MB 43826 78793, 120 30, Sound, Radio-Off, Thunderstorm-Sound, None, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:27:10, 71, 38S MB 43826 78793, 120 30, Group Activity, Unloading, Normal, Restricted Zone, 2, Sedan, Medium Weight, 90",
        "Cam-1, 01/27/10, 11:27:43, 81, 38S MB 43826 78793, 120 30, Group Activity, loading, Abnormal, Frequent Event, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:28:37, 114, 38S MB 43826 78793, 120 30, Group Activity, Vehicle-Changing, Normal, State Transition, 1, Sedan, Plastic, 90",
        "Cam-1, 01/27/10, 11:29:10, 148, 38S MB 43826 78793, 120 30, Object, Black, Glass, Square, Small, Dropped, Light Weight, 90",
        "Cam-1, 01/27/10, 11:29:41, 186, 38S MB 43826 78793, 120 30, Object, White, Plastic, Long, Medium, Left-Behind, Metal, 90",
        "Cam-1, 01/27/10, 11:30:39, 264, 38S MB 43826 78793, 120 30, Vehicle, Motorbike, Yellow, Fast, Departing, North-West, None, 90"
    };
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVehicleColorList();
            LoadVehicleTypeList();
            LoadVehicleSpeedList();
            LoadVehicleStateList();
            LoadVehicleMovingDirectionList();
            LoadVehicleInteractionList();
            LoadHumanClothColorList();
            LoadHumanPostureList();
            LoadHumanKinematicsList();
            LoadHumanSocialRoleList();
            LoadObjectColorList();
            LoadObjectTypeList();
            LoadObjectShapeList();
            LoadObjectSizeList();
            LoadObjectStateList();
            LoadGroupFormationList();
            LoadGroupStatesList();
            LoadGATypeList();
            LoadGAStatusList();
            LoadGAReasonList();
            LoadIntSndList();
            LoadEnvSndList();

            CreateMessages();

            LoadPlaceHolder1CheckBoxes();
        }
    }


    private void LoadPlaceHolder1CheckBoxes()
    {
        string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
        DataSet ds= new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
        {
            using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventTypes", objCon))
            {
                objCon.Open();
                objOleDbCmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = objOleDbCmd;
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr[0].ToString();
                    li.Value = dr[0].ToString();
                    chkEventTypes.Items.Add(li);
                    //CheckBox box = new CheckBox();
                    //box.Text = dr[0].ToString();
                    //box.AutoPostBack = true;
                    //box.CheckedChanged += new EventHandler(this.EventTypeCheckedChange);
                    //PlaceHolder1.Controls.Add(box);
                }
            }
        }

        TabPanel FirstTab = new TabPanel();
        FirstTab.ID = "Event1";
        FirstTab.HeaderText = "Event1";

        TabPanel SecondTab = new TabPanel();
        SecondTab.ID = "Event2";
        SecondTab.HeaderText = "Event2";

        Label lbl = new Label();
        lbl.Text = "Event1";
        FirstTab.Controls.Add(lbl);
        Label lbl2 = new Label();
        lbl2.Text = "Event2";
        SecondTab.Controls.Add(lbl2);
        this.tabContainer4.Tabs.Add(FirstTab);
        this.tabContainer4.Tabs.Add(SecondTab);
        tabContainer4.ActiveTabIndex = 0;
    }

    protected void EventTypeCheckedChange(object sender, EventArgs e)
    {
        CheckBoxList x = (CheckBoxList)sender;
    }

    protected void LoadVehicleColorList()
    {
        chkListVehicleColor.Items.Clear();
        for (int i = 0; i < color.Length; i++)
        {
            ListItem li = new ListItem(color[i]);
            chkListVehicleColor.Items.Add(li);
        }
    }

    protected void LoadVehicleTypeList()
    {
        this.chkListVehicleType.Items.Clear();
        for (int i = 0; i < Vehicle_Type.Length; i++)
        {
            ListItem li = new ListItem(Vehicle_Type[i]);
            chkListVehicleType.Items.Add(li);
        }
    }

    protected void LoadVehicleSpeedList()
    {
        this.chkListVehicleSpeed.Items.Clear();
        for (int i = 0; i < this.Speed.Length; i++)
        {
            ListItem li = new ListItem(Speed[i]);
            chkListVehicleSpeed.Items.Add(li);
        }
    }

    protected void LoadVehicleStateList()
    {
        this.chkListVehicleState.Items.Clear();
        for (int i = 0; i < this.V_c_state.Length; i++)
        {
            ListItem li = new ListItem(V_c_state[i]);
            chkListVehicleState.Items.Add(li);
        }
    }

    protected void LoadVehicleMovingDirectionList()
    {
        this.chkListVehicleMovingDirection.Items.Clear();
        for (int i = 0; i < this.V_p_state.Length; i++)
        {
            ListItem li = new ListItem(V_p_state[i]);
            chkListVehicleMovingDirection.Items.Add(li);
        }
    }

    protected void LoadVehicleInteractionList()
    {
        this.chkListVehicleInteraction.Items.Clear();
        for (int i = 0; i < this.HVI.Length; i++)
        {
            ListItem li = new ListItem(HVI[i]);
            chkListVehicleInteraction.Items.Add(li);
        }
    }

    protected void LoadHumanClothColorList()
    {
        this.chkListHumanClothColor.Items.Clear();
        for (int i = 0; i < this.Skin_color.Length; i++)
        {
            ListItem li = new ListItem(Skin_color[i]);
            chkListHumanClothColor.Items.Add(li);
        }
    }

    protected void LoadHumanPostureList()
    {
        this.chkListHumanPosture.Items.Clear();
        for (int i = 0; i < this.posture.Length; i++)
        {
            ListItem li = new ListItem(posture[i]);
            chkListHumanPosture.Items.Add(li);
        }
    }

    protected void LoadHumanKinematicsList()
    {
        this.chkListHumanKinematics.Items.Clear();
        for (int i = 0; i < this.kinematic.Length; i++)
        {
            ListItem li = new ListItem(kinematic[i]);
            chkListHumanKinematics.Items.Add(li);
        }
    }

    protected void LoadHumanSocialRoleList()
    {
        this.chkListHumanSocialRole.Items.Clear();
        for (int i = 0; i < this.mus.Length; i++)
        {
            ListItem li = new ListItem(mus[i]);
            chkListHumanSocialRole.Items.Add(li);
        }
    }

    protected void LoadObjectColorList()
    {
        this.chkListObjectColor.Items.Clear();
        for (int i = 0; i < this.refSpace.Length; i++)
        {
            ListItem li = new ListItem(refSpace[i]);
            chkListObjectColor.Items.Add(li);
        }
    }

    protected void LoadObjectTypeList()
    {
        this.chkListObjectType.Items.Clear();
        for (int i = 0; i < this.Object_Type.Length; i++)
        {
            ListItem li = new ListItem(Object_Type[i]);
            chkListObjectType.Items.Add(li);
        }
    }

    protected void LoadObjectShapeList()
    {
        this.chkListObjectShape.Items.Clear();
        for (int i = 0; i < this.Object_Shape.Length; i++)
        {
            ListItem li = new ListItem(Object_Shape[i]);
            chkListObjectShape.Items.Add(li);
        }
    }

    protected void LoadObjectSizeList()
    {
        this.chkListObjectSize.Items.Clear();
        for (int i = 0; i < this.Siz.Length; i++)
        {
            ListItem li = new ListItem(Siz[i]);
            chkListObjectSize.Items.Add(li);
        }
    }

    protected void LoadObjectStateList()
    {
        this.chkListObjectState.Items.Clear();
        for (int i = 0; i < this.Object_State.Length; i++)
        {
            ListItem li = new ListItem(Object_State[i]);
            chkListObjectState.Items.Add(li);
        }
    }

    protected void LoadGroupFormationList()
    {
        this.chkListGroupFormation.Items.Clear();
        for (int i = 0; i < this.G_form.Length; i++)
        {
            ListItem li = new ListItem(G_form[i]);
            chkListGroupFormation.Items.Add(li);
        }
    }

    protected void LoadGroupStatesList()
    {
        this.chkListGroupStates.Items.Clear();
        for (int i = 0; i < this.G_State.Length; i++)
        {
            ListItem li = new ListItem(G_State[i]);
            chkListGroupStates.Items.Add(li);
        }
    }

    protected void LoadGATypeList()
    {
        this.chkListGroupActivityType.Items.Clear();
        for (int i = 0; i < this.GA_Type.Length; i++)
        {
            ListItem li = new ListItem(GA_Type[i]);
            chkListGroupActivityType.Items.Add(li);
        }
    }

    protected void LoadGAStatusList()
    {
        this.chkListActivityStatus.Items.Clear();
        for (int i = 0; i < this.Event_Severity.Length; i++)
        {
            ListItem li = new ListItem(Event_Severity[i]);
            chkListActivityStatus.Items.Add(li);
        }
    }

    protected void LoadGAReasonList()
    {
        this.chkListActivityStatusReason.Items.Clear();
        for (int i = 0; i < this.Event_type.Length; i++)
        {
            ListItem li = new ListItem(Event_type[i]);
            chkListActivityStatusReason.Items.Add(li);
        }
    }

    protected void LoadIntSndList()
    {
        this.chkListInteractionSound.Items.Clear();
        for (int i = 0; i < this.snd.Length; i++)
        {
            ListItem li = new ListItem(snd[i]);
            chkListInteractionSound.Items.Add(li);
        }
    }

    protected void LoadEnvSndList()
    {
        this.chkListEnvironmentalSound.Items.Clear();
        for (int i = 0; i < this.esnd.Length; i++)
        {
            ListItem li = new ListItem(esnd[i]);
            chkListEnvironmentalSound.Items.Add(li);
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        FilterMessages();
    }

    protected void CreateMessages()
    {
        for (int i = 0; i < strMessages.Length; i++)
        {
            txtMessages.Text += strMessages[i] + "\r\n";
        }
        this.lblMsgCount.Text = strMessages.Length.ToString();

    }

    private void FilterMessages()
    {
        txtMessages.Text = string.Empty;
        string[] strMsg;
        bool blnCheck = false;
        int iMsgCount = 0;
        for (int i = 0; i < strMessages.Length; i++)
        {
            strMsg = strMessages[i].Split(',');
            if (chkVehicle.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Vehicle")
                {
                    if (FilterVehicle(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            if (chkHuman.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Human")
                {
                    if (FilterHuman(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            if (chkObject.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Object")
                {
                    if (FilterObject(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            if (chkGroupPattern.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Group Pattern")
                {
                    if (FilterGroupPattern(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            if (chkGroupActivity.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Group Activity")
                {
                    if (FilterGroupActivity(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            if (chkSound.Checked)
            {
                blnCheck = true;
                if (strMsg[6].Trim() == "Sound")
                {
                    if (FilterSound(strMsg))
                    {
                        txtMessages.Text += strMessages[i] + "\r\n";
                        iMsgCount++;
                    }
                }
            }
            //Show all messages if no checkbox is checked.
            if (blnCheck == false)
            {
                txtMessages.Text += strMessages[i] + "\r\n";
                iMsgCount++;
            }

        }
        lblMsgCount.Text = iMsgCount.ToString();
    }

    private bool FilterVehicle(string[] strMessageLine)
    {
        List<string> listVehicleColor = new List<string>();
        List<string> listVehicleType = new List<string>();
        List<string> listVehicleSpeed = new List<string>();
        List<string> listVehicleState = new List<string>();
        List<string> listMovingDirection = new List<string>();
        List<string> listVehicleInteraction = new List<string>();

        foreach (ListItem li in chkListVehicleColor.Items)
        {
            if (li.Selected)
            {
                listVehicleColor.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListVehicleType.Items)
        {
            if (li.Selected)
            {
                listVehicleType.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListVehicleSpeed.Items)
        {
            if (li.Selected)
            {
                listVehicleSpeed.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListVehicleState.Items)
        {
            if (li.Selected)
            {
                listVehicleState.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListVehicleMovingDirection.Items)
        {
            if (li.Selected)
            {
                listMovingDirection.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListVehicleInteraction.Items)
        {
            if (li.Selected)
            {
                listVehicleInteraction.Add(li.Value);
            }
        }

        if (listVehicleColor.Count() > 0)
        {
            if (!listVehicleColor.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }

        if (listVehicleType.Count() > 0)
        {
            if (!listVehicleType.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listVehicleSpeed.Count() > 0)
        {
            if (!listVehicleSpeed.Contains(strMessageLine[9].Trim()))
            {
                return false;
            }
        }

        if (listVehicleState.Count() > 0)
        {
            if (listVehicleState.Contains(strMessageLine[10].Trim()))
            {
                return false;
            }
        }

        if (listMovingDirection.Count() > 0)
        {
            if (!listMovingDirection.Contains(strMessageLine[11].Trim()))
            {
                return false;
            }
        }

        if (listVehicleInteraction.Count() > 0)
        {
            if (!listVehicleInteraction.Contains(strMessageLine[12].Trim()))
            {
                return false;
            }
        }
        return true;

    }

    private bool FilterHuman(string[] strMessageLine)
    {
        List<string> listHumanClothColor = new List<string>();
        List<string> listHumanPosture = new List<string>();
        List<string> listHumanKinematics = new List<string>();
        List<string> listHumanSocialRole = new List<string>();

        foreach (ListItem li in chkListHumanClothColor.Items)
        {
            if (li.Selected)
            {
                listHumanClothColor.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListHumanPosture.Items)
        {
            if (li.Selected)
            {
                listHumanPosture.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListHumanKinematics.Items)
        {
            if (li.Selected)
            {
                listHumanKinematics.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListHumanSocialRole.Items)
        {
            if (li.Selected)
            {
                listHumanSocialRole.Add(li.Value);
            }
        }



        if (listHumanClothColor.Count() > 0)
        {
            if (!listHumanClothColor.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listHumanPosture.Count() > 0)
        {
            if (!listHumanPosture.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }

        if (listHumanKinematics.Count() > 0)
        {
            if (!listHumanKinematics.Contains(strMessageLine[9].Trim()))
            {
                return false;
            }
        }

        if (listHumanSocialRole.Count() > 0)
        {
            if (listHumanSocialRole.Contains(strMessageLine[10].Trim()))
            {
                return false;
            }
        }

        return true;

    }

    private bool FilterObject(string[] strMessageLine)
    {
        List<string> listObjectColor = new List<string>();
        List<string> listObjectType = new List<string>();
        List<string> listObjectShape = new List<string>();
        List<string> listObjectSize = new List<string>();
        List<string> listObjectState = new List<string>();

        foreach (ListItem li in this.chkListObjectColor.Items)
        {
            if (li.Selected)
            {
                listObjectColor.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListObjectType.Items)
        {
            if (li.Selected)
            {
                listObjectType.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListObjectShape.Items)
        {
            if (li.Selected)
            {
                listObjectShape.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListObjectSize.Items)
        {
            if (li.Selected)
            {
                listObjectSize.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListObjectState.Items)
        {
            if (li.Selected)
            {
                listObjectState.Add(li.Value);
            }
        }

        if (listObjectColor.Count() > 0)
        {
            if (!listObjectColor.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listObjectType.Count() > 0)
        {
            if (!listObjectType.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }

        if (listObjectShape.Count() > 0)
        {
            if (!listObjectShape.Contains(strMessageLine[9].Trim()))
            {
                return false;
            }
        }

        if (listObjectSize.Count() > 0)
        {
            if (listObjectSize.Contains(strMessageLine[10].Trim()))
            {
                return false;
            }
        }

        if (listObjectState.Count() > 0)
        {
            if (!listObjectState.Contains(strMessageLine[11].Trim()))
            {
                return false;
            }
        }
        return true;

    }

    private bool FilterGroupPattern(string[] strMessageLine)
    {
        List<string> listGroupFormation = new List<string>();
        List<string> listGroupStates = new List<string>();

        foreach (ListItem li in this.chkListGroupFormation.Items)
        {
            if (li.Selected)
            {
                listGroupFormation.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListGroupStates.Items)
        {
            if (li.Selected)
            {
                listGroupStates.Add(li.Value);
            }
        }

        if (listGroupFormation.Count() > 0)
        {
            if (!listGroupFormation.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listGroupStates.Count() > 0)
        {
            if (!listGroupStates.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }
        return true;

    }

    private bool FilterGroupActivity(string[] strMessageLine)
    {
        List<string> listGroupActivityType = new List<string>();
        List<string> listActivityStatus = new List<string>();
        List<string> listActivityStatusReason = new List<string>();

        foreach (ListItem li in this.chkListGroupActivityType.Items)
        {
            if (li.Selected)
            {
                listGroupActivityType.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListActivityStatus.Items)
        {
            if (li.Selected)
            {
                listActivityStatus.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListActivityStatusReason.Items)
        {
            if (li.Selected)
            {
                listActivityStatusReason.Add(li.Value);
            }
        }

        if (listGroupActivityType.Count() > 0)
        {
            if (!listGroupActivityType.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listActivityStatus.Count() > 0)
        {
            if (!listActivityStatus.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }

        if (listActivityStatusReason.Count() > 0)
        {
            if (!listActivityStatusReason.Contains(strMessageLine[9].Trim()))
            {
                return false;
            }
        }
        return true;

    }

    private bool FilterSound(string[] strMessageLine)
    {
        List<string> listInteractionSound = new List<string>();
        List<string> listEnvironmentalSound = new List<string>();

        foreach (ListItem li in this.chkListInteractionSound.Items)
        {
            if (li.Selected)
            {
                listInteractionSound.Add(li.Value);
            }
        }

        foreach (ListItem li in this.chkListEnvironmentalSound.Items)
        {
            if (li.Selected)
            {
                listEnvironmentalSound.Add(li.Value);
            }
        }

        if (listInteractionSound.Count() > 0)
        {
            if (!listInteractionSound.Contains(strMessageLine[7].Trim()))
            {
                return false;
            }
        }

        if (listEnvironmentalSound.Count() > 0)
        {
            if (!listEnvironmentalSound.Contains(strMessageLine[8].Trim()))
            {
                return false;
            }
        }

        return true;

    }
}
