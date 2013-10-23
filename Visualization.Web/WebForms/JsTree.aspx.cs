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
using System.IO;
using System.Data.SqlClient;
using System.Text;

public partial class WebForms_JsTree : System.Web.UI.Page
{
    private string UploadMessagesFolder = @"C:\UploadMessages\";
    private string UploadImagesFolder = @"C:\UploadImages\";
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Directory.Exists(UploadMessagesFolder))
        {
            Directory.CreateDirectory(UploadMessagesFolder);
        }

        if (!Directory.Exists(UploadImagesFolder))
        {
            Directory.CreateDirectory(UploadImagesFolder);
        }

        if (IsPostBack)
            hdnIsPostBack.Value = "1";

        if (!IsPostBack)
        {
            txtEventList.Text = "'Vehicle', 'Human', 'Object', 'Group Pattern'";
            FilterMessages();
        }

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        FilterMessages();
    }

    protected void FilterMessages()
    {
        
       //string strEventList = GenerateQuery();

       //DataSet ds =  Message.GetMessages(strEventList);
       //int iMessageCount = 0;
       //string strMessageList = string.Empty;
       //if (ds.Tables[0].Rows.Count > 0)
       //{
       //    foreach (DataRow dr in ds.Tables[0].Rows)
       //    {
       //        string strMessageRow = string.Empty;

       //        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
       //        {
       //            strMessageRow += dr[j].ToString() + " ";
       //        }
       //        if (!strMessageRow.Trim().Equals(string.Empty))
       //        {
       //            strMessageList += strMessageRow + "\r\n";
       //            iMessageCount++;
       //        }
       //    }
       //}

       //lblMsgCount.Text = iMessageCount.ToString();
       //txtMessages.Text = strMessageList;
    }

    private string GenerateQuery()
    {
        StringBuilder strFullQuery = new StringBuilder();
        string strEventList = string.Empty;
        string strEvents = txtEventList.Text.Trim().TrimEnd(',');
        if (!String.IsNullOrEmpty(strEvents))
        {
            strEventList = "[Event] IN ( " + strEvents + " )";
        }

        if (!strEventList.Equals(string.Empty))
        {
            strFullQuery.Append(strEventList);
        }


        string strAttributes = txtEventAttributeTypeList.Text.Trim().TrimEnd(',');
        if (!String.IsNullOrEmpty(strAttributes))
        {
            string[] strTemp = strAttributes.Split(',');
            for (int i = 0; i < strTemp.Length; i++)
            {
                string[] strTempSub = strTemp[i].Split(':');
                if (strFullQuery.Length == 0)
                {
                    strFullQuery.Append(" ( [Event] = '");
                    strFullQuery.Append(strTempSub[0].Trim());
                    strFullQuery.Append("' AND ");
                    strFullQuery.Append(strTempSub[1].Trim());
                    strFullQuery.Append(" IS NOT NULL ");
                    strFullQuery.Append(")");
                }
                else
                {
                    strFullQuery.Append(" OR ( [Event] = '");
                    strFullQuery.Append(strTempSub[0].Trim());
                    strFullQuery.Append("' AND ");
                    strFullQuery.Append(strTempSub[1].Trim());
                    strFullQuery.Append(" IS NOT NULL ");
                    strFullQuery.Append(")");
                }
            }

        }

        string strAttributeSubTypes = txtEventAttributeSubTypeList.Text.Trim().TrimEnd(',');
        if (!String.IsNullOrEmpty(strAttributeSubTypes))
        {
            string[] strTemp = strAttributeSubTypes.Split(',');
            for (int i = 0; i < strTemp.Length; i++)
            {
                string[] strTempSub = strTemp[i].Split(':');
                if (strFullQuery.Length == 0)
                {
                    strFullQuery.Append(" ( [Event] = '");
                    strFullQuery.Append(strTempSub[0].Trim());
                    strFullQuery.Append("' AND ");
                    strFullQuery.Append(strTempSub[1].Trim());
                    strFullQuery.Append(" = '");
                    strFullQuery.Append(strTempSub[2].Trim());
                    strFullQuery.Append("' )");
                }
                else
                {
                    strFullQuery.Append(" OR ( [Event] = '");
                    strFullQuery.Append(strTempSub[0].Trim());
                    strFullQuery.Append("' AND ");
                    strFullQuery.Append(strTempSub[1].Trim());
                    strFullQuery.Append(" = '");
                    strFullQuery.Append(strTempSub[2].Trim());
                    strFullQuery.Append("' )");
                }
            }
        }
        
        return strFullQuery.ToString();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Upload.UploadMessagesFromExcel(UploadMessagesFolder);
        Upload.UploadImages(UploadImagesFolder);
    }
}
