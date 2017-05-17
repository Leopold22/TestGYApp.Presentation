using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;





namespace TestGYApp.Data
{
    public class ClientsRepository
    {

        public static List<DTO.Client> GetClients()
        {
            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();
            var clients = db.Clients.ToList();

            var dtoClients = new List<DTO.Client>();

            foreach (var client in clients)
            {
                var dtoClient = new DTO.Client();

                dtoClient.Address = client.Address;
                dtoClient.Age = client.Age;
                dtoClient.BirthDate = client.BirthDate;
                dtoClient.cl_name = client.cl_name;
                dtoClient.Comment = client.Comment;
                dtoClient.Deleted = client.Deleted;
                dtoClient.Email = client.Email;
                dtoClient.FirstName = client.FirstName;
                dtoClient.FullName = client.FullName;
                dtoClient.ID = client.ID;
                dtoClient.IsChild = client.IsChild;
                dtoClient.IsOldMan = client.IsOldMan;
                dtoClient.LastName = client.LastName;
                dtoClient.Patronymic = client.Patronymic;
                dtoClient.Phone = client.Phone;


                dtoClients.Add(dtoClient);
            }

            return dtoClients;
        }


        public static void AddClient(DTO.Client newClient)
        {

            if (newClient.cl_name.Trim().Length == 0)
                throw new Exception("Name field required");

            var client = new Clients();

            client.cl_name = newClient.cl_name;
            client.BirthDate = newClient.BirthDate;
            client.Age = newClient.Age;
            client.Phone = newClient.Phone;
            client.Comment = newClient.Comment;
            client.Address = newClient.Address;

            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();
            var tblClients = db.Clients;
            tblClients.Add(client);
            db.SaveChanges();


        }

        public static void UpdateClient(int clientID, string firstName, string lastName, string patronymic)
        {
            GY_ContentEntitiesMain db = new GY_ContentEntitiesMain();

            var query = from client in db.Clients
                        where client.ID == clientID
                        select client;
            
            foreach (Clients client in query)
            {
                client.FirstName = firstName.Trim();
                client.LastName = lastName.Trim();
                client.Patronymic = patronymic.Trim();
                client.cl_name = lastName.Trim()  + ' ' + firstName.Trim()  + ' ' +  patronymic.Trim() ;
            }
            db.SaveChanges();
        }



        public static string GetClients(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize)

            
        {
            string query = "[GetClients_Pager]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTermName", searchTermName);
            cmd.Parameters.AddWithValue("@searchTermLastName", searchTermLastName);
            cmd.Parameters.AddWithValue("@searchTermPatronymic", searchTermPatronymic);
            cmd.Parameters.AddWithValue("@SearchTermPhone", searchTermPhone);
            cmd.Parameters.AddWithValue("@SearchTermMarketingInfo", searchTermMarketingInfo);
            cmd.Parameters.AddWithValue("@AgeFrom", searchTermAgeFrom);
            cmd.Parameters.AddWithValue("@AgeTo", searchTermAgeTo);
            cmd.Parameters.AddWithValue("@BirthDateFrom", searchTermBirthDateFrom);
            cmd.Parameters.AddWithValue("@BirthDateTo", searchTermBirthDateTo);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetData(cmd, pageIndex, pageSize).GetXml(); 
        }

        public static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize)
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




        //Excel эксперимент


        public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, int pageIndex, int pageSize)


        {
            
            string query = "[GetClients_ReportPager]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTermName", searchTermName);
            cmd.Parameters.AddWithValue("@searchTermLastName", searchTermLastName);
            cmd.Parameters.AddWithValue("@searchTermPatronymic", searchTermPatronymic);
            cmd.Parameters.AddWithValue("@SearchTermPhone", searchTermPhone);
            cmd.Parameters.AddWithValue("@SearchTermMarketingInfo", searchTermMarketingInfo);
            cmd.Parameters.AddWithValue("@AgeFrom", searchTermAgeFrom);
            cmd.Parameters.AddWithValue("@AgeTo", searchTermAgeTo);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetDataForExcel(cmd, pageIndex, pageSize);
        }



        public static DataTable GetDataForExcel(SqlCommand cmd, int pageIndex, int pageSize)
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
                       
                        return ds.Tables["Clients"];
                    }

                }
            }
        }



    


    }
}
