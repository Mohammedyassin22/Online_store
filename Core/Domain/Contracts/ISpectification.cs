using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpectification<TEntity,TKey>where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>> Criterial { get; set; }
        List<Expression<Func<TEntity, object>>> IncludeExpression { get; set; }
    }
}
