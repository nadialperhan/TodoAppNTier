using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.DataAccess.Context;

namespace Udemy.TodoAppNTierBusiness.Extensions
{
    public static class DbContextResultEtension
    {
        public static async Task<List<TResult>> ExecuteStoredProcedure<TResult>(this DbContext dbContext,string spname,List<KeyValuePair<string,object>> paramz) where TResult:class,new()
        {
            using (var ctx=new GenericContext<TResult>(dbContext.Database.GetConnectionString()))
            {
                var sqlParams = new SqlParameter[paramz.Count];
                var prmString = "";
                for (int i = 0; i < paramz.Count; i++)
                {
                    //var prm = new SqlParameter(paramz[i].Key, paramz[i].Key);
                    var prm = new SqlParameter( paramz[i].Key, paramz[i].Value); // Anahtar ve değerler düzeltiliyor
                    sqlParams[i] = prm;
                    if (i>0)
                    {
                        prmString += ",{" + i + "}";
                    }
                    else
                    {
                        prmString += "{" + i + "}";
                    }
                }
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec " + spname + prmString, sqlParams).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec Deneme @Definition,@IsCompleted", sqlParams).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec Deneme @Definition,@IsCompleted", "pari",true).AsNoTracking();
                var q = ctx.Entities.FromSqlRaw<TResult>("SELECT * FROM Works WHERE Definition LIKE '%' + @Definition + '%' AND IsCompleted = @IsCompleted",
                    new SqlParameter("@Definition", "pari"),
                    new SqlParameter("@IsCompleted", true))
                    .AsNoTracking();
                return await q.ToListAsync();
            }
        }
    }
}
