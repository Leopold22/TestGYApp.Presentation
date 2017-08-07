using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.DTO
{

     public partial class GlobalSetting
    {

        public int ID { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

    }


    public partial class ReportSettingColumn
    {
        public string Name { get; set; }
        public string DispName { get; set; }
        public int Order { get; set; }
    }

}
