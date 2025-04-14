using Domain.Contracts;
using Domain.Models;
using presistences.Data;
using presistences.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositores;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositores=new ConcurrentDictionary<string, object>();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepository < TEntity, TKey >) _repositores.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_context));

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
