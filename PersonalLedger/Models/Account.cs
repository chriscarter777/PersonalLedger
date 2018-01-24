using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLedger.Models
{
    public class Account
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }
        public bool Debit { get; set; }
        public int? DefaultAcct { get; set; }
        public decimal? DefaultAmt { get; set; }
        public int? DefaultCat { get; set; }
        public string Institution { get; set; }
        public decimal Interest { get; set; }
        public decimal? Limit { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public bool Owned { get; set; }
        public int User { get; set; }
    }  //class
}  //namespace
