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



    public class ClientFilterObject
    {

        public string sortOrder { get; set; }
        public string searchTermName { get; set; }
        public string searchTermLastName { get; set; }
        public string searchTermPatronymic { get; set; }
        public string searchTermPhone { get; set; }
        public string searchTermMarketingInfo { get; set; }
        public int? searchTermAgeFrom { get; set; }
        public int? searchTermAgeTo { get; set; }
        public string searchTermBirthDateFrom { get; set; }
        public string searchTermBirthDateTo { get; set; }
        public int pageIndex { get; set; }


        //дефолтный конструктор
        public ClientFilterObject() {

            sortOrder = "FullName asc";  //сортировка по умолчанию: ФИО
            searchTermName = "";
            searchTermLastName = "";
            searchTermPatronymic = "";
            searchTermPhone = "";
            searchTermMarketingInfo = "";
            searchTermAgeFrom = null;
            searchTermAgeTo = null;
            searchTermBirthDateFrom = null;
            searchTermBirthDateTo = null;
            pageIndex = 1;
        }


    }


}
