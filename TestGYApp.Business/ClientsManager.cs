using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.Business
{
    public class ClientsManager
    {

        public static List<DTO.Client> GetClients()
        {
            var clients = Data.ClientsRepository.GetClients();
            return clients;
        }


        public static void AddClient(DTO.Client client)
        {
            Data.ClientsRepository.AddClient(client);
        }


        public static void UpdateClient(int clientID, string firstName, string lastName, string patronymic, string email)
        {
            Data.ClientsRepository.UpdateClient(clientID, firstName, lastName, patronymic, email);
        }

        //получение элементов списка (всех, либо для конкретной страницы)
        public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize, bool getAllItems)
            
        {
            return Data.ClientsRepository.GetClients(sortOrder, searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize, getAllItems);
        }

        public static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize, string sortOrder, bool getAllItems)
        {
            return Data.ClientsRepository.GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems);
        }





        //универсализация эксперимент


        

       // public static string GetClientsPageGrid(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize)
        public static string GetClientsPageGrid(DTO.ClientFilterObject filters, int pageIndex, int pageSize)

        {
            return Data.ClientsRepository.GetClientsPageGrid(filters,  pageIndex,  pageSize);
        }



        ////для всех  для данной страницы
        //public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize)

        //{
        //    bool getAllItems = false;
        //    return Data.ClientsRepository.GetClients(sortOrder, searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize, getAllItems);
        //}

        //public static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize, string sortOrder)
        //{
        //    bool getAllItems = false;
        //    return Data.ClientsRepository.GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems);
        //}











        //Получение таблицы с клиентами для Excel-отчета
        //public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo)
        public static DataTable GetClientsForExcel(DTO.ClientFilterObject filters)

        {
            return Data.ClientsRepository.GetClientsForExcel(filters);
        }

        //public static DataTable GetDataForExcel(SqlCommand cmd, int pageIndex, int pageSize)
        //{
        //    return Data.ClientsRepository.GetDataForExcel(cmd, pageIndex, pageSize);
        //}



        public static DataTable BuildExcelReport(DTO.CheckedItemsInfo checkedItems, DTO.ClientFilterObject filters)
        {
           return Data.ClientsRepository.BuildExcelReport(checkedItems, filters);

        }



    }
}
