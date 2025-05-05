using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Specifications.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Specifications
{
   public static  class BulidQuery<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Creteria is not null)
               query = query.Where(spec.Creteria);
            if (spec.OrderByAsc is not null)
                query = query.OrderBy(spec.OrderByAsc);
            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            if(spec.Includes.Any())
            query =spec.Includes.Aggregate(query, (currentQuery,includeExpression) => currentQuery.Include(includeExpression));
            if (spec.Take > 0)
                query = query.Skip(spec.Skip).Take(spec.Take);
            return query;
        }
    }
}
