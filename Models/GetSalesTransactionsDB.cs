using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Entrytask_Urban.Services;


namespace Entrytask_Urban.Models
{
    public class GetSalesTransactionsDB : DbContext
    {
        public GetSalesTransactionsDB(DbContextOptions<GetSalesTransactionsDB> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity < GetSalesTransactions>().HasKey(gst => gst.TransactionKey);
            builder.Entity<GetSalelinesTransactions>().HasKey(o => o.SaleLineId); //tunove
            
        }

        public DbSet<GetSalesTransactions> GetSalesTransactions { get; set; }
        public DbSet<GetSalelinesTransactions> GetSalelinesTransactions { get; set; } //tunove

    }

    
    
}