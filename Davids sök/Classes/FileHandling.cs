using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Sakregister.Classes
{
    public class FileHandling
    {
        private void SaveFileToDatabase(string filePath)
        {
            String strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();

            String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);
            //Create Connection to Excel work book 
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
            {
                //Create OleDbCommand to fetch data from Excel 
                using (OleDbCommand cmd = new OleDbCommand("Select [nr],[ar],[ord],[arende],[betankande],[skrivelse],[protokoll] from [Sheet1$]", excelConnection))
                {
                    excelConnection.Open();
                    using (OleDbDataReader dReader = cmd.ExecuteReader())
                    {
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection))
                        {
                            //Give your Destination table name 
                            sqlBulk.DestinationTableName = "sakregister";
                            sqlBulk.WriteToServer(dReader);
                        }
                    }
                }
            }
        }


        private string GetLocalFilePath(string saveDirectory, FileUpload fileUploadControl)
        {


            string filePath = Path.Combine(saveDirectory, fileUploadControl.FileName);

            fileUploadControl.SaveAs(filePath);

            return filePath;

        }
    }
}