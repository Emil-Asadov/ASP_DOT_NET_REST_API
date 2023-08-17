using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_REST.Models
{
    public class ClassControlsOper
    {
        public int custIdn { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthPlace { get; set; }
        public string birthDate { get; set; }
        public int gender { get; set; }
        public string docNo { get; set; }
        public string finCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
    }
}