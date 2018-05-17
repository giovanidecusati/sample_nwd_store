using System;
using System.Threading.Tasks;

namespace BuildingBlock.Core.Transactions
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        void Rollback();
    }
}
