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


        public static void UpdateClient(int clientID, string firstName, string lastName, string patronymic)
        {
            Data.ClientsRepository.UpdateClient(clientID, firstName, lastName, patronymic);
        }

        public static string GetClients(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, int pageSize)
            
        {
            return Data.ClientsRepository.GetClients(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize);
        }

        public static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize)
        {
            return Data.ClientsRepository.GetData(cmd, pageIndex, pageSize);
        }









        public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, int pageIndex, int pageSize)

        {
            return Data.ClientsRepository.GetClientsForExcel(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, pageIndex, pageSize);
        }

        public static DataTable GetDataForExcel(SqlCommand cmd, int pageIndex, int pageSize)
        {
            return Data.ClientsRepository.GetDataForExcel(cmd, pageIndex, pageSize);
        }







    }
}
