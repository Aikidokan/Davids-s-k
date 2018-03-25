using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sakregister
{
    public partial class UploadFile : System.Web.UI.Page
    {
        private string SqlTableDataSortColumn
        {
            get { return Convert.ToString(ViewState["SqlTableDataSortColumn"]); }
            set { ViewState["SqlTableDataSortColumn"] = value; }
        }

        private string SqlTableDataSortDirection
        {
            get { return Convert.ToString(ViewState["SqlTableDataSortDirection"]); }
            set { ViewState["SqlTableDataSortDirection"] = value; }
        }

        private string ImportTableDataSortColumn
        {
            get { return Convert.ToString(ViewState["ImportTableDataSortColumn"]); }
            set { ViewState["ImportTableDataSortColumn"] = value; }
        }

        private string ImportTableDataSortDirection
        {
            get { return Convert.ToString(ViewState["ImportTableDataSortDirection"]); }
            set { ViewState["ImportTableDataSortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // The Page is accessed for the first time. 
            if (!IsPostBack)
            {
                SqlTableDataSortColumn = "Id";
                SqlTableDataSortDirection = "DESC";

                ImportTableDataSortDirection = "DESC";
                ImportTableDataSortColumn = "Id";

                btnInsertToSql.Visible = false;
                btnUpload.Visible = true;
                lblStatus.Text = "";
            }


        }

        protected void DoUpload(object sender, EventArgs e)
        {
            if (fuImportfile.PostedFile != null && fuImportfile.PostedFile.ContentLength > 0)
            {
                var fn = Path.GetFileName(fuImportfile.PostedFile.FileName);
                var SaveLocation = Server.MapPath("Data") + "\\" + fn;
                try
                {
                    fuImportfile.PostedFile.SaveAs(SaveLocation);

                    AddToDatatable(SaveLocation);

                    DataTable dt = AddToDatatable(SaveLocation);
                    int numrows = dt.DefaultView.Count;
                    gvFileContent.Caption = Path.GetFileName(SaveLocation) + " Antal rader: " + numrows;
                    gvFileContent.DataSource = dt;
                    gvFileContent.DataBind();
                    btnInsertToSql.Visible = true;
                    btnUpload.Visible = false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                Response.Write("Välj fil att ladda upp");
            }
        }

        private DataTable AddToDatatable(string saveLocation)
        {

            var excelConnString = string.Format(
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", saveLocation);
            //Create Connection to Excel work book 
            try
            {
                using (var connExcel = new OleDbConnection(excelConnString))
                {

                    var query = "Select * from [Sheet1$]";
                    using (var cmdExcel = new OleDbCommand(query, connExcel))
                    {
                        using (var da = new OleDbDataAdapter())
                        {

                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            var dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            var sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            //int numrows =  TargetWorksheet1.UsedRange.Rows.Count - 1;
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText =
                                "SELECT Ar,Ord,Arende,Betankande,Skrivelse,Protokoll From [" + sheetName + "]";
                            da.SelectCommand = cmdExcel;
                            connExcel.Close();

                            var tmpdt = new DataTable();
                            da.Fill(tmpdt);
                            var dt = new DataTable();
                            //Add columns to DataTable.
                            dt.Columns.AddRange(
                                new DataColumn[7]
                                {
                                    new DataColumn("Id" , Type.GetType("System.Int32")) ,
                                    new DataColumn("Ar") , new DataColumn("Ord") ,
                                    new DataColumn("Arende") , new DataColumn("Betankande") ,
                                    new DataColumn("Skrivelse") , new DataColumn("Protokoll")
                                });

                            //Set AutoIncrement True for the First Column.
                            dt.Columns["Id"].AutoIncrement = true;

                            //Set the Starting or Seed value.
                            dt.Columns["Id"].AutoIncrementSeed = 1;

                            //Set the Increment value.
                            dt.Columns["Id"].AutoIncrementStep = 1;

                            foreach (DataRow row in tmpdt.Rows)
                            {

                                dt.ImportRow(row);

                            }

                            // Set the sort column and sort order. 
                            dt.DefaultView.Sort = ImportTableDataSortColumn + " " + ImportTableDataSortDirection;
                            ViewState["ImportFileContent"] = dt;

                            return dt;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());

            }
        }

        protected void gvImportFileContent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFileContent.EditIndex = e.NewEditIndex;
            BindImportFileContentGridView();

        }

        protected void gvImportFileContentSorting(object sender, GridViewSortEventArgs e)
        {
            //ImportTableDataSortDirection = 
            ImportTableDataSortDirection = ImportTableDataSortDirection == "ASC" ? "DESC" : "ASC";
            ImportTableDataSortColumn = e.SortExpression;
            BindImportFileContentGridView();
        }

        private void BindImportFileContentGridView()
        {
            if (ViewState["ImportFileContent"] != null)
            {
                // Get the DataTable from ViewState. 
                var dt = (DataTable)ViewState["ImportFileContent"];

                // Set the sort column and sort order. 
                dt.DefaultView.Sort = ImportTableDataSortColumn + " " + ImportTableDataSortDirection;


                // Bind the GridView control. 
                gvFileContent.DataSource = dt.DefaultView;
                gvFileContent.DataBind();
            }
        }

        protected void GvImportGridViewRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvFileContent.EditIndex = -1;
            BindImportFileContentGridView();

        }

        protected void GvImportGridViewOnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the index of the new display page.  
            gvFileContent.PageIndex = e.NewPageIndex;


            // Rebind the GridView control to  
            // show data in the new page. 
            BindImportFileContentGridView();
        }

        private void BindSqlTableGridView()
        {
            if (ViewState["SqlTableData"] != null)
            {
                // Get the DataTable from ViewState. 
                var dta = (DataTable)ViewState["SqlTableData"];
                dta.DefaultView.Sort = SqlTableDataSortColumn + " " + SqlTableDataSortDirection;
                gvSqlTableSakRegister.DataSource = dta.DefaultView;
                gvSqlTableSakRegister.DataBind();

            }
        }
        protected void gvImportFileDatabound(object sender, EventArgs e)
        {
            var columnIndex = 0;
            foreach (DataControlFieldHeaderCell headerCell in gvFileContent.HeaderRow.Cells)
                if (headerCell.ContainingField.SortExpression == ImportTableDataSortColumn)
                {
                    columnIndex = gvFileContent.HeaderRow.Cells.GetCellIndex(headerCell);
                    break;
                }

            var sortImage = new Image();
            sortImage.ImageUrl = string.Format("images/{0}.png", ImportTableDataSortDirection);
            gvFileContent.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
        }

        private DataTable GetSqlTableData()
        {
            var dta = new DataTable();
            var strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
            using (var con = new SqlConnection(strConnection))
            {
                con.Open();
                var cmd = new SqlCommand("Select * from sakregistret", con);
                var sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(dta);
                ViewState["SqlTableData"] = dta;
                return dta;
            }
        }

        protected void gvSqlTableDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var strConnection = ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
                using (var con = new SqlConnection(strConnection))
                {
                    var gvSqlTableData = (GridView)sender;
                    var row = e.RowIndex;
                    var rowId = gvSqlTableData.DataKeys[row].Values[0].ToString();

                    var cmd = new SqlCommand("delete from sakregistret where Id=" + rowId, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BindSqlTableGridView();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

            // Refresh the GridView
        }

        protected void gvSqlTablePageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvSqlTableSakRegister.PageIndex = e.NewPageIndex;
            BindSqlTableGridView();
        }

        protected void gvSqlTableSorting(object sender, GridViewSortEventArgs e)
        {
            SqlTableDataSortDirection = SqlTableDataSortDirection == "ASC" ? "DESC" : "ASC";
            SqlTableDataSortColumn = e.SortExpression;
            BindSqlTableGridView();
        }



        protected void gvSqlTableDatabound(object sender, EventArgs e)
        {
            var columnIndex = 0;
            foreach (DataControlFieldHeaderCell headerCell in gvSqlTableSakRegister.HeaderRow.Cells)
                if (headerCell.ContainingField.SortExpression == SqlTableDataSortColumn)
                {
                    columnIndex = gvSqlTableSakRegister.HeaderRow.Cells.GetCellIndex(headerCell);
                    break;
                }

            var sortImage = new Image();
            sortImage.ImageUrl = string.Format("images/{0}.png", SqlTableDataSortDirection);
            gvSqlTableSakRegister.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
        }


        protected void gvSqlTableRowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                var db = (LinkButton)e.Row.Cells[0].Controls[0];

                db.OnClientClick = "return confirm('Du håller på att ta bort en rad, vill du fortsätta?');";
            }
        }

        protected void DoShowSqlTableData(object sender, EventArgs e)
        {
            FillSqlData();
        }

        private void FillSqlData()
        {
            if (ViewState["SqlTableData"] == null)
            {
                DataTable dt = GetSqlTableData();
                dt.DefaultView.Sort = "Id desc";

                gvSqlTableSakRegister.DataSource = dt.DefaultView;
                gvSqlTableSakRegister.DataBind();
            }
            else
            {
                BindSqlTableGridView();
            }
        }

        protected void DoImportFileContent(object sender, EventArgs e)
        {
            if (ViewState["ImportFileContent"] != null)
            {
                try
                {

                    // Get the DataTable from ViewState. 
                    var dtImportFc = (DataTable)ViewState["ImportFileContent"];

                    var strConnection =
                        ConfigurationManager.ConnectionStrings["CSImportConnectionString"].ToString();
                    using (var sqlBulk = new SqlBulkCopy(strConnection))
                    {
                        sqlBulk.ColumnMappings.Add("Ar", "Ar");
                        sqlBulk.ColumnMappings.Add("Ord", "Ord");
                        sqlBulk.ColumnMappings.Add("Arende", "Arende");
                        sqlBulk.ColumnMappings.Add("Betankande", "Betankande");
                        sqlBulk.ColumnMappings.Add("Skrivelse", "Skrivelse");
                        sqlBulk.ColumnMappings.Add("Protokoll", "Protokoll");

                        sqlBulk.DestinationTableName = "sakregistret";
                        sqlBulk.WriteToServer(dtImportFc);
                    }
                    ViewState.Remove("ImportFileContent");
                    ViewState.Remove("SqlTableData");
                    gvFileContent.Visible = false;
                    FillSqlData();
                    lblStatus.Text = "Uppladdad och inlagd";
                    btnInsertToSql.Visible = false;
                    btnUpload.Visible = true;
                    ClearUlForm();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        protected void ClearUlForm()
        {
            fuImportfile.Attributes.Clear();
            //Clear other form fields
        }

        protected void gvFileContent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = gvFileContent.Rows[e.RowIndex]; //Find the row that was clicked for updating.
            gvFileContent.EditIndex = -1; //Change the edit index to -1 .
            if (row != null)
            {

                TextBox Ar = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[2].Controls[0];
                TextBox Ord = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[3].Controls[0];
                TextBox Arende = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[4].Controls[0];
                TextBox Betankande = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[5].Controls[0];
                TextBox Skrivelse = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[6].Controls[0];
                TextBox Protokoll = (TextBox)gvFileContent.Rows[e.RowIndex].Cells[7].Controls[0];
                DataTable dt = (DataTable)ViewState["ImportFileContent"]; ; //Get the values of the datatable from the session variable

                //Traverse through the Datatable till you hit the same row as the row needed to be updated. 

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (e.RowIndex == i)
                    {
                        dt.Rows[i][0] = Ar.Text;
                        dt.Rows[i][1] = Ord.Text;
                        dt.Rows[i][2] = Arende.Text;
                        dt.Rows[i][3] = Betankande.Text;
                        dt.Rows[i][4] = Skrivelse.Text;
                        dt.Rows[i][5] = Protokoll.Text;
                        ViewState.Remove("ImportFileContent");

                        ViewState["ImportFileContent"] = dt;
                        BindImportFileContentGridView();
                    }
                }
            }

        }
    }
}