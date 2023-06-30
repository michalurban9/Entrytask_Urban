using Microsoft.AspNetCore.SignalR;
using Entrytask_Urban.Models;

namespace Entrytask_Urban.Services
{
    public interface ISales
    {
        public GetSalesTransactions Create(GetSalesTransactions getsalestransactions);
        public GetSalesTransactions Get(int transactionkey);
        public List<GetSalesTransactions> List();


    }
}
