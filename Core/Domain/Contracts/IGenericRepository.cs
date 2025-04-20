using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey>where TEntity:BaseEntity<TKey>
    {
        Task<int> CountAsync(ISpectification<TEntity, TKey> spec);
        Task<IEnumerable<TEntity>> GetAllAsunc(bool trackchanges= false);
        Task <TEntity?>GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsunc(ISpectification<TEntity,TKey>spec,bool trackchanges=false);
        Task<TEntity?> GetAsync(ISpectification<TEntity,TKey>spec);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
