using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.DTO
{
    public partial class Client
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<short> Age { get; set; }
        public Nullable<bool> IsChild { get; set; }
        public Nullable<bool> IsOldMan { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string cl_name { get; set; }
    }
}
