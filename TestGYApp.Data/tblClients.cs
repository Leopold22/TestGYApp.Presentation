//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestGYApp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblClients
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