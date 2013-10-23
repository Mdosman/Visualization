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

namespace Visa
{
    /// <summary>
    /// Request Data
    /// </summary>
    public class Request
    {
        #region Properties

        public string NodeType { get; set; }
        public string Id { get; set; }
        public string AttrType { get; set; }
        public string EventDateFrom { get; set; }
        public string EventDateTo { get; set; }
        public string Cam { get; set; }
        public string SQLQueryID { get; set; }
        public string EventType { get; set; }
        public string EventAttr { get; set; }
        public string EventAttrSub { get; set; }
        public string QueryName { get; set; }

        #endregion Properties

        public Request()
        {

        }

    }
}
