using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sakregister.Classes;

namespace Sakregister
{
    public partial class upload : Page
    {
        private string SortColumn
        {
            get { return Convert.ToString(ViewState["SortColumn"]); }
            set { ViewState["SortColumn"] = value; }
        }

        private string SortDirection
        {
            get {return Convert.ToString(ViewState["SortDirection"]); } 
            set { ViewState["SortDirection"] = value; }
        }


        protected void Page_Load( object sender , EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                            btnSubmit.Value = "Ladda upp för verifiering";

                SortDirection = "DESC";
                SortColumn = "Nr1";
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var dt = GetTableData();
            if ( dt != null && dt.Rows.Count>0 )
            {
                //Sort the data.
                dt.DefaultView.Sort = SortColumn + " " + SortDirection;
                gvSakRegister.DataSource = dt;
                
                gvSakRegister.DataBind();
            }
        }

        private DataTable GetTableData()
        {
            var dta = new DataTable();
            var strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
            using ( var con = new SqlConnection(strConnection) )
            {
                con.Open();

                var cmd = new SqlCommand("Select * from sakregister" , con);
                var adapter = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                adapter.Fill(ds , "sakregister");
                dta = ds.Tables[0];
                adapter.Dispose();
                ds = null;
                cmd.Dispose();
                con.Close();
                return dta;
            }
        }

        protected void DoSubmit( object sender , EventArgs e )
        {
            if ( ulFile.PostedFile != null && ulFile.PostedFile.ContentLength > 0 )
            {
                var fn = Path.GetFileName(ulFile.PostedFile.FileName);
                var SaveLocation = Server.MapPath("Data") + "\\" + fn;
                try
                {
                    ulFile.PostedFile.SaveAs(SaveLocation);
                    Response.Write("Filen är uppladdad");
                    gvSakRegister.DataSource = InsertExcelRecords(SaveLocation , cbPreview.Checked);
                    gvSakRegister.DataBind();
                }
                catch ( Exception ex )
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            else
            {
                Response.Write("Välj fil att ladda upp");
            }
        }

        private List< RegistryItem > InsertExcelRecords( string filePath , bool cbPreviewChecked )
        {
            try
            {
                var strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
                //string query = "Select [Nr],[Ar],[Ord],[Arende],[Betankande],[Skrivelse],[Protokoll] from [Sheet1$]";
                var query = "Select * from [Sheet1$]";
                var excelConnString = string.Format(
                    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"" , filePath);
                //Create Connection to Excel work book 
                using ( var connExcel = new OleDbConnection(excelConnString) )
                {
                    var regItems = new List< RegistryItem >();
                    //Create OleDbCommand to fetch data from Excel 
                    using ( var cmdExcel = new OleDbCommand(query , connExcel) )
                    {
                        if ( !cbPreviewChecked )
                        {
                            using ( var odaExcel = new OleDbDataAdapter() )
                            {
                                var dt = new DataTable();
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                var dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables , null);
                                var sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();

                                foreach ( DataRow row in dt.Rows )
                                    regItems.Add(
                                        new RegistryItem
                                        {
                                            Ar = row["Ar"].ToString() ,
                                            Ord = row["Ord"].ToString() ,
                                            Arende = row["Arende"].ToString() ,
                                            Betankande = row["Betankande"].ToString() ,
                                            Skrivelse = row["Skrivelse"].ToString() ,
                                            Protokoll = row["Protokoll"].ToString()
                                        });
                            }
                            btnSubmit.Value = "verifiera & markera för uppladdning.";
                        }
                        else
                        {
                            connExcel.Open();
                            using ( var dReader = cmdExcel.ExecuteReader() )
                            {
                                using ( var sqlBulk = new SqlBulkCopy(strConnection) )
                                {
                                    sqlBulk.ColumnMappings.Add("Ar" , "Ar");
                                    sqlBulk.ColumnMappings.Add("Ord" , "Ord");
                                    sqlBulk.ColumnMappings.Add("Arende" , "Arende");
                                    sqlBulk.ColumnMappings.Add("Betankande" , "Betankande");
                                    sqlBulk.ColumnMappings.Add("Skrivelse" , "Skrivelse");
                                    sqlBulk.ColumnMappings.Add("Protokoll" , "Protokoll");

                                    sqlBulk.DestinationTableName = "sakregister";
                                    sqlBulk.WriteToServer(dReader);
                                }
                            }
                            BindGrid();
                        }
                    }
                    return regItems;
                }
            }
            catch ( Exception ex )
            {
                Response.Write("Error: " + ex.Message);
                return new List< RegistryItem >();
            }
        }

        protected void GvRowDeleting( object sender , GridViewDeleteEventArgs e )
        {
            try
            {
                var strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
                using ( var con = new SqlConnection(strConnection) )
                {
                    var GridView1 = ( GridView ) sender;
                    var row = e.RowIndex;
                    var rowId = GridView1.DataKeys[row].Values[0].ToString();

                    var cmd = new SqlCommand("delete from sakregister where Nr1=" + rowId , con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BindGrid();
                }
            }
            catch ( Exception ex )
            {
                ScriptManager.RegisterStartupScript(this , GetType() , "alert" , "alert('" + ex.Message + "');" , true);
            }

            // Refresh the GridView
        }

        protected void gvPageIndexChanged( object sender , GridViewPageEventArgs e )
        {
            gvSakRegister.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvSorting( object sender , GridViewSortEventArgs e )
        {
            SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";
            SortColumn = e.SortExpression;
            BindGrid();
        }

        protected void gvDatabound( object sender , EventArgs e )
        {
            var columnIndex = 0;
            foreach ( DataControlFieldHeaderCell headerCell in gvSakRegister.HeaderRow.Cells )
                if ( headerCell.ContainingField.SortExpression == SortColumn )
                {
                    columnIndex = gvSakRegister.HeaderRow.Cells.GetCellIndex(headerCell);
                    break;
                }

            var sortImage = new Image();
            sortImage.ImageUrl = string.Format("images/{0}.png" , SortDirection);
            gvSakRegister.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
        }


        protected void gvRowDatabound( object sender , GridViewRowEventArgs e )
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                // reference the Delete LinkButton
                var db = ( LinkButton ) e.Row.Cells[0].Controls[0];

                db.OnClientClick = "return confirm('Du håller på att ta bort en rad, vill du fortsätta?');";
            }
        }
    }
}
