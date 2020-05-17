using System.Threading.Tasks;

namespace Infra.Transactions.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
