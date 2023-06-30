using System.ComponentModel.DataAnnotations;

namespace Entrytask_Urban.Models
{
    public class GetSalesTransactions
    {
            [Key]
            public int TransactionKey { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Guid { get; set; }
            public int TicketNumber { get; set; }
            public string LocationName { get; set; }
            public string DeviceName { get; set; }
            public string CashierName { get; set; }
            public string CustomerName { get; set; }
        
        
    }
}
