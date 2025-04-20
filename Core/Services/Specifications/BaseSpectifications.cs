using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpectifications<TEntity, TKey> : ISpectification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criterial { get ; set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get ; set ; }
        public int skip { get ; set; }
        public int take { get ; set; }
        public bool Ispagration { get; set; }

        public BaseSpectifications(Expression<Func<TEntity, bool>> expression)
        {
            Criterial = expression;
        }
        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpression.Add(expression);
        }
        protected void AddOrderBy(Expression<Func<TEntity,object>>expression)
        {
            OrderBy=expression;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }
        protected void ApplyPgination(int pageindex,int pagesize)
        {
            Ispagration = true;
            take = pagesize;
            skip=(pageindex-1)*pagesize;
        }
    }

}
