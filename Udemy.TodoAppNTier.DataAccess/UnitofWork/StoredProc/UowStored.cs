using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Context;
using Udemy.TodoAppNTier.DataAccess.Interfaces;
using Udemy.TodoAppNTier.DataAccess.Repositories;

namespace Udemy.TodoAppNTier.DataAccess.UnitofWork.StoredProc
{
    public class UowStored: IUowStored
    {
        private readonly TodoContext _context;
        public UowStored(TodoContext context)
        {
            _context = context;
        }
        public IStoredProcedureRepository<T> GetStoredProcedureValues<T>() where T : class, new()
        {
            return new StoredProcedureRepository<T>(_context);
        }

    }
}
