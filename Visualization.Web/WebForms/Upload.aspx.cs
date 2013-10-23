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
using System.Reflection;

public partial class WebForms_Upload : System.Web.UI.Page
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

        lblUploadImagesFolder.Text = "Copy all Images to  - " + UploadImagesFolder;
        lblUploadMessagesFolder.Text = "Copy all Messages to  - " + UploadMessagesFolder;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Upload.UploadMessagesFromExcel(UploadMessagesFolder);
        Upload.UploadImages(UploadImagesFolder);
    }



}
