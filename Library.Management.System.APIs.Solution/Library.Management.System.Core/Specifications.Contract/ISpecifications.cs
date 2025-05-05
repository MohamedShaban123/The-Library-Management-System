using Library.Management.System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Management.System.Core.Specifications.Contract
{
  public  interface ISpecifications<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity,bool>> Creteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity,object>> OrderByAsc { get; set; }
        public Expression<Func<TEntity,object>> OrderByDesc { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }


    }
}
