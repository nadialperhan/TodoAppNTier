using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Context;
using Udemy.TodoAppNTier.DataAccess.Extensions;
using Udemy.TodoAppNTier.DataAccess.Interfaces;

namespace Udemy.TodoAppNTier.DataAccess.Repositories
{
    public class StoredProcedureRepository<T>: IStoredProcedureRepository<T> where T:class,new()
    {
        private readonly TodoContext _todoContext;
      
        public StoredProcedureRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task<List<T>> GetWorkDataUsingStoredProcedure(string spname, List<KeyValuePair<string, object>> paramz) 
        {
            return await _todoContext.ExecuteStoredProcedure<T>(spname, paramz);

        }
    }
}
