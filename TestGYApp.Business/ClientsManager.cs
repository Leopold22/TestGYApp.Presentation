using System;
using System.Collections.Generic;
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


    }
}
