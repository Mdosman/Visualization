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

public partial class WebForms_SlideShowPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            slides.Controls.Clear();
            if (Session["MsgIDList"] != null)
            {
                string strMsgIDList = Session["MsgIDList"].ToString();

                DataTable dt = Message.GetImages(strMsgIDList).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        slides.Controls.Add(new HtmlImage()
                        {
                            Src = "../Pictures/" + dRow["FolderName"].ToString() + "/" + dRow["FileName"].ToString(),
                            Alt = dRow["FileName"].ToString()
                        });
                    }
                }
                else
                {
                    this.slidesContainer.Controls.Add(new Label()
                    {
                        Text = "Search returned 0 results.",
                        ID= "lblWarn"
                    });
                }
            }
            else
            {
                this.slidesContainer.Controls.Add(new Label()
                {
                    Text = "Search returned 0 results.",
                    ID = "lblWarn"
                });
            }
        }
    }

}
