using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace TestGYApp.Presentation
{
    public partial class ClientsButtonFilterPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MyClientsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MyClientsGridView.PageIndex = e.NewPageIndex;
            MyClientsGridView.DataBind();

        }

        protected void NameFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}