using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sakregister
{
    public partial class sok : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //if ( Page.IsPostBack )
        //    //{
        //    //    if ( searchTerm.Text != "" )
        //    //    {
        //    //        gvItems.DataSource = GetItemsRecord(searchTerm.Text);
        //    //        gvItems.DataBind();
        //    //    }
        //    //}

        //}

        //public List<Classes.Sakregister> GetItemsRecord(string searchterm)
        //{
        //    sakregisterDataContext db = new sakregisterDataContext();
        //    var listitemsrecord = db.Sakregisters.Where(sakregister => sakregister.Ord == searchterm).ToList();
        //    return listitemsrecord;

        //}

        //protected void btn_search_Click(object sender, EventArgs e)
        //{
        //    gvItems.DataBind();
        //}

        //protected void btn_reset_Click(object sender, EventArgs e)
        //{
        //    searchTerm.Text = "";
        //    gvItems.DataBind();
        //}
        protected void Page_Load( object sender , EventArgs e )
        {
            if ( Page.IsPostBack )
            {
                Response.Redirect(Request.Path + "?ord=" + searchTerm.Text.Trim(), false);
            }
            if ( !string.IsNullOrEmpty(Request.QueryString["ord"]) )
            {
                string searchTerm = Request.QueryString["ord"];
                this.searchTerm.Text = searchTerm;
            }
           
        }



        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTerm.Text.Trim()))
            {
                Response.Redirect(Request.Path+"?ord=" + searchTerm.Text.Trim(),false);
            }
            GridView1.DataBind();
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            searchTerm.Text = "";
            Response.Redirect(Request.Path);
            GridView1.DataBind();
        }

    }
}