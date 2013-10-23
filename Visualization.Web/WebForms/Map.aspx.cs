using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForms_Map : System.Web.UI.Page
{
    int searchCount = 0; int imgCount = 0;
    string folderprop;
    string[] Images; string ImgDir;
    string[] Vehicle_Type = new string[6] { "None", "Sedan", "Semi_truck", "Truck", "Van", "Motorbike" };
    string[] color = new string[11] { "None", "Black", "White", "Red", "Blue", "Green", "Orange", "Yellow", "Brown", "Violet", "Grey" };
    string[] localSpace = new string[7] { "N/A", "Parking_Lot-1", "Parking_Lot-2", "Engineering_Entrance-1", "Engineering_Entrance-2", "Dock_Area", "Circle_Area" };
    string[] refSpace = new string[7] { "None", "D_S_Front_Door", "P_S_Front_Door", "D_S_Back_Door", "P_S_Back_Door", "Trunk", "Hood" };
    string[] cnt = new string[10] { "None", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Others" };
    string[] Speed = new string[4] { "None", "Slow", "Normal", "Fast" };
    string[] V_c_state = new string[6] { "None", "Arriving", "Parking", "Departing", "Engine_On", "Radio_On" };
    string[] V_p_state = new string[6] { "None", "Arrived", "Parked", "Departed", "Engine_Off", "Radio_Off" };
    string[] HVI = new string[7] { "None", "Door_Open", "Door_Close", "Trunk_Open", "Trunk_Close", "Hood_Open", "Hood_Close" };
    string[] snd = new string[6] { "None", "Engine_On", "Engine_Off", "Radio_On", "Radio_Off", "People_Sound" };
    string[] posture = new string[5] { "None", "Standing", "Bending", "Sitting", "Laying_down" };
    string[] kinematic = new string[8] { "None", "Stationary", "Walking", "Running", "Jumping", "Crawling", "Pushing", "Pulling" };
    string[] Object_Type = new string[9] { "None", "Glass", "Plastic", "Metal", "Wooden", "Cardboard_Box", "Light_Weight", "Medium_Weight", "Heavy_Weight" };
    string[] Siz = new string[4] { "None", "Small", "Medium", "Large" };
    string[] Object_Shape = new string[5] { "None", "Square_Shape", "Long_Shape", "Round_Shape", "Unknown_Shape" };
    string[] Object_State = new string[7] { "None", "Dropped_by_Person", "Picked_by_Person", "Carried_by_Person", "Taken_from_Vehicle", "Placed_in_the_Vehicle", "Left_Behind" };
    //string[] Object_State = new string[7] { "None", "Person Dropped", "Person Picked", "Person Carrying", "Taken from Vehicle", "Placed in the Vehicle", "Left Behind" };
    string[] Count = new string[10] { "None", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Others" };
    string[] G_form = new string[7] { "None", "Merging", "Scattered", "United", "Arriving", "Joined", "Marching" };
    string[] GA_Type = new string[12] { "None", "Loading", "Unloading", "Object_Exchange", "Unattended_Object_Dropping", "Unattended_Object_Picking", "Vehicle_Exchange", "Group_Fleeing", "Object_Delivering", "Vehicle_Changing", "Group_Loitering", "Vehicle_Loitering" };
    string[] G_State = new string[11] { "None", "Standing", "Walking", "Running", "Jumping", "Joined", "Talking", "Fighting", "Screaming", "Talking_on_Phone", "Walkie_Talkie" };
    string[] Event_type = new string[8] { "None", "Intruding", "Screaming", "Fighting", "Fleeing", "Shooting", "Banging", "Hiding" };
    string[] Event_Severity = new string[6] { "None", "Low", "Gaurded", "Warning", "Elevated", "Critical" };
    string[] hat = new string[7] { "None", "Sports_Cap", "Military_Hat", "Hard_Hat", "Hat", "Straw_Hats", "Others" };
    string[] Gender = new string[3] { "None", "Male", "Female" };
    string[] Skin_color = new string[6] { "None", "White", "Light_Brown", "Moderate_Brown", "Dark_Brown", "Black" };
    string[] msk = new string[4] { "None", "Mask", "Reading_Glass", "Sun_Glass" };
    string[] mus = new string[3] { "None", "Yes", "No" };
    string[] brd = new string[3] { "None", "Yes", "No" };
    string[] vsnd = new string[12] { "None", "Drived_By", "Arrived", "Engine_On", "Engine_Off", "Departed", "Door_Open", "Door_Close", "Hood_Open", "Hood_Close", "Trunk_Open", "Trunk_Close" };
    string[] hsnd = new string[8] { "None", "Talking", "Yelling", "Walking", "Running", "Knocking", "Talking_on-Phone", "Talking_on_Walkie-Talkie" };
    string[] osnd = new string[22] { "None", "Drived-By", "Arrived", "Engine_On", "Engine_Off", "Departed", "Door_Open", "Door_Close", "Hood_Open", "Hood_Close", "Trunk_Open", "Trunk_Close", "Radio", "Wooden_Box", "Cartoon_Box", "ToolBox", "Glass_Containers", "Plastic_Containers", "Metallic_Parts", "Metallic_Trash_Can", "Sparse_Gun_Shots", "Machine_Gun" };
    string[] esnd = new string[4] { "None", "Wind_Sound", "Thunderstorm_Sound", "Rain-Sound" };
    string[] vehid = new string[11] { "None", "VehId-1", "VehId-2", "VehId-3", "VehId-4", "VehId-5", "VehId-6", "VehId-7", "VehId-8", "VehId-9", "VehId-10" };
    string[] humid = new string[11] { "None", "HumId-1", "HumId-2", "HumId-3", "HumId-4", "HumId-5", "HumId-6", "HumId-7", "HumId-8", "HumId-9", "HumId-10" };
    string[] objid = new string[11] { "None", "ObjId-1", "ObjId-2", "ObjId-3", "ObjId-4", "ObjId-5", "ObjId-6", "ObjId-7", "ObjId-8", "ObjId-9", "ObjId-10" };
    string[] grpid = new string[11] { "None", "GrpId-1", "GrpId-2", "GrpId-3", "GrpId-4", "GrpId-5", "GrpId-6", "GrpId-7", "GrpId-8", "GrpId-9", "GrpId-10" };
    string[] Id = new string[7] { "None", "1", "2", "3", "4", "5", "6" };
    string[] Operations = new string[9] { "Vehicle", "Human", "Object", "Group_Pattern", "Group_Activity", "Event", "Biometric_Features", "Face-Features", "Sound" };
    string[] strMessages = new string[15] {
        "Cam-1, 01/27/10, 11:21:28, 35, 38S MB 43826 78793, 120 30, Vehicle, Sedan, Black, Normal, None, Arrived, None, 87.5",
        "Cam-1, 01/27/10, 11:21:46, 38, 38S MB 43826 78793, 120 30, Vehicle, Sedan, Black, Normal, None, Arrived, None, 90",
        "Cam-1, 01/27/10, 11:22:56, 46, 38S MB 43826 78793, 120 30, Human, Blue, none, Walking, Sedan, None, None, 90",
        "Cam-1, 01/27/10, 11:23:07, 46, 38S MB 43826 78793, 120 30, Human, Grey, none, Walking, Sedan, None, None, 90",
        "Cam-1, 01/27/10, 11:23:28, 49, 38S MB 43826 78793, 120 30, Human, Black, none, Walking, Sedan, None, None, 90",
        "Cam-1, 01/27/10, 11:24:38, 50, 38S MB 43826 78793, 120 30, Group Pattern, 3, Merging, Walking, None, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:03, 51, 38S MB 43826 78793, 120 30, Group Pattern, 3, United, Standing, 3, None, None, 87.5",
        "Cam-1, 01/27/10, 11:25:47, 54, 38S MB 43826 78793, 120 30, Group Pattern, 3, United, Talking, 3, 1, None, 87.5",
        "Cam-1, 01/27/10, 11:26:42, 67, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 3, None, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:27:10, 71, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 3, 1, 2, Sedan, Medium Weight, 90",
        "Cam-1, 01/27/10, 11:27:43, 81, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 1, 1, 1, Sedan, Cardboard Box, 90",
        "Cam-1, 01/27/10, 11:28:37, 114, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 1, 1, 1, Sedan, Plastic, 90",
        "Cam-1, 01/27/10, 11:29:10, 148, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 3, 1, 2, Sedan, Light Weight, 90",
        "Cam-1, 01/27/10, 11:29:41, 186, 38S MB 43826 78793, 120 30, Group Activity, Unloading, 3, 1, 2, Sedan, Metal, 90",
        "Cam-1, 01/27/10, 11:30:39, 264, 38S MB 43826 78793, 120 30, Vehicle, Sedan, Black, Fast, Departing, None, None, 90" 
    };
        
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

}
