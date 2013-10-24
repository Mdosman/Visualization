using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class WebForms_Mapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = LoadData("FRS_Source1");
        GridView1.DataBind();
    }

    public DataSet LoadData(string strSource)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        try
        {
            string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

            using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
            {
                using (SqlCommand objOleDbCmd = new SqlCommand("SELECT * FROM " + strSource, objCon))
                {
                    objCon.Open();
                    da.SelectCommand = objOleDbCmd;
                    da.Fill(ds);

                    return ds;
                }
            }

        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ProcessMapping()
    {
       
        try
        {
            string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

            using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
            {
                using (SqlCommand objOleDbCmd = new SqlCommand("usp_ConvertData", objCon))
                {
                    objCon.Open();
                    objOleDbCmd.CommandType = CommandType.StoredProcedure;
                    objOleDbCmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ProcessMapping();
        GridView1.DataSource = LoadData("FRS_Source3");
        GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string filename = "DownloadExcel.xls";
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

        GridView1.DataSource = LoadData("FRS_Source3");
        GridView1.DataBind();
        //Get the HTML for the control.
        GridView1.RenderControl(hw);
        //Write the HTML back to the browser.
        //Response.ContentType = application/vnd.ms-excel;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;
        Response.Write(tw.ToString());
        Response.End();
    }
}
