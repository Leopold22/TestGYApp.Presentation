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
    public partial class ClientsLiveFilterPage : System.Web.UI.Page
    {
        public static int pageSize = 5;

        private void BindGYDataRow()
        {
            DataTable clients = new DataTable();
            clients.Columns.Add("cl_name");
            clients.Columns.Add("Phone");
            clients.Rows.Add();
            MyClientsGridView.DataSource = clients;
            MyClientsGridView.DataBind();
        }

        [WebMethod]
        public static string GetClients(string searchTerm, int pageIndex)
        {
            string query = "[GetClients_Pager]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetData(cmd, pageIndex).GetXml();
        }


        public static DataSet GetData(SqlCommand cmd, int pageIndex)
        {
            string gyConnString = ConfigurationManager.ConnectionStrings["GY_ContentConnectionString"].ConnectionString;
            using (SqlConnection gyCon = new SqlConnection(gyConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = gyCon;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "Clients");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = pageSize;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }

                }
            }
        }


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