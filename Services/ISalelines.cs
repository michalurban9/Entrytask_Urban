using Entrytask_Urban.Models;

namespace Entrytask_Urban.Services
{
    public interface ISalelines
    {
        public GetSalelinesTransactions Createsalelines(GetSalelinesTransactions getsalelinestransactions);
        // public GetSalelinesTransactions Get(int salineid);
        public List<GetSalelinesTransactions> Listsalelines();
    }
}
