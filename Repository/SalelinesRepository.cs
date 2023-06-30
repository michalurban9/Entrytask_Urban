using Entrytask_Urban.Models;

namespace Entrytask_Urban.Repository
{
    public class SalelinesRepository
    {
        public static readonly string[] ItemNameList = new[]
        {
        "ItemNameName1", "ItemNameName2", "ItemNameName3"
    };

        public static int[] quantityList =
        {
            1,2,3
            };

        public static int[] IttemNumberList =
        {
            1,2,3
            };

        public static int[] PriceList =
        {
            5,7,10
            };
        public static List<GetSalelinesTransactions> GetSalelinesTransactionsList = new() 
        {
            new() { }
            
        };
    }
}
