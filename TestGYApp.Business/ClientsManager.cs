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

        public static string GetClients(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, DateTime? searchTermBirthDateFrom, DateTime? searchTermBirthDateTo, int pageIndex, int pageSize)
            
        {

            DateTime? testBDF = new DateTime?();
            DateTime? testBDT = new DateTime?();

            //if (searchTermBirthDateFrom == null || searchTermBirthDateTo == null)
            //{
            //    testBDF = null;
            //    testBDF = null;
            //}
            //else
            //{
            //    testBDF = DateTime.Parse(searchTermBirthDateFrom);
            //    testBDT = DateTime.Parse(searchTermBirthDateTo);
            //}
            //    testBDF = null;
            // testBDT = null;

            testBDF = DateTime.Parse("2002-02-01 00:00:00.000");
            testBDT = DateTime.Parse("2015-02-01 00:00:00.000");




            return Data.ClientsRepository.GetClients(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, testBDF, testBDT, pageIndex, pageSize);
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
