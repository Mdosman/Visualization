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
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Upload Messages and Images
/// </summary>
public class Upload
{

	public Upload()
	{
       
	}
    
    public static void LogEvent(string Message)
    {
        StreamWriter writer = null;
        try
        {
            //string LOG_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ApplicationLog.txt");
            string LOG_FILE = @"C:\WebProject\ApplicationLog.txt";
            writer = new StreamWriter(LOG_FILE, true, System.Text.Encoding.ASCII);
            writer.WriteLine("======================================================");
            writer.WriteLine(String.Format("Log Message - {0} - {1}", DateTime.Now.ToString("MM-dd-yyyy:HH-mm-ss-fff", CultureInfo.InvariantCulture), Message));
            writer.WriteLine("======================================================");
            writer.Flush();

        }
        catch { }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }

    public void ClearLog(string Message)
    {
        StreamWriter writer = null;
        try
        {
            string LOG_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ApplicationLog.txt");
            writer = new StreamWriter(LOG_FILE, false, System.Text.Encoding.ASCII);
            writer.Flush();

        }
        catch { }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }

    #region Upload Messages 

    public static void UploadMessagesFromExcel(string UploadMessagesFolder)
    {

        try
        {
            string[] myFiles = Directory.GetFiles(UploadMessagesFolder);

            foreach (string myFile in myFiles)
            {
                LogEvent(string.Format("Processing File Started :{0} ", myFile));
                try
                {
                    bool blnImportedSuccesfully = ParseImportFile(myFile);
                    
                }
                catch (Exception ex)
                {
                    LogEvent(ex.ToString());
                }

                LogEvent(string.Format("Processing File Ended :{0} ", myFile));
            }

        }
        catch (Exception ex)
        {
            LogEvent(ex.ToString());

        }
    }

