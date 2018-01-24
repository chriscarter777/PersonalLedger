using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLedger.Models
{
    public class Ledger
    {
        public int[] Accounts { get; set; }
        public decimal[] Assets { get; set; }
        public decimal[,] Balances { get; set; }
        public decimal[] Liabilities { get; set; }
        public decimal[] NetWorth { get; set; }
        public int[] Transactions { get; set; }
    }  //class
}  //namespace
