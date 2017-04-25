using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.DTO
{
    public partial class Client
    {
        public int id { get; set; }
        public string card_number { get; set; }
        public string cl_name { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public Nullable<short> old_count { get; set; }
        public Nullable<bool> child { get; set; }
        public Nullable<bool> old_man { get; set; }
        public string adress { get; set; }
        public string tel_number { get; set; }
        public string email { get; set; }
        public string primechanie { get; set; }
        public Nullable<bool> deleted { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