    private static bool ParseImportFile(string strFile)
    {
        try
        {
            int fileStatus = 0;
            FileInfo objFileInfo = new FileInfo(strFile);
            string strFileName = objFileInfo.Name;
            string[] strName = strFileName.Split('.');
            using (FileStream fs = new FileStream(strFile, FileMode.Open, FileAccess.Read))
            {
                if (objFileInfo != null)
                {
                    #region Perform Sql Bulk Copy

                    string strOleDbConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;\"", objFileInfo.FullName);
                    using (OleDbConnection objOleDbCon = new OleDbConnection(strOleDbConnectionString))
                    {
                        StringBuilder objSQLBuilder = new StringBuilder();

                        objSQLBuilder.AppendLine("SELECT * ");
                        objSQLBuilder.AppendLine(", '" + strName[0] + "' AS [FolderName] ");
                        objSQLBuilder.AppendFormat("     FROM [" + GetExcelSheetNames(objFileInfo.FullName)[0] + "]");

                        using (OleDbCommand objOleDbCmd = new OleDbCommand(objSQLBuilder.ToString(), objOleDbCon))
                        {
                            // Open the connection 
                            objOleDbCon.Open();
                            objOleDbCmd.CommandType = CommandType.Text;
                            OleDbDataReader objOleDbReader = objOleDbCmd.ExecuteReader();
                            string strConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();
                            using (SqlBulkCopy objSqlBulkCopy = new SqlBulkCopy(strConnectionString, SqlBulkCopyOptions.TableLock))
                            {
                                objSqlBulkCopy.BulkCopyTimeout = 3600;
                                objSqlBulkCopy.BatchSize = 500;
                                objSqlBulkCopy.ColumnMappings.Add("FolderName", "FolderName");
                                objSqlBulkCopy.ColumnMappings.Add("MessageID", "MessageID");
                                objSqlBulkCopy.ColumnMappings.Add("SourceID", "SourceID");
                                objSqlBulkCopy.ColumnMappings.Add("Date", "EventDate");
                                objSqlBulkCopy.ColumnMappings.Add("ImageID", "ImageID");
                                objSqlBulkCopy.ColumnMappings.Add("ImageID", "ImageName");
                                objSqlBulkCopy.ColumnMappings.Add("LAT", "Lat");
                                objSqlBulkCopy.ColumnMappings.Add("LON", "Lon");
                                objSqlBulkCopy.ColumnMappings.Add("CamPAN", "CamPAN");
                                objSqlBulkCopy.ColumnMappings.Add("CamTILT", "CamTILT");
                                objSqlBulkCopy.ColumnMappings.Add("EnvLocation", "EnvLocation");
                                objSqlBulkCopy.ColumnMappings.Add("SubLocation", "SubLocation");
                                objSqlBulkCopy.ColumnMappings.Add("Event", "Event");
                                objSqlBulkCopy.ColumnMappings.Add("EventID", "EventID");
                                objSqlBulkCopy.ColumnMappings.Add("EA1", "EA1");
                                objSqlBulkCopy.ColumnMappings.Add("EA2", "EA2");
                                objSqlBulkCopy.ColumnMappings.Add("EA3", "EA3");
                                objSqlBulkCopy.ColumnMappings.Add("EA4", "EA4");
                                objSqlBulkCopy.ColumnMappings.Add("EA5", "EA5");
                                objSqlBulkCopy.ColumnMappings.Add("EA6", "EA6");
                                objSqlBulkCopy.ColumnMappings.Add("GroupID", "GroupID");
                                objSqlBulkCopy.ColumnMappings.Add("Conf", "Conf");

                                objSqlBulkCopy.DestinationTableName = "FRS_Message";
                                objSqlBulkCopy.WriteToServer(objOleDbReader);
                                objSqlBulkCopy.Close();
                            }
                        }


                    }

                    #endregion
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            LogEvent(ex.ToString());
            return false;
        }
        return true;
    }

    private static String[] GetExcelSheetNames(string excelFile)
    {
        OleDbConnection objConn = null;
        System.Data.DataTable dt = null;

        try
        {
            // Connection String. Change the excel file to the file you
            // will search.
            String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
            // Create connection object by using the preceding connection string.
            objConn = new OleDbConnection(connString);
            // Open connection with the database.
            objConn.Open();
            // Get the data table containg the schema guid.
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheets;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            // Clean up.
            if (objConn != null)
            {
                objConn.Close();
                objConn.Dispose();
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
    
    #endregion
    
    #region Upload Images

    public static void UploadImages(string UploadImagesFolder)
    {
        try
        {
            //Get array of file names in the C:\Upload directory.
            string[] myFiles = Directory.GetFiles(UploadImagesFolder);

            foreach (string myFile in myFiles)
            {
                //Copy file to InProcess folder
                //string strFileName = Path.GetFileName(myFile);
                //string strInProcessFile = Path.Combine(InProcessFolder, strFileName);
                //MoveFile(myFile, strInProcessFile);
                FileInfo currentFileToProcessInfo = new FileInfo(myFile);
                LogEvent(string.Format("Processing File Started :{0} ", myFile));
                try
                {
                    string strMIMEType = @"image/bmp";
                    FileStream fs = File.OpenRead(currentFileToProcessInfo.FullName);
                    byte[] imgBytes = new byte[fs.Length];
                    fs.Read(imgBytes, 0, Convert.ToInt32(fs.Length));

                    bool blnImportedSuccesfully = Import(imgBytes, strMIMEType, currentFileToProcessInfo.Name);

                    fs.Close();
                    if (blnImportedSuccesfully)
                    {
                        LogEvent(myFile + " upload successfully.");
                    }
                    else
                    {
                        LogEvent("Error. " + myFile + " could not uploaded");
                    }
                }
                catch (Exception ex)
                {
                    LogEvent(ex.ToString());
                }

                LogEvent(string.Format("Processing File Ended :{0} ", myFile));
            }

        }
        catch (Exception ex)
        {
            LogEvent(ex.ToString());
        }
    }

    private static bool Import(byte[] imgBytes, string strFileType, string strFileName)
    {
        string strStagingDbConnectionString = ConfigurationManager.ConnectionStrings["CTS"].ConnectionString.ToString();

        string[] strTemp = strFileName.Split('-');
        string strFolderName = strTemp[1].Substring(0, 2);
        int intImageNumber;
        if (int.TryParse(strTemp[1].Substring(2, 6), out intImageNumber))
        {
            using (SqlConnection objCon = new SqlConnection(strStagingDbConnectionString))
            {
                using (SqlCommand objOleDbCmd = new SqlCommand("sp_UploadImages", objCon))
                {
                    objCon.Open();
                    objOleDbCmd.CommandType = CommandType.StoredProcedure;

                    objOleDbCmd.Parameters.Add("@FolderName", SqlDbType.VarChar);
                    objOleDbCmd.Parameters["@FolderName"].Value = strFolderName;
                    objOleDbCmd.Parameters.Add("@FileName", SqlDbType.VarChar);
                    objOleDbCmd.Parameters["@FileName"].Value = strFileName;
                    objOleDbCmd.Parameters.Add("@MIMEType", SqlDbType.VarChar);
                    objOleDbCmd.Parameters["@MIMEType"].Value = strFileType;
                    objOleDbCmd.Parameters.Add("@ImageNumber", SqlDbType.Int);
                    objOleDbCmd.Parameters["@ImageNumber"].Value = intImageNumber;
                    objOleDbCmd.Parameters.Add("@ImageBinary", SqlDbType.VarBinary);
                    objOleDbCmd.Parameters["@ImageBinary"].Value = imgBytes;

                    int i = objOleDbCmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
        else
        {
            return false;
        }

        

        return false;
    }

    #endregion
}
