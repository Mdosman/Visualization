using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.SessionState;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Visa
{

    public class MessageHandler : IHttpHandler, IRequiresSessionState 
    {
        #region Properties
        
        public string Operation { get; set; }
        public string NodeType { get; set; }
        public string Id { get; set; }
        public string AttrType { get; set; }
        //public string EventDateFrom { get; set; }
        //public string EventDateTo { get; set; }
        //public string Cam { get; set; }
        //public string SQLQueryID { get; set; }
        //public string EventType { get; set; }
        //public string EventAttr { get; set; }
        //public string EventAttrSub { get; set; }
        //public string QueryName { get; set; }

        #endregion Properties

        public class FullQuery
        {
            public string FQ_EventType { get; set; }
            public string FQ_EventAttr { get; set; }
            public string FQ_EventAttrSub { get; set; }

        }

        public class RequestData
        {
            public string RD_EventDateFrom { get; set; }
            public string RD_EventDateTo { get; set; }
            public string RD_Cam { get; set; }
            public string RD_TML { get; set; }
            public string RD_SQLQueryID { get; set; }
            public string RD_QueryName { get; set; }
            public string RD_FilteredMessage { get; set; }
            public string RD_FilteredMessageName { get; set; }
            public FullQuery RD_FQuery { get; set; }
            public string FQ_PID { get; set; }
            public string FQ_PSID { get; set; }

        }

        public void ProcessRequest(HttpContext context)
        {
            string strResponse = string.Empty;
            try
            {
                string operation = string.Empty;
                string strRequestData = string.Empty;
                RequestData requestData = new RequestData();
                if (context.Request.QueryString["operation"] != null)
                {
                    operation = context.Request.QueryString["operation"].ToString();
                }
                if (context.Request.QueryString["RequestData"] != null)
                {
                    strRequestData = context.Request.QueryString["RequestData"].ToString();
                    requestData = new JavaScriptSerializer().Deserialize<RequestData>(strRequestData);
                }
                //string strNodeType = context.Request.QueryString["nodeType"].ToString();
                //string strId = context.Request.QueryString["id"].ToString();
                //string strAttributeType = context.Request.QueryString["attributeType"].ToString();

                switch (operation)
                {
                    #region jsTree

                    case "jsTree":
                        {
                            NodeType = context.Request.QueryString["nodeType"].ToString();
                            Id = context.Request.QueryString["id"].ToString();
                            AttrType = context.Request.QueryString["attributeType"].ToString();
                            string processId = context.Request.QueryString["processId"].ToString();
                            string processSubTypeId = context.Request.QueryString["processSubTypeId"].ToString();
                            switch (NodeType)
                            {
                                case "Message":
                                    {
                                        DataTable dtEvents = Message.GetEventTypes(processId, processSubTypeId).Tables[0];

                                        if (dtEvents.Rows.Count > 0)
                                        {
                                            StringBuilder sbResponse = new StringBuilder();
                                            sbResponse.Append("[");
                                            foreach (DataRow dRow in dtEvents.Rows)
                                            {
                                                string strEvent = dRow["Event"].ToString();
                                                sbResponse.Append("{ \"data\" : \"" + strEvent + "\" , \"state\" : \"closed\" , \"attr\" : { \"some-other-attribute\": \"Event\" , \"id\" : \"" + strEvent + "\" }  },");

                                            }
                                            strResponse = sbResponse.ToString();
                                            strResponse = strResponse.Substring(0, strResponse.LastIndexOf(',')) + "]";
                                        }
                                        break;
                                    }
                                case "Event":
                                    {
                                        DataTable dtEventSubTypes = Message.GetEventAttributeTypes().Tables[0];

                                        if (dtEventSubTypes.Rows.Count > 0)
                                        {
                                            StringBuilder sbResponse = new StringBuilder();
                                            sbResponse.Append("[");
                                            foreach (DataRow dRow in dtEventSubTypes.Rows)
                                            {
                                                string strEventAttributeType = dRow["EventAttributeType"].ToString();
                                                sbResponse.Append("{ \"data\" : \"" + strEventAttributeType + "\" , \"state\" : \"closed\" , \"attr\" : { \"some-other-attribute\": \"EventAttributeType\" , \"id\" : \"" + Id + "\" , \"attributeType\" : \"" + strEventAttributeType + "\" }  },");
                                                //sbResponse.Append("{ \"data\" : \"" + strRuleDesc + "\" , \"attr\" : { \"id\": \"" + strID + "\" , \"effectiveDate\": \"" + dRow["EffectiveDate"].ToString() + "\" ,  \"ruleID\": \"" + dRow["NonComplianceRuleID"].ToString() + "\"  ,  \"some-other-attribute\": \"Rule\"}  },");
                                            }
                                            strResponse = sbResponse.ToString();
                                            strResponse = strResponse.Substring(0, strResponse.LastIndexOf(',')) + "]";
                                        }
                                        break;
                                    }
                                case "EventAttributeType":
                                    {
                                        DataTable dtEventSubTypesList = Message.GetEventAttributeSubTypes(Id, AttrType, processId, processSubTypeId).Tables[0];

                                        if (dtEventSubTypesList.Rows.Count > 0)
                                        {
                                            StringBuilder sbResponse = new StringBuilder();
                                            sbResponse.Append("[");
                                            foreach (DataRow dRow in dtEventSubTypesList.Rows)
                                            {
                                                string strEventAttributeSubType = dRow["EventAttributeSubType"].ToString();
                                                sbResponse.Append("{ \"data\" : \"" + strEventAttributeSubType + "\" , \"state\" : \"open\" , \"attr\" : { \"some-other-attribute\": \"EventAttributeSubType\" , \"id\" : \"" + Id + "\" , \"attributeType\" : \"" + AttrType + "\", \"attributeSubType\" : \"" + strEventAttributeSubType + "\"}  },");
                                                //sbResponse.Append("{ \"data\" : \"" + strRuleDesc + "\" , \"attr\" : { \"id\": \"" + strID + "\" , \"effectiveDate\": \"" + dRow["EffectiveDate"].ToString() + "\" ,  \"ruleID\": \"" + dRow["NonComplianceRuleID"].ToString() + "\"  ,  \"some-other-attribute\": \"Rule\"}  },");
                                            }
                                            strResponse = sbResponse.ToString();
                                            strResponse = strResponse.Substring(0, strResponse.LastIndexOf(',')) + "]";
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        strResponse = string.Empty;
                                        break;
                                    }
                            }
                            break;
                        }
                        
                    #endregion jsTree

                    #region Map

                    case "Map":
                        {
                            Message msg = new Message(requestData);
                            //ParseRequestData(requestData,msg);
                            DataTable dtMarkersList = msg.GetMapMarkers(msg.ProcessId, msg.ProcessSubTypeId).Tables[0];
                            if (dtMarkersList.Rows.Count > 0)
                            {
                                StringBuilder sbResponse = new StringBuilder();
                                sbResponse.Append("[");

                                foreach (DataRow dr in dtMarkersList.Rows)
                                {
                                    sbResponse.Append(" { \"Lat\" : \"" + dr["Lat"].ToString() + "\" , \"Lon\" : \"" + dr["Lon"].ToString() + "\"  },");
                                }
                                strResponse = sbResponse.ToString();
                                strResponse = strResponse.Substring(0, strResponse.LastIndexOf(',')) + "]";
                            }
                            break;
                        }
                    #endregion Map

                    #region BarGraph
                    case "BarGraph":
                        {
                            if (context.Session["MsgIDList"] != null)
                            {
                                string strMsgIDList = context.Session["MsgIDList"].ToString();
                                //Message msg = new Message(requestData);
                                //ParseRequestData(requestData,msg);
                                DataTable dtColumnList = Message.GetColumnGraphData(strMsgIDList).Tables[0];
                                if (dtColumnList.Rows.Count > 0)
                                {
                                    StringBuilder sbResponse = new StringBuilder();
                                    sbResponse.Append("[");
                                    foreach (DataRow dRow in dtColumnList.Rows)
                                    {
                                        StringBuilder sbDates = new StringBuilder();
                                        string strDates = string.Empty;
                                        string[] strEventDates = dRow["EventDates"].ToString().Split(';');
                                        foreach (string strDate in strEventDates)
                                        {
                                            double dtTicks = UnixTicks(Convert.ToDateTime(strDate));
                                            sbDates.Append("[" + dtTicks + ", 1 ] ,");
                                        }

                                        strDates = sbDates.ToString();
                                        strDates = strDates.Substring(0, strDates.LastIndexOf(','));

                                        sbResponse.Append("{ \"name\" : \"" + dRow["EventType"].ToString() + "\"  ,\"type\": \"column\" , \"data\" : [" + strDates + " ] } ,");
                                    }
                                    strResponse = sbResponse.ToString();
                                    strResponse = strResponse.Substring(0, strResponse.LastIndexOf(',')) + "]";
                                }
                            }
                            //strResponse = "[{ \"name\" : \"" + operation + "\"  ,\"type\": \"column\" , \"data\" : [ [" + strVal + "," + strNum + " ],[ " + strVal + "," + strNum + " ]] }]";                            

                             break;
                        }

                    #endregion BarGraph
                        
                    #region MessageList

                    case "MessageList":
                        {
                            Message msg = new Message(requestData);
                            //ParseRequestData(requestData,msg);

                            DataTable dtMessageList = msg.GetMessages(msg.ProcessId, msg.ProcessSubTypeId).Tables[0];
                            string strMsgIDList = string.Empty;
                            
                            if (dtMessageList.Rows.Count > 0)
                            {
                                StringBuilder sbMsgIDList = new StringBuilder();
                                sbMsgIDList.Append(" ( ");
                                foreach (DataRow dr in dtMessageList.Rows)
                                {
                                    sbMsgIDList.Append(dr["MsgID"].ToString() + " ,");

                                    string strMessageRow = string.Empty;

                                    for (int j = 0; j < dtMessageList.Columns.Count; j++)
                                    {
                                        strMessageRow += dr[j].ToString() + " ";
                                    }
                                    if (!strMessageRow.Trim().Equals(string.Empty))
                                    {
                                        strResponse += strMessageRow + ";";
                                    }
                                }
                                strMsgIDList = sbMsgIDList.ToString();
                                strMsgIDList = strMsgIDList.Substring(0, strMsgIDList.LastIndexOf(',')) + " )";
                            
                            }
                            context.Session["MsgIDList"] = strMsgIDList;
                            context.Session["MessageList"] = strResponse;
                            break;
                        }

                    #endregion MessageList

                    #region NodeGraph

                    case "NodeGraph":
                        {
                            if (context.Session["MsgIDList"] != null)
                            {
                                string strMsgIDList = context.Session["MsgIDList"].ToString();
                                strResponse = Message.GetNodesByMsgIDList(strMsgIDList.Trim().TrimStart('(').TrimEnd(')'));

                            }
                            break;
                        }

                    #endregion NodeGraph

                    #region Parse Data

                    case "ParseData":
                        {
                            string[] strKeywordArray = null;
                            string strMsgIDList = string.Empty;
                            string strMessagesList = string.Empty;
                            string strMarkersList = string.Empty;
                            if (context.Request.QueryString["Keywords"] != null)
                            {
                                string strKeywords = context.Request.QueryString["Keywords"].ToString();
                                strKeywordArray = strKeywords.Trim().Split(',');
                            }
                            if (context.Session["MessageList"] != null && strKeywordArray != null)
                            {
                                string messageList = context.Session["MessageList"].ToString();
                                string[] strTempArray = messageList.Split(';');
                                List<string> strList = new List<string>(strTempArray);
                                
                                for(int i=0; i < strKeywordArray.Length; i++)
                                {
                                    strList = strList.FindAll(x => x.ToLower().Contains(strKeywordArray[i].Trim().ToLower()));
                                }
                                
                                if (strList.Count > 0)
                                {
                                    StringBuilder sbMsgIDList = new StringBuilder();
                                    sbMsgIDList.Append(" ( ");
                                    foreach (string strMsg in strList)
                                    {
                                        if (strMsg.IndexOf(' ') > 0)
                                        {
                                            string[] strArray = strMsg.Split(' ');
                                            sbMsgIDList.Append( strArray[0] + " ,");
                                        }
                                    }
                                    strMsgIDList = sbMsgIDList.ToString();
                                    strMsgIDList = strMsgIDList.Substring(0, strMsgIDList.LastIndexOf(',')) + " )";
                                }
                                context.Session["MsgIDList"] = strMsgIDList;
                                DataTable dtMarkersList = Message.GetMapMarkersByMsgIDList(strMsgIDList).Tables[0];

                                if (dtMarkersList.Rows.Count > 0)
                                {
                                    StringBuilder sbMarkers = new StringBuilder();
                                    sbMarkers.Append("[");

                                    foreach (DataRow dr in dtMarkersList.Rows)
                                    {
                                        sbMarkers.Append(" { \"Lat\" : \"" + dr["Lat"].ToString() + "\" , \"Lon\" : \"" + dr["Lon"].ToString() + "\"  },");
                                    }

                                    string strTempList = sbMarkers.ToString();
                                    strMarkersList = strTempList.Substring(0, strTempList.LastIndexOf(',')) + " ]";

                                }
                                strMessagesList = string.Join(";", strList.ToArray());
                                if (strMessagesList.Trim().Length > 0)
                                {
                                    strMessagesList += ";";
                                }
                                strResponse = strMessagesList + "~" + strMarkersList;
                            }
                            break;
                        }

                    #endregion Parse Data

                    #region jqGrid Data

                    case "jqGrid":
                        {
                            int iPageNum = 1, iRowsPerPage = 10, iTotalCount;
                            string strTotalCount = "0";
                            string strOperation = context.Request.Form["oper"];
                            string strQueryId = context.Request.Form["ID"];
                            string strQueryDesc = context.Request.Form["QueryDescription"];
                            string strQueryList = string.Empty;

                            switch (strOperation)
                            {
                                case null:
                                    {
                                        if (context.Request.QueryString["page"] != null)
                                        {
                                            iPageNum = int.Parse(context.Request.QueryString["page"]);
                                        }
                                        if (context.Request.QueryString["rows"] != null)
                                        {
                                            iRowsPerPage = int.Parse(context.Request.QueryString["rows"]);
                                        }
                                        DataTable dT = Message.LoadSQLQueries(iPageNum, iRowsPerPage).Tables[0];
                                        if (dT.Rows.Count > 0)
                                        {
                                            int count = dT.Rows.Count;
                                            StringBuilder sbQueries = new StringBuilder();
                                            sbQueries.Append("{");
                                            sbQueries.Append(" \"rows\"  : [ ");
                                            foreach (DataRow dr in dT.Rows)
                                            {
                                                strTotalCount = dr["TotalCount"].ToString();
                                                string strDesc = dr["QueryDescription"] == DBNull.Value ? string.Empty : dr["QueryDescription"].ToString();
                                                sbQueries.Append(" { \"ID\" : \"" + dr["QueryID"].ToString() + "\" , \"QueryName\" : \"" + dr["QueryName"].ToString() + "\" , \"QueryDescription\" : \"" + strDesc + "\"  },");
                                            }
                                            strQueryList = sbQueries.ToString();
                                            strQueryList = strQueryList.Substring(0, strQueryList.LastIndexOf(','));

                                            sbQueries = new StringBuilder();
                                            sbQueries.Append(strQueryList + " ], ");

                                            int totalPages = int.Parse(strTotalCount) / iRowsPerPage;

                                            sbQueries.Append(" \"total\"  : \"" + (totalPages+1).ToString() + "\" ,");
                                            sbQueries.Append(" \"page\"  : \"" + iPageNum.ToString() + "\" ,");
                                            sbQueries.Append(" \"records\"  : \"" + strTotalCount + "\" } ");
                                            strResponse = sbQueries.ToString();
                                        }
                                        break;
                                    }
                                case "edit":
                                    {
                                        Message.EditQuery(int.Parse(strQueryId), strQueryDesc);
                                        strResponse = string.Empty;
                                        break;
                                    }
                                case "del":
                                    {
                                        Message.DeleteQuery(int.Parse(strQueryId));
                                        strResponse = string.Empty;
                                        break;
                                    }
                                default:
                                    {
                                        strResponse = string.Empty;
                                        break;
                                    }

                            }

                            break;
                        }

                    #endregion jqGrid Data


                    #region jqGridSavedFilteredMessages Data

                    case "jqGridSavedFilteredMessages":
                        {
                            int iPageNum = 1, iRowsPerPage = 10, iTotalCount;
                            string strTotalCount = "0";
                            string strOperation = context.Request.Form["oper"];
                            string strFilteredMessageId = context.Request.Form["ID"];
                            string strFilteredMessageDescription = context.Request.Form["Description"];
                            string strQueryList = string.Empty;

                            switch (strOperation)
                            {
                                case null:
                                    {
                                        if (context.Request.QueryString["page"] != null)
                                        {
                                            iPageNum = int.Parse(context.Request.QueryString["page"]);
                                        }
                                        if (context.Request.QueryString["rows"] != null)
                                        {
                                            iRowsPerPage = int.Parse(context.Request.QueryString["rows"]);
                                        }
                                        DataTable dT = Message.LoadSavedFilteredMessages(iPageNum, iRowsPerPage).Tables[0];
                                        if (dT.Rows.Count > 0)
                                        {
                                            int count = dT.Rows.Count;
                                            StringBuilder sbQueries = new StringBuilder();
                                            sbQueries.Append("{");
                                            sbQueries.Append(" \"rows\"  : [ ");
                                            foreach (DataRow dr in dT.Rows)
                                            {
                                                strTotalCount = dr["TotalCount"].ToString();
                                                sbQueries.Append(" { \"ID\" : \"" + dr["FilteredMessageID"].ToString() + "\" , \"Name\" : \"" + dr["FilteredMessageName"].ToString() + "\" , \"FilteredMessage\" : \"" + dr["FilteredMessage"].ToString().Replace("\n","<br />") + "\" , \"Description\" : \"" + dr["FilteredMessageDescription"].ToString() + "\"  },");
                                            }
                                            strQueryList = sbQueries.ToString();
                                            strQueryList = strQueryList.Substring(0, strQueryList.LastIndexOf(','));

                                            sbQueries = new StringBuilder();
                                            sbQueries.Append(strQueryList + " ], ");

                                            int totalPages = int.Parse(strTotalCount) / iRowsPerPage;

                                            sbQueries.Append(" \"total\"  : \"" + (totalPages + 1).ToString() + "\" ,");
                                            sbQueries.Append(" \"page\"  : \"" + iPageNum.ToString() + "\" ,");
                                            sbQueries.Append(" \"records\"  : \"" + strTotalCount + "\" } ");
                                            strResponse = sbQueries.ToString();
                                        }
                                        break;
                                    }
                                case "edit":
                                    {
                                        Message.EditSavedFilteredMessage(int.Parse(strFilteredMessageId), strFilteredMessageDescription);
                                        strResponse = string.Empty;
                                        break;
                                    }
                                case "del":
                                    {
                                        Message.DeleteSavedFilteredMessage(int.Parse(strFilteredMessageId));
                                        strResponse = string.Empty;
                                        break;
                                    }
                                default:
                                    {
                                        strResponse = string.Empty;
                                        break;
                                    }

                            }

                            break;
                        }

                    #endregion jqGridSavedFilteredMessages Data

                    #region GetSavedQueries

                    case "GetSavedQueries":
                        {
                            try
                            {
                                StringBuilder sbQueries = new StringBuilder(); sbQueries.Append("[ { \"ID\" : \"0\" , \"QueryName\" : \"-- Select --\"  }");
                                DataSet ds = Message.LoadSQLQueries(1, 9999);
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            sbQueries.Append(" ,{ \"ID\" : \"" + dr["QueryID"].ToString() + "\" , \"QueryName\" : \"" + dr["QueryName"].ToString() + "\"  }");                                        
                                        }
                                    }
                                }
                                sbQueries.Append(" ]");
                                strResponse = sbQueries.ToString();
                            }
                            catch (Exception ex)
                            {
                                strResponse = "{ \"ID\" : \"0\" , \"QueryName\" : \"-- Select --\"  }";
                            }
                            break;
                        }
                        
                    #endregion GetSavedQueries

                    #region SaveQuery

                    case "SaveQuery":
                        {
                            try
                            {
                                Message msg = new Message(requestData);
                                //ParseRequestData(requestData,msg);
                                strResponse = msg.SaveQuery();
                            }
                            catch(Exception ex)
                            {
                                strResponse = "Error, Query not saved. Please try again.";
                            }
                            break;
                        }
                        
                    #endregion SaveQuery

                    #region SaveFilteredMessages

                    case "SaveFilteredMessages":
                        {
                            try
                            {
                                Message msg = new Message(requestData);
                                //ParseRequestData(requestData,msg);
                                strResponse = msg.SaveFilteredMessages();
                            }
                            catch (Exception ex)
                            {
                                strResponse = "Error, Filtered Messages not saved. Please try again.";
                            }
                            break;
                        }

                    #endregion SaveQuery

                    default:
                        {
                            strResponse = string.Empty;
                            break;
                        }
                }

                
            }
            catch (Exception ex)
            {
                strResponse = string.Empty;
            }

            string replaceWith = "";
            string strRemovedBreaks = strResponse.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            context.Response.ContentType = "text/plain";
            context.Response.Write(strRemovedBreaks);
        }

        // returns the number of milliseconds since Jan 1, 1970 
        public double UnixTicks(DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(dt.Ticks - d1.Ticks);
            return Math.Round(ts.TotalMilliseconds, 0);
        }
                
        //public void ParseRequestData(RequestData data, Message msg)
        //{
        //    msg.EventDateFrom = data.RD_EventDateFrom;
        //    msg.EventDateTo = data.RD_EventDateTo;
        //    msg.Cam = data.RD_Cam;
        //    msg.SQLQueryID = data.RD_SQLQueryID;
        //    msg.QueryName = data.RD_QueryName;
        //    msg.EventType = data.RD_FQuery.FQ_EventType;
        //    msg.EventAttr = data.RD_FQuery.FQ_EventAttr;
        //    msg.EventAttrSub = data.RD_FQuery.FQ_EventAttrSub;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}