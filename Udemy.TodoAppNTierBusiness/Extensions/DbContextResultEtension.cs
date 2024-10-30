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
        public static async Task<List<TResult>> ExecuteStoredProcedure<TResult>(this DbContext dbContext,string spname, List<SqlParameter> paramz) where TResult:class,new()
        {
            using (var ctx=new GenericContext<TResult>(dbContext.Database.GetConnectionString()))
            {

                var paramNames = string.Join(", ", paramz.Select(param => param.ParameterName));

                spname = "sp_Deneme";
                var query = $"EXEC {spname} {paramNames}";

                // Sorguyu çalıştırıyoruz
                var nadi= ctx.Entities.FromSqlRaw<TResult>(query, paramz.ToArray()).AsNoTracking();
                return await nadi.ToListAsync();





                #region eski
                //var sqlParams = new SqlParameter[paramz.Count];
                //var prmString = "";
                //for (int i = 0; i < paramz.Count; i++)
                //{
                //    //var prm = new SqlParameter(paramz[i].Key, paramz[i].Key);
                //    var prm = new SqlParameter( paramz[i].ParameterName, paramz[i].ParameterName); // Anahtar ve değerler düzeltiliyor
                //    sqlParams[i] = prm;
                    //if (i>0)
                    //{
                    //    prmString += ",{" + i + "}";
                    //}
                    //else
                    //{
                    //    prmString += "{" + i + "}";
                    //}
                //    if (i > 0)
                //    {
                //        prmString += "," + paramz[i].ParameterName  ;
                //    }
                //    else
                //    {
                //        prmString +=   paramz[i].ParameterName ;
                //    }
                //}

                //var userType = ctx.Entities.FromSqlRaw<TResult>("dbo.sp_Deneme @Definition = {0}, @IsCompleted = {1}", "test", true);


                //var q = ctx.Entities.FromSqlRaw<TResult>("exec " + spname + prmString, sqlParams).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec " + spname ).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec Deneme @Definition,@IsCompleted", sqlParams).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("exec Deneme @Definition,@IsCompleted", "pari",true).AsNoTracking();
                //var q = ctx.Entities.FromSqlRaw<TResult>("SELECT * FROM Works WHERE Definition LIKE '%' + @Definition + '%' AND IsCompleted = @IsCompleted",
                //    new SqlParameter("@Definition", "pari"),
                //    new SqlParameter("@IsCompleted", true))
                //    .AsNoTracking();
               // return await q.ToListAsync();
                #endregion
            }
        }
    }
}
