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
    /// Upload Messages and Images
    /// </summary>
    public class Message
    {

        #region Properties

        public string NodeType { get; set; }
        public string Id { get; set; }
        public string AttrType { get; set; }
        public string EventDateFrom { get; set; }
        public string EventDateTo { get; set; }
        public string Cam { get; set; }
        public string TML { get; set; }
        public string SQLQueryID { get; set; }
        public string EventType { get; set; }
        public string EventAttr { get; set; }
        public string EventAttrSub { get; set; }
        public string QueryName { get; set; }
        public string ProcessId { get; set; }
        public string ProcessSubTypeId { get; set; }
        public string  FilteredMessage { get; set; }
        public string FilteredMessageName { get; set; }

        #endregion Properties

        public Message()
        {
        }

        public Message(MessageHandler.RequestData data)
        {
            EventDateFrom = data.RD_EventDateFrom;
            EventDateTo = data.RD_EventDateTo;
            Cam = data.RD_Cam;
            TML = data.RD_TML;
            SQLQueryID = data.RD_SQLQueryID;
            QueryName = data.RD_QueryName;
            FilteredMessage = data.RD_FilteredMessage;
            FilteredMessageName = data.RD_FilteredMessageName;
            EventType = data.RD_FQuery.FQ_EventType;
            EventAttr = data.RD_FQuery.FQ_EventAttr;
            EventAttrSub = data.RD_FQuery.FQ_EventAttrSub;
            ProcessId = data.RD_FQuery.FQ_PID;
            ProcessSubTypeId = data.RD_FQuery.FQ_PSID;
        }

        #region Generate Query

        private string GenerateQuery()
        {
            StringBuilder strFullQuery = new StringBuilder();
            string strEventList = string.Empty;
            if (!String.IsNullOrEmpty(EventType))
            {
                strEventList = "[Event] IN ( " + EventType.Trim().TrimEnd(',') + " )";
            }

            if (!strEventList.Equals(string.Empty))
            {
                strFullQuery.Append(strEventList);
            }


            if (!String.IsNullOrEmpty(EventAttr))
            {
                string[] strTemp = EventAttr.Trim().TrimEnd(',').Split(',');
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

            if (!String.IsNullOrEmpty(EventAttrSub))
            {
                string[] strTemp = EventAttrSub.Trim().TrimEnd(',').Split(',');
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

        #endregion


        #region Load Queries

        public static DataSet LoadSQLQueries(int iPageIndex, int iRowsPerPage)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objSqlCmd = new SqlCommand("sp_GetSQLQueries", objCon))
                    {
                        objCon.Open();
                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int);
                        objSqlCmd.Parameters["@PageIndex"].Value = iPageIndex;
                        objSqlCmd.Parameters.Add("@NumRows", SqlDbType.Int);
                        objSqlCmd.Parameters["@NumRows"].Value = iRowsPerPage;
                        da.SelectCommand = objSqlCmd;
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

        #endregion


        #region Load Cams

        public static DataSet LoadCams()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetCamsList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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


        #region Load TMLs

        public static DataSet LoadTMLs()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetTMLs", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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

        #endregion

        public static DataSet LoadProcess()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetProcessList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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

        public static DataSet LoadProcessSubType()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetProcessSubTypeList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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
        #endregion


        #region Save Query

        public string SaveQuery()
        {
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objSqlCmd = new SqlCommand("sp_SaveQuery", objCon))
                    {
                        objCon.Open();
                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.Parameters.Add("@QueryName", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@QueryName"].Value = this.QueryName;
                        objSqlCmd.Parameters.Add("@Params", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@Params"].Value = GenerateQuery();
                        int result = objSqlCmd.ExecuteNonQuery();

                        if (result == -1)
                        {
                            return "Error, Duplicate Query Name.";
                        }
                        else
                        {
                            return "Query saved successfully.";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return "Error, Query not saved. Please try again.";
            }
        }

        #endregion


        #region Delete Query

        public static void DeleteQuery(int iQueryId)
        {
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objSqlCmd = new SqlCommand("sp_DeleteQuery", objCon))
                    {
                        objCon.Open();
                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.Parameters.Add("@QueryId", SqlDbType.Int);
                        objSqlCmd.Parameters["@QueryId"].Value = iQueryId;
                        objSqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion

        #region Edit Query

        public static void EditQuery(int iQueryId, string strQueryDesc)
        {
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objSqlCmd = new SqlCommand("sp_EditQuery", objCon))
                    {
                        objCon.Open();
                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.Parameters.Add("@QueryId", SqlDbType.Int);
                        objSqlCmd.Parameters["@QueryId"].Value = iQueryId;
                        objSqlCmd.Parameters.Add("@QueryDesc", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@QueryDesc"].Value = strQueryDesc;
                        objSqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


        #region Get Map Markers

        public DataSet GetMapMarkers()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strParams = string.Empty;
            try
            {

                if (SQLQueryID == "0")
                {
                    strParams = GenerateQuery();
                }
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetMapMarkers", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@EventDateFrom", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateFrom"].Value = EventDateFrom;
                        objOleDbCmd.Parameters.Add("@EventDateTo", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateTo"].Value = EventDateTo;
                        objOleDbCmd.Parameters.Add("@Cam", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Cam"].Value = Cam;
                        objOleDbCmd.Parameters.Add("@TML", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@TML"].Value = TML;
                        objOleDbCmd.Parameters.Add("@SavedQueryID", SqlDbType.Int);
                        objOleDbCmd.Parameters["@SavedQueryID"].Value = SQLQueryID;
                        objOleDbCmd.Parameters.Add("@Params", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Params"].Value = strParams;
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

        public DataSet GetMapMarkers(string processId, string processSubTypeId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strParams = string.Empty;
            try
            {

                if (SQLQueryID == "0")
                {
                    strParams = GenerateQuery();
                }
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetMapMarkersWithFilter", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@EventDateFrom", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateFrom"].Value = EventDateFrom;
                        objOleDbCmd.Parameters.Add("@EventDateTo", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateTo"].Value = EventDateTo;
                        objOleDbCmd.Parameters.Add("@Cam", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Cam"].Value = Cam;
                        objOleDbCmd.Parameters.Add("@TML", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@TML"].Value = TML;
                        objOleDbCmd.Parameters.Add("@SavedQueryID", SqlDbType.Int);
                        objOleDbCmd.Parameters["@SavedQueryID"].Value = SQLQueryID;
                        objOleDbCmd.Parameters.Add("@Params", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Params"].Value = strParams;
                        objOleDbCmd.Parameters.Add("@processId", SqlDbType.Int).Value = processId;
                        objOleDbCmd.Parameters.Add("@processSubTypeId", SqlDbType.Int).Value = processSubTypeId;
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

        #endregion


        #region Get Map Markers By MsgIDList

        public static DataSet GetMapMarkersByMsgIDList(string strMsgIDList)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strParams = string.Empty;
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetMapMarkersByMsgIDList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@MsgIDList", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@MsgIDList"].Value = strMsgIDList;
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

        #endregion

        #region Get NodeGraph Data By MsgIDList

        public static string GetNodesByMsgIDList(string strMsgIDList)
        {
            DataSet ds = new DataSet();
            SqlDataReader rdr = null;
            string strNodesJSON = string.Empty;
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("usp_FRS_GenerateGraphNodes", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@i_MessageIds", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@i_MessageIds"].Value = strMsgIDList;

                        rdr = objOleDbCmd.ExecuteReader();

                        // iterate through results, printing each to console
                        while (rdr.Read())
                        {
                            strNodesJSON = rdr["NodesJSON"].ToString();
                        }
                        return strNodesJSON;
                    }
                }

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #endregion


        #region Get Graph Data

        public static DataSet GetColumnGraphData(string strMsgIDList)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetColumnGraphDataByMsgIDList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@MsgIDList", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@MsgIDList"].Value = strMsgIDList;
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

        #endregion


        #region Get Messages

        public DataSet GetMessages()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strParams = string.Empty;
            try
            {

                if (SQLQueryID == "0")
                {
                    strParams = GenerateQuery();
                }
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetMessages", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@EventDateFrom", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateFrom"].Value = EventDateFrom;
                        objOleDbCmd.Parameters.Add("@EventDateTo", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateTo"].Value = EventDateTo;
                        objOleDbCmd.Parameters.Add("@Cam", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Cam"].Value = Cam;
                        objOleDbCmd.Parameters.Add("@TML", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@TML"].Value = TML;
                        objOleDbCmd.Parameters.Add("@SavedQueryID", SqlDbType.Int);
                        objOleDbCmd.Parameters["@SavedQueryID"].Value = SQLQueryID;
                        objOleDbCmd.Parameters.Add("@Params", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Params"].Value = strParams;
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

        public DataSet GetMessages(string processId, string processSubTypeId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strParams = string.Empty;
            try
            {

                if (SQLQueryID == "0")
                {
                    strParams = GenerateQuery();
                }
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetMessagesWithFilter", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@EventDateFrom", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateFrom"].Value = EventDateFrom;
                        objOleDbCmd.Parameters.Add("@EventDateTo", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventDateTo"].Value = EventDateTo;
                        objOleDbCmd.Parameters.Add("@Cam", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Cam"].Value = Cam;
                        objOleDbCmd.Parameters.Add("@TML", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@TML"].Value = TML;
                        objOleDbCmd.Parameters.Add("@SavedQueryID", SqlDbType.Int);
                        objOleDbCmd.Parameters["@SavedQueryID"].Value = SQLQueryID;
                        objOleDbCmd.Parameters.Add("@Params", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Params"].Value = strParams;
                        objOleDbCmd.Parameters.Add("@processId", SqlDbType.Int).Value = processId;
                        objOleDbCmd.Parameters.Add("@processSubTypeId", SqlDbType.Int).Value = processSubTypeId;
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

        #endregion

        #region Get ImageNames

        public static DataSet GetImages(string strMsgIDList)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string strImages = string.Empty;
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetImagesByMsgIDList", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@MsgIDList", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@MsgIDList"].Value = strMsgIDList;
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

        #endregion ImageNames

        #region Get Event Types

        public static DataSet GetEventTypes(string processId, string processSubTypeId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventTypesByFilter", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@processId", SqlDbType.Int).Value = processId;
                        objOleDbCmd.Parameters.Add("@processSubTypeId", SqlDbType.Int).Value = processSubTypeId;
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


        public static DataSet GetEventTypes()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventTypes", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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

        #endregion


        #region GetEventAttributeTypes

        public static DataSet GetEventAttributeTypes()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventAttributeTypes", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
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

        #endregion


        #region GetEventAttributeSubTypes

        public static DataSet GetEventAttributeSubTypes(string strEvent, string strAttributeType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventAttributeSubTypes", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@Event", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Event"].Value = strEvent;
                        objOleDbCmd.Parameters.Add("@EventAttributeType", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventAttributeType"].Value = strAttributeType;
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

        public static DataSet GetEventAttributeSubTypes(string strEvent, string strAttributeType, string processId, string processSubTypeId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objOleDbCmd = new SqlCommand("sp_GetEventAttributeSubTypesWithFilter", objCon))
                    {
                        objCon.Open();
                        objOleDbCmd.CommandType = CommandType.StoredProcedure;
                        objOleDbCmd.Parameters.Add("@Event", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@Event"].Value = strEvent;
                        objOleDbCmd.Parameters.Add("@EventAttributeType", SqlDbType.VarChar);
                        objOleDbCmd.Parameters["@EventAttributeType"].Value = strAttributeType;
                        objOleDbCmd.Parameters.Add("@processId", SqlDbType.VarChar).Value = processId;
                        objOleDbCmd.Parameters.Add("@processSubTypeId", SqlDbType.VarChar).Value = processSubTypeId;
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
        #endregion


        #region Save Filtered Messages

        public string SaveFilteredMessages()
        {
            try
            {
                string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
                using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
                {
                    using (SqlCommand objSqlCmd = new SqlCommand("sp_SaveFilteredMessages", objCon))
                    {
                        objCon.Open();
                        objSqlCmd.CommandType = CommandType.StoredProcedure;
                        objSqlCmd.Parameters.Add("@FilteredMessageName", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@FilteredMessageName"].Value = this.FilteredMessageName;
                        objSqlCmd.Parameters.Add("@FilteredMessage", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@FilteredMessage"].Value = this.FilteredMessage;
                        objSqlCmd.Parameters.Add("@FilteredMessageDescription", SqlDbType.VarChar);
                        objSqlCmd.Parameters["@FilteredMessageDescription"].Value = this.ProcessId + "-" + this.ProcessSubTypeId + "-" + this.SQLQueryID + this.EventType + "-" + this.EventAttr + "-" + this.EventAttrSub + "-" + this.Cam + "-" + this.TML + this.EventDateFrom + "-" + this.EventDateTo;
                        int result = objSqlCmd.ExecuteNonQuery();

                        if (result == -1)
                        {
                            return "Error, Duplicate Filtered Message Name.";
                        }
                        else
                        {
                            return "Filtered Message saved successfully.";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return "Error, Query not saved. Please try again.";
            }
        }

        #endregion

    }

}