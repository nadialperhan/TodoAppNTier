using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Interfaces;

namespace Udemy.TodoAppNTier.DataAccess.UnitofWork.StoredProc
{
    public interface IUowStored
    {
        IStoredProcedureRepository<T> GetStoredProcedureValues<T>() where T : class, new();

    }
}
