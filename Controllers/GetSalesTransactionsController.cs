using Entrytask_Urban.Models;
using Entrytask_Urban.Services;
using Entrytask_Urban.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace Entrytask_Urban.Controllers
{

    public class GetSalesTransactionsController : ISales
    {
        private readonly GetSalesTransactionsDB context;
        private readonly ILogger<GetSalesTransactionsController> logger;
        public GetSalesTransactionsController(GetSalesTransactionsDB context, ILogger<GetSalesTransactionsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

       
        public GetSalesTransactions Create(GetSalesTransactions getsalestransactions)
        {
            
            //getsalestransactions.TransactionKey = SalesRepository.GetSalesTransactionsList.Count;
            getsalestransactions.TransactionDate = DateTime.Now;
            getsalestransactions.TicketNumber = SalesRepository.GetSalesTransactionsList.Count ;
            getsalestransactions.Guid = SalesRepository.GuidList[Random.Shared.Next(SalesRepository.GuidList.Length)];
            getsalestransactions.LocationName = SalesRepository.LocationNameList[Random.Shared.Next(SalesRepository.LocationNameList.Length)];
            getsalestransactions.DeviceName = SalesRepository.DeviceNameList[Random.Shared.Next(SalesRepository.DeviceNameList.Length)];
            getsalestransactions.CashierName = SalesRepository.CashierNameList[Random.Shared.Next(SalesRepository.CashierNameList.Length)];
            getsalestransactions.CustomerName = SalesRepository.CustomerNameList[Random.Shared.Next(SalesRepository.CustomerNameList.Length)];
            SalesRepository.GetSalesTransactionsList.Add(getsalestransactions);
            context.GetSalesTransactions.Add(getsalestransactions);
            context.SaveChanges();
            //logger.LogInformation(getsalestransactions.ToString());
            return getsalestransactions;
        }

        public GetSalesTransactions Get(int transactionkey)
        {
            
            var getsalestransactions = SalesRepository.GetSalesTransactionsList.FirstOrDefault(o => o.TransactionKey == transactionkey);
            if (getsalestransactions is null) return null;
            return getsalestransactions;
        }

        public List<GetSalesTransactions> List()
        {
            var getsalestransactionss = SalesRepository.GetSalesTransactionsList;
            return getsalestransactionss;
        }
        
    }
    
}
