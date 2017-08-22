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
using System.Xml.Linq;

namespace TestGYApp.Data
{
    public class ClientsRepository
    {
        // Получение коллекции посетителей
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

        public static void UpdateClient(int clientID, string firstName, string lastName, string patronymic, string email)
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
                client.Email = email;
            }


            db.SaveChanges();
        }


        // текущая реализация фильтрации
        public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize, bool getAllItems)

            
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
            return GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems).GetXml(); 
        }

        public static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize, string sortOrder, bool getAllItems)
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
                        int startRecord = (pageIndex - 1) * pageSize + 1; //порядковый номер первой записи
                        int endRecord = (((pageIndex - 1) * pageSize + 1) + pageSize) - 1; //порядковый номер последней записи
                        DataTable clients = new DataTable();
                        sda.Fill(clients);  //заполнили таблицу clients выборкой, которую возвращает хранимая процедура       

                        //сортировка
                        DataView dvClients = new DataView();
                        dvClients = clients.AsDataView();
                        dvClients.Sort = sortOrder; // "FullName asc";
                        clients = dvClients.ToTable("Clients");


                        if (getAllItems)
                        {
                           // int clientsColumnsCount = clients.Columns.Count;
                            for (int i = clients.Columns.Count - 1; i >= 0; i--)
                            {
                                if (clients.Columns[i].ColumnName != "ID")
                                {
                                    clients.Columns.RemoveAt(i);
                                }
                            }


                            ds.Tables.Add(clients);
                            
                        }
                        else {
                            //заполняем результирующую таблицу данными для конкретной страницы
                            DataTable clientsCurrentPage = clients.Clone(); //копируем таблицу clients без содержимого

                            //заполняем clientsCurrentPage записями из clients, которые попадают в интервал строк для текущей страницы 
                            for (int i = startRecord - 1; i <= endRecord - 1; i++)
                            {
                                if (i > clients.Rows.Count - 1) { break; };


                                var desRow = clientsCurrentPage.NewRow();
                                var sourceRow = clients.Rows[i];
                                desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                                clientsCurrentPage.Rows.Add(desRow);

                            }


                            ds.Tables.Add(clientsCurrentPage); //добавили clientsCurrentPage в датасет

                            // добавляем пейджер
                            DataTable dt = new DataTable("Pager");
                            dt.Columns.Add("PageIndex");
                            dt.Columns.Add("PageSize");
                            dt.Columns.Add("RecordCount");
                            dt.Rows.Add();
                            dt.Rows[0]["PageIndex"] = pageIndex;
                            dt.Rows[0]["PageSize"] = pageSize;
                            dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                            ds.Tables.Add(dt);
                        }
                        

                        //  sda.Fill(ds, "Clients");

                        return ds;
                    }

                }
            }
        }




        //универсализация

        //public static string GetClientsPageGrid(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize )
        public static string GetClientsPageGrid(DTO.ClientFilterObject filters, int pageIndex, int pageSize )
        {
            
            using (DataSet ds = new DataSet())
            {
                int startRecord = (pageIndex - 1) * pageSize + 1; //порядковый номер первой записи
                int endRecord = (((pageIndex - 1) * pageSize + 1) + pageSize) - 1; //порядковый номер последней записи

                //заполнили таблицу clients выборкой, которую возвращает GetFilteredClientsTable
                DataTable clients = new DataTable();
                clients = GetFilteredClientsTable(filters);


                //DataTable clients = new DataTable();
                //sda.Fill(clients);  //заполнили таблицу clients выборкой, которую возвращает хранимая процедура       

                //сортировка
                DataView dvClients = new DataView();
                dvClients = clients.AsDataView();
                dvClients.Sort = filters.sortOrder; // "FullName asc";
                clients = dvClients.ToTable("Clients");


                //заполняем результирующую таблицу данными для конкретной страницы
                DataTable clientsCurrentPage = clients.Clone(); //копируем таблицу clients без содержимого

                //заполняем clientsCurrentPage записями из clients, которые попадают в интервал строк для текущей страницы 
                for (int i = startRecord - 1; i <= endRecord - 1; i++)
                {
                    if (i > clients.Rows.Count - 1) { break; };


                    var desRow = clientsCurrentPage.NewRow();
                    var sourceRow = clients.Rows[i];
                    desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    clientsCurrentPage.Rows.Add(desRow);

                }


                ds.Tables.Add(clientsCurrentPage); //добавили clientsCurrentPage в датасет

                // добавляем пейджер
                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = pageIndex;
                dt.Rows[0]["PageSize"] = pageSize;
                //dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                int recordCount = clients.Rows.Count;
                dt.Rows[0]["RecordCount"] = recordCount;
                ds.Tables.Add(dt);


                return ds.GetXml();
            }

            


        }



            //получение отфильтрованной таблицы Clients (базовый метод для фильтрации, вызова excel-отчета и получения списка ID при клике на общий чекбокс)
            //public static DataTable GetFilteredClientsTable(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo)
            public static DataTable GetFilteredClientsTable(DTO.ClientFilterObject filters)

        {




            string query = "[GetClients_Pager]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTermName", filters.searchTermName);
            cmd.Parameters.AddWithValue("@searchTermLastName", filters.searchTermLastName);
            cmd.Parameters.AddWithValue("@searchTermPatronymic", filters.searchTermPatronymic);
            cmd.Parameters.AddWithValue("@SearchTermPhone", filters.searchTermPhone);
            cmd.Parameters.AddWithValue("@SearchTermMarketingInfo", filters.searchTermMarketingInfo);
            cmd.Parameters.AddWithValue("@AgeFrom", filters.searchTermAgeFrom);
            cmd.Parameters.AddWithValue("@AgeTo", filters.searchTermAgeTo);
            cmd.Parameters.AddWithValue("@BirthDateFrom", filters.searchTermBirthDateFrom);
            cmd.Parameters.AddWithValue("@BirthDateTo", filters.searchTermBirthDateTo);
            //cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            //cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
           // return GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems).GetXml();


            string gyConnString = ConfigurationManager.ConnectionStrings["GY_ContentConnectionString"].ConnectionString;
            using (SqlConnection gyCon = new SqlConnection(gyConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = gyCon;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        //int startRecord = (pageIndex - 1) * pageSize + 1; //порядковый номер первой записи
                        //int endRecord = (((pageIndex - 1) * pageSize + 1) + pageSize) - 1; //порядковый номер последней записи
                        DataTable clients = new DataTable();
                        sda.Fill(clients);  //заполнили таблицу clients выборкой, которую возвращает хранимая процедура       

                        ////сортировка - перенести в фильтрацию
                        //DataView dvClients = new DataView();
                        //dvClients = clients.AsDataView();
                        //dvClients.Sort = sortOrder; // "FullName asc";
                        //clients = dvClients.ToTable("Clients");


                        //if (getAllItems)
                        //{
                        //    // int clientsColumnsCount = clients.Columns.Count;
                        //    for (int i = clients.Columns.Count - 1; i >= 0; i--)
                        //    {
                        //        if (clients.Columns[i].ColumnName != "ID")
                        //        {
                        //            clients.Columns.RemoveAt(i);
                        //        }
                        //    }


                        //    ds.Tables.Add(clients);

                        //}
                        //else
                        //{
                        //    //заполняем результирующую таблицу данными для конкретной страницы
                        //    DataTable clientsCurrentPage = clients.Clone(); //копируем таблицу clients без содержимого

                        //    //заполняем clientsCurrentPage записями из clients, которые попадают в интервал строк для текущей страницы 
                        //    for (int i = startRecord - 1; i <= endRecord - 1; i++)
                        //    {
                        //        if (i > clients.Rows.Count - 1) { break; };


                        //        var desRow = clientsCurrentPage.NewRow();
                        //        var sourceRow = clients.Rows[i];
                        //        desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                        //        clientsCurrentPage.Rows.Add(desRow);

                        //    }


                        //    ds.Tables.Add(clientsCurrentPage); //добавили clientsCurrentPage в датасет

                        //    // добавляем пейджер
                        //    DataTable dt = new DataTable("Pager");
                        //    dt.Columns.Add("PageIndex");
                        //    dt.Columns.Add("PageSize");
                        //    dt.Columns.Add("RecordCount");
                        //    dt.Rows.Add();
                        //    dt.Rows[0]["PageIndex"] = pageIndex;
                        //    dt.Rows[0]["PageSize"] = pageSize;
                        //    dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        //    ds.Tables.Add(dt);
                        //}


                        //  sda.Fill(ds, "Clients");

                        return clients;
                    }

                }
            }
        }
















        






        //Получение таблицы с клиентами для Excel-отчета
        public static DataTable GetClientsForExcel(DTO.ClientFilterObject filters)


        {
            //получили полную таблицу Clients с учетом фильтров
            DataTable clients = new DataTable();
            //clients = GetFilteredClientsTable(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo);
            clients = GetFilteredClientsTable(filters);


            // УБИРАЕМ ЛИШНИЕ КОЛОНКИ


            //исходный набор колонок
            string[] columnNames = (from clientColumn in clients.Columns.Cast<DataColumn>()
                                    select clientColumn.ColumnName).ToArray();

            //колонки, которые должны присутствовать в отчете
             string[] reportColumns = new string[] { "FullName", "Email", "Age" };


            string reportSettingValue = SettingsRepository.GetGlobalSettingValue("ClientsReport");


            // получаем объект настроек
            var  reportSetting = XDocument.Parse(reportSettingValue);

           
            var reportSettingColumns = (from r in reportSetting.Root.Elements("column")
                                        select new DTO.ReportSettingColumn()
                         {
                             Name = (string)r.Element("name"),
                             DispName = (string)r.Element("dispName"),
                             Order = (int)r.Element("order")
                         }).ToList();



            DataTable excelClients = clients.Clone();


            for (int i = 1; i <= reportSettingColumns.Count(); i++)
            {
                DTO.ReportSettingColumn repColumn = reportSettingColumns.Find(item => item.Order == i);
                string name = repColumn.Name;
                string dispName = repColumn.DispName;


                if (clients.Columns.Contains(name))
                {
                    clients.Columns[name].SetOrdinal(i-1);
                    clients.Columns[name].ColumnName = dispName;
                }

             //   excelClients

            }

            int clientsColumnsCount = clients.Columns.Count;

            for (int i = clientsColumnsCount; i > reportSettingColumns.Count(); i--)
            {
                clients.Columns.RemoveAt(i-1);
            }
            


            ////удаляем лишние колонки
            //List<DataColumn> columnsToRemove = new List<DataColumn> { };

            //foreach (DataColumn column in clients.Columns)
            //{
            //    int columnPosition = Array.IndexOf(reportColumns, column.ColumnName);
            //    if (columnPosition == -1) {
            //       columnsToRemove.Add(column);
            //    }
            //}

            
            //foreach (DataColumn column in columnsToRemove)
            //{
            //    clients.Columns.Remove(column.ColumnName);
            //}

   


            return clients;


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



        public static DataTable BuildExcelReport(DTO.CheckedItemsInfo checkedItems, DTO.ClientFilterObject filters)
        {
            //DataSet ds = new DataSet();
            DataTable clients = GetClientsForExcel(filters);
            clients.PrimaryKey = new DataColumn[] { clients.Columns["ID"] };
            // ds.Tables.Add(clients);

            DataTable excelClients = clients.Clone();


            if (checkedItems.GeneralCheckboxChecked)
            {
                foreach (var item in checkedItems.UncheckedItemsArray)
                {
                    // DataRow row = clients.Rows.Find(item);
                    int rowIndex = clients.Rows.IndexOf(clients.Rows.Find(item));
                    clients.Rows.RemoveAt(rowIndex);
                }
            }
            else
            {
                foreach (var item in checkedItems.CheckedItemsArray)
                {
                    
                    // DataRow row = clients.Rows.Find(item);
                    int rowIndex = clients.Rows.IndexOf(clients.Rows.Find(item));
                    clients.Rows.RemoveAt(rowIndex);

                    //excelClients.
                }

            }


            return clients;

        }





    }
}
