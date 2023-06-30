using Entrytask_Urban.Models;
using Entrytask_Urban.Services;
using Entrytask_Urban.Repository;
using Newtonsoft.Json.Linq;

namespace Entrytask_Urban.Controllers
{
    public class GetSalelinesTransactionsController : ISalelines
    {
        private readonly GetSalesTransactionsDB context;
        private readonly ILogger<GetSalelinesTransactionsController> logger;
        public GetSalelinesTransactionsController(GetSalesTransactionsDB context, ILogger<GetSalelinesTransactionsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public GetSalelinesTransactions Createsalelines(GetSalelinesTransactions getsalelinestransactions)
        {
            int variable = Random.Shared.Next(3);
            //getsalelinestransactions.SaleLineId = SalelinesRepository.GetSalelinesTransactionsList.Count;
            getsalelinestransactions.SaleLineTransactionKey = SalelinesRepository.GetSalelinesTransactionsList.Count;
            getsalelinestransactions.ItemName = SalelinesRepository.ItemNameList[variable];
            getsalelinestransactions.ItemNumber = SalelinesRepository.IttemNumberList[variable];
            getsalelinestransactions.Quantity = Random.Shared.Next(1, 10);
            getsalelinestransactions.Price = SalelinesRepository.PriceList[variable];
            getsalelinestransactions.Discount = (Random.Shared.Next(1, 51));
            getsalelinestransactions.Discount = getsalelinestransactions.Discount / 100;
            getsalelinestransactions.Discount = Math.Round(getsalelinestransactions.Discount, 2);
            getsalelinestransactions.VATRate = (Random.Shared.Next(1, 21));
            getsalelinestransactions.VATRate = getsalelinestransactions.VATRate / 100;
            getsalelinestransactions.VATRate = Math.Round(getsalelinestransactions.VATRate, 2);
            getsalelinestransactions.VAT = (getsalelinestransactions.Price * getsalelinestransactions.Quantity);
            getsalelinestransactions.VAT = getsalelinestransactions.VAT - (getsalelinestransactions.VAT * getsalelinestransactions.Discount);
            getsalelinestransactions.VAT = getsalelinestransactions.VAT * getsalelinestransactions.VATRate;
            getsalelinestransactions.VAT = Math.Round(getsalelinestransactions.VAT, 2);
            SalelinesRepository.GetSalelinesTransactionsList.Add(getsalelinestransactions);
            context.GetSalelinesTransactions.Add(getsalelinestransactions);
            context.SaveChanges();
            //logger.LogInformation(getsalestransactions.ToString());
            return getsalelinestransactions;
        }

        public List<GetSalelinesTransactions> Listsalelines()
        {
            var getsalelinestransactionss = SalelinesRepository.GetSalelinesTransactionsList;
            return getsalelinestransactionss;
        }
    }
}
