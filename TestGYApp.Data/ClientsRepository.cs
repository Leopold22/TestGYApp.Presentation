using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.Data
{
    public class ClientsRepository
    {

        public static List<DTO.Client> GetClients()
        {
            GY_ContentEntities db = new GY_ContentEntities();
            var clients = db.tblClients.ToList();

            var dtoClients = new List<DTO.Client>();

            foreach (var client in clients)
            {
                var dtoClient = new DTO.Client();

                dtoClient.adress = client.adress;
                dtoClient.card_number = client.card_number;
                dtoClient.child = client.child;
                dtoClient.cl_name = client.cl_name;
                dtoClient.date_of_birth = client.date_of_birth;
                dtoClient.deleted = client.deleted;
                dtoClient.email = client.email;
                dtoClient.id = client.id;
                dtoClient.old_count = client.old_count;
                dtoClient.old_man = client.old_man;
                dtoClient.primechanie = client.primechanie;
                dtoClient.SSMA_TimeStamp = client.SSMA_TimeStamp;
                dtoClient.tel_number = client.tel_number;

                dtoClients.Add(dtoClient);
            }

            return dtoClients;
        }

   

    }
}
