using System;
using System.Collections;
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
using System.Text;

public partial class WebForms_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            hdnIsPostBack.Value = "1";

        //if (!IsPostBack)
        //{
        //    txtEventList.Text = "'Vehicle', 'Human', 'Object', 'Group Pattern'";
        //    FilterMessages();
        //}
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //FilterMessages();
    }

    //protected void FilterMessages()
    //{

    //    string strEventList = GenerateQuery();

    //    DataSet ds = Message.GetMessages(strEventList);
    //    int iMessageCount = 0;
    //    string strMessageList = string.Empty;
    //    string strLatLonJSON = string.Empty;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in ds.Tables[0].Rows)
    //        {
    //            string strMessageRow = string.Empty;

    //            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
    //            {
    //                strMessageRow += dr[j].ToString() + " ";
    //            }
    //            if (!strMessageRow.Trim().Equals(string.Empty))
    //            {
    //                strMessageList += strMessageRow + "\r\n";
    //                iMessageCount++;
    //            }
    //        }
    //    }

    //    if (ds.Tables[1].Rows.Count == 13)
    //    {
    //        StringBuilder sbResponse = new StringBuilder();
    //        sbResponse.Append("[");

    //        foreach (DataRow dr in ds.Tables[1].Rows)
    //        {
    //            sbResponse.Append(" { \"Lat\" : \"" + dr["Lat"].ToString() + "\" , \"Lon\" : \"" + dr["Lon"].ToString() + "\"  },");
    //        }
    //        strLatLonJSON = sbResponse.ToString();
    //        strLatLonJSON = strLatLonJSON.Substring(0, strLatLonJSON.LastIndexOf(',')) + "]";
    //    }

    //    if (strLatLonJSON.Length > 0)
    //    {
    //        //txtLatLon.Text = strLatLonJSON;
    //        string sJava = string.Empty;

    //        // Create Function in Page
    //        sJava += "<script language='javascript'>";
    //        // Assign True Value
    //        sJava += "strLatLonJSON = " + strLatLonJSON;
    //        sJava += "CreateMarkers();";
    //        // Complete Script
    //        sJava += "</script>";

    //        // Register String
    //        //RegisterStartupScript("CloseChild", sJava);
    //        ClientScript.RegisterStartupScript(this.GetType(), "CloseChild", sJava);
    //    }
    //    lblMsgCount.Text = iMessageCount.ToString();
    //    txtMessages.Text = strMessageList;
    //}

    //private string GenerateQuery()
    //{
    //    StringBuilder strFullQuery = new StringBuilder();
    //    string strEventList = string.Empty;
    //    string strEvents = txtEventList.Text.Trim().TrimEnd(',');
    //    if (!String.IsNullOrEmpty(strEvents))
    //    {
    //        strEventList = "[Event] IN ( " + strEvents + " )";
    //    }

    //    if (!strEventList.Equals(string.Empty))
    //    {
    //        strFullQuery.Append(strEventList);
    //    }


    //    string strAttributes = txtEventAttributeTypeList.Text.Trim().TrimEnd(',');
    //    if (!String.IsNullOrEmpty(strAttributes))
    //    {
    //        string[] strTemp = strAttributes.Split(',');
    //        for (int i = 0; i < strTemp.Length; i++)
    //        {
    //            string[] strTempSub = strTemp[i].Split(':');
    //            if (strFullQuery.Length == 0)
    //            {
    //                strFullQuery.Append(" ( [Event] = '");
    //                strFullQuery.Append(strTempSub[0].Trim());
    //                strFullQuery.Append("' AND ");
    //                strFullQuery.Append(strTempSub[1].Trim());
    //                strFullQuery.Append(" IS NOT NULL ");
    //                strFullQuery.Append(")");
    //            }
    //            else
    //            {
    //                strFullQuery.Append(" OR ( [Event] = '");
    //                strFullQuery.Append(strTempSub[0].Trim());
    //                strFullQuery.Append("' AND ");
    //                strFullQuery.Append(strTempSub[1].Trim());
    //                strFullQuery.Append(" IS NOT NULL ");
    //                strFullQuery.Append(")");
    //            }
    //        }

    //    }

    //    string strAttributeSubTypes = txtEventAttributeSubTypeList.Text.Trim().TrimEnd(',');
    //    if (!String.IsNullOrEmpty(strAttributeSubTypes))
    //    {
    //        string[] strTemp = strAttributeSubTypes.Split(',');
    //        for (int i = 0; i < strTemp.Length; i++)
    //        {
    //            string[] strTempSub = strTemp[i].Split(':');
    //            if (strFullQuery.Length == 0)
    //            {
    //                strFullQuery.Append(" ( [Event] = '");
    //                strFullQuery.Append(strTempSub[0].Trim());
    //                strFullQuery.Append("' AND ");
    //                strFullQuery.Append(strTempSub[1].Trim());
    //                strFullQuery.Append(" = '");
    //                strFullQuery.Append(strTempSub[2].Trim());
    //                strFullQuery.Append("' )");
    //            }
    //            else
    //            {
    //                strFullQuery.Append(" OR ( [Event] = '");
    //                strFullQuery.Append(strTempSub[0].Trim());
    //                strFullQuery.Append("' AND ");
    //                strFullQuery.Append(strTempSub[1].Trim());
    //                strFullQuery.Append(" = '");
    //                strFullQuery.Append(strTempSub[2].Trim());
    //                strFullQuery.Append("' )");
    //            }
    //        }
    //    }

    //    return strFullQuery.ToString();
    //}

}
