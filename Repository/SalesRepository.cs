using Entrytask_Urban.Models;

namespace Entrytask_Urban.Repository
{
    public class SalesRepository
    {
        public static readonly string[] GuidList = new[]
        {
        "Guid1", "Guid2", "Guid3"
    };

        public static readonly string[] LocationNameList = new[]
        {
        "Denver", "Colorado", "Washington"
    };

        public static readonly string[] DeviceNameList = new[]
        {
        "Device1", "Device2", "Device3"
    };

        public static readonly string[] CashierNameList = new[]
{
        "Veronika", "Maria", "Kristina"
    };

        public static readonly string[] CustomerNameList = new[]
        {
        "Michal", "Jozed", "David"
    };
        

        public static List<GetSalesTransactions> GetSalesTransactionsList = new()
        {
            new() {/* TransactionKey = 1, TransactionDate = DateTime.Now , Guid = GuidList[Random.Shared.Next(GuidList.Length)],
            TicketNumber = 1, LocationName = LocationNameList[Random.Shared.Next(LocationNameList.Length)],
            DeviceName = DeviceNameList[Random.Shared.Next(DeviceNameList.Length)],
            CashierName = CashierNameList[Random.Shared.Next(CashierNameList.Length)],
            CustomerName = CustomerNameList[Random.Shared.Next(CustomerNameList.Length)],
            SaleLineId = 1, SaleLineTransactionKey = 1 ,
            ItemName = ItemNameList[Random.Shared.Next(ItemNameList.Length)],
            ItemNumber = 1, Quantity=1, Price = 5, Discount = 0, VATRate=20 ,VAT = ((5 * 1)-5*0) * 0.20 */
            }
        };

        
    }
}
