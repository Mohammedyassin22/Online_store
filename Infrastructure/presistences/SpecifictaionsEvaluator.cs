using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences
{
    static class SpecifictaionsEvaluator
    {
       public static IQueryable<TEntity>GetQuery<TEntity,TKey>(IQueryable<TEntity> 
           inputquery,ISpectification<TEntity,TKey>spec)where TEntity : BaseEntity<TKey>
        {
            var query = inputquery;
            if(spec.Criterial is not null)
            {
                query=query.Where(spec.Criterial);
            }
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDesc is not null) 
            {
                query=query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.Ispagration)
                query = query.Skip(spec.skip).Take(spec.take);

            query = spec.IncludeExpression.Aggregate(query,
                (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));
            return query;
        }
    
}
}
