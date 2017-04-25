using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestGYApp.Presentation
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var clients = Business.ClientsManager.GetClients();
            ClientsGridView.DataSource = clients;
            ClientsGridView.DataBind();

            //foreach (DataGridColumn  gridColumn in ClientsGridView.Rows)
            //{
            //    var cell = gridRow.Cells[1];
            //    var value = 
            //    if (cell == null) cell = 

            //}



        }

        protected void ClientsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClientsGridView.PageIndex = e.NewPageIndex;
            ClientsGridView.DataBind();
        }

        protected void ClientsGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //
        }

        protected void ApplyFiltersButton_Click(object sender, EventArgs e)
        {

            //(ClientsGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name = '{0}'", NameFilterTextBox.Text);

            //var bd = (BindingSource)ClientsGridView.DataSource;
            //var dt = (DataTable)bd.DataSource;
            //dt.DefaultView.RowFilter = string.Format("LibService like '%{0}%'", NameFilterTextBox.Text.Trim().Replace("'", "''"));
            //ClientsGridView.Refresh();



        }

        protected void NameFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}