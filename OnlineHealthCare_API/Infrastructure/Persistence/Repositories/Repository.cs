using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual IQueryable<TEntity> Queryable
        {
            get
            {
                return GetQueryable();
            }
        }
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        #region Add

        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        #endregion


        #region update
        public TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity;
        }

        public void UpdateRange(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
        }
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                // dbSet.Attach(entity); // I have **await** here 
                _dbContext.Entry(entity).State = EntityState.Modified;
            });

            //  await Task.FromResult(_dbContext.Set<TEntity>().Update(entity));
        }

        public async Task UpdateRangeAsync(List<TEntity> entity)
        {
            await Task.Run(() =>
            {
                foreach (var item in entity)
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
                // dbSet.Attach(entity); // I have **await** here 
                //_dbContext.Entry(entity).State = EntityState.Modified;
            });

        }

        #endregion


        #region Any
        public bool Any()
        {
            return _dbContext.Set<TEntity>().Any();
        }


        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            return _dbContext.Set<TEntity>().Any(where);
        }

        public Task<bool> AnyAsync()
        {
            return _dbContext.Set<TEntity>().AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return _dbContext.Set<TEntity>().AnyAsync(where);
        }

        #endregion

        #region FirstOrDefault
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, string sortDirection = "")
        {
            switch (sortDirection.ToLower())
            {
                case "desc":
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderByDescending(orderBy).FirstOrDefaultAsync();
                default:
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderBy(orderBy).FirstOrDefaultAsync();
            }
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderBy, string sortDirection = "")
        {
            switch (sortDirection.ToLower())
            {
                case "desc":
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderByDescending(orderBy).FirstOrDefaultAsync();
                default:
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderBy(orderBy).FirstOrDefaultAsync();
            }
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderBy, string sortDirection = "")
        {
            switch (sortDirection.ToLower())
            {
                case "desc":
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderByDescending(orderBy).FirstOrDefaultAsync();
                default:
                    return await _dbContext.Set<TEntity>().Where(predicate).OrderBy(orderBy).FirstOrDefaultAsync();
            }
        }

        #endregion


        #region Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
          => await Task.FromResult(_dbContext.Remove(entity));


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        #endregion
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
