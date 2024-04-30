using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.DataAccess.Interfaces
{
    public interface IStoredProcedureRepository<T> where T : class,new()
    
    {
        Task<List<T>> GetWorkDataUsingStoredProcedure(string spname, List<KeyValuePair<string, object>> paramz);

    }
}
