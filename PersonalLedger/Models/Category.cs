using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLedger.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Tax { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
    }  //class
}  //namespace
