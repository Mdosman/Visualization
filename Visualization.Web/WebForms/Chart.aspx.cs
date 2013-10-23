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

using Visa;
public partial class WebForms_Chart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            hdnIsPostBack.Value = "1";
        }
        else
        {
            //LoadSQLQueries();
            LoadCams();
            LoadTMLs();
            LoadProcess();
            LoadProcessSubType();
        }
    }

    private void LoadCams()
    {
        DataSet ds = Message.LoadCams();
        ListItem selItem = new ListItem("-- Select --", "0");
        this.ddlCam.Items.Add(selItem);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem myItem = new ListItem(dr["SourceID"].ToString(), dr["SourceID"].ToString());
                    this.ddlCam.Items.Add(myItem);
                }
            }
        }
    }

    private void LoadTMLs()
    {
        DataSet ds = Message.LoadTMLs();
        ListItem selItem = new ListItem("-- Select --", "0");
        this.ddlTML.Items.Add(selItem);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem myItem = new ListItem(dr["MessageID"].ToString(), dr["MessageID"].ToString());
                    this.ddlTML.Items.Add(myItem);
                }
            }
        }
    }


    private void LoadProcess()
    {
        DataSet ds = Message.LoadProcess();
        ListItem selItem = new ListItem("-- Select --", "0");
        this.ddlProcess.Items.Add(selItem);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem myItem = new ListItem(dr["Process"].ToString(), dr["ProcessId"].ToString());
                    this.ddlProcess.Items.Add(myItem);
                }
            }
        }
    }

    private void LoadProcessSubType()
    {
        DataSet ds = Message.LoadProcessSubType();
        ListItem selItem = new ListItem("-- Select --", "0");
        this.ddlProcessSubType.Items.Add(selItem);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem myItem = new ListItem(dr["ProcessSubType"].ToString(), dr["ProcessSubTypeId"].ToString());
                    this.ddlProcessSubType.Items.Add(myItem);
                }
            }
        }
    }

    private void LoadSQLQueries()
    {
        DataSet ds = Message.LoadSQLQueries(1,9999);
        ListItem selItem = new ListItem("-- Select --", "0");
        this.ddlSQLQuery.Items.Add(selItem);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem myItem = new ListItem(dr["QueryName"].ToString(), dr["QueryId"].ToString());
                    this.ddlSQLQuery.Items.Add(myItem);
                }
            }
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
    }

}
