using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using presistences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private IQueryable<TEntity> ApplySpictifications(ISpectification<TEntity, TKey> spec)
        {
            return SpecifictaionsEvaluator.GetQuery(_dbContext.Set<TEntity>(),spec);
        }
        private readonly StoreDbContext _dbContext;
        public GenericRepository(StoreDbContext context) {
        _dbContext = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsunc(bool trackchanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackchanges ?
                     await _dbContext.Products.Include(p => p.ProductType).Include
                     (p => p.ProductBrand).ToListAsync() as IEnumerable<TEntity> :
                    await _dbContext.Products.Include(p => p.ProductType).Include
                    (p => p.ProductBrand).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return trackchanges ?
                await _dbContext.Set<TEntity>().ToListAsync() :
               await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsunc(ISpectification<TEntity, TKey> spec, bool trackchanges = false)
        {
            return await ApplySpictifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
            }
                return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity?> GetAsync(ISpectification<TEntity, TKey> spec)
        {
            return ApplySpictifications(spec).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public async Task<int> CountAsync(ISpectification<TEntity, TKey> spec)
        {
            return await ApplySpictifications(spec).CountAsync();
        }
    }
}
