using System.ComponentModel.DataAnnotations;

namespace Entrytask_Urban.Models
{
    public class GetSalelinesTransactions
    {
        [Key]
        public int SaleLineId { get; set; }
        public int SaleLineTransactionKey { get; set; }
        public int ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Discount { get; set; }
        public double VATRate { get; set; }
        public double VAT { get; set; }
    }
}
