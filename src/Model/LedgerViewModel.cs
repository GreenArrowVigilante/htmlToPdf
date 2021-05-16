using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Model
{
    public class LedgerViewModel
    {
        public string PartyName { get; set; }
        public string FinancialPeriod { get; set; }
        public DateTime AsOn { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<Ledger> LedgerDetails { get; set; }
    }

    public class Ledger
    {
        public string Particular { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
