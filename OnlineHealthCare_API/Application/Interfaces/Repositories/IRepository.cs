using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
   public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }
        IQueryable<TEntity> GetQueryable();

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);


        TEntity Update(TEntity entity);
        void UpdateRange(List<TEntity> entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(List<TEntity> entity);


        bool Any();
        bool Any(Expression<Func<TEntity, bool>> where);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);


        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, string sortDirection = "");
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderBy, string sortDirection = "");
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderBy, string sortDirection = "");


        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
