using Infra.Persistence.EF;
using Infra.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContexto _context;

        public UnitOfWork(EFContexto context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChangesAsync();
        }
    }
}
