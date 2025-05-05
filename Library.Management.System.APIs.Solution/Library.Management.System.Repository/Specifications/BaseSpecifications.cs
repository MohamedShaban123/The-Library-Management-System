using Library.Management.System.Core.Entities;
using Library.Management.System.Core.Specifications.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Repository.Specifications
{
 public   class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Creteria { get ; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderByAsc { get; set ; }
        public Expression<Func<TEntity, object>> OrderByDesc { get ; set; }
        public int Take { get ; set; }
        public int Skip { get ; set ; }

        public BaseSpecifications()
        {
            // Creteria = null
            // Includes = new List<Expression<Func<TEntity, object>>>();
        }
        public BaseSpecifications(Expression<Func<TEntity, bool>> creteriaExpression)
        {
            Creteria = creteriaExpression;
        }

        public void ApplyOrderAsc(Expression<Func<TEntity, object>> OrderByAscExpression)
        {
            OrderByAsc = OrderByAscExpression;
        }
        public void ApplyOrderDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
        public void ApplyPagination(int take,int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
