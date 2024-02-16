using System.Linq.Expressions;

using Wreade.Domain.Entities.Common;

namespace Wreade.Application.Abstractions.Repostories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool IgnoreQuery = false, bool IsTracking = false, params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool IsTracking = false, params string[] includes);
        IQueryable<T> GetPagination(int skip = 0, int take = 0, bool IgnoreQuery = false, Expression<Func<T, object>>? orderExpression = null, bool IsDescending = false, params string[] includes);
        IQueryable<T> GetOrderBy(Expression<Func<T, object>>? orderExpression = null, bool IsDescending = false);
        Task<T> GetByIdAsync(int id, bool IgnoreQuery = false, bool IsTracking = false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IgnoreQuery = false, bool IsTracking = false, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseDelete(T entity);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task SaveChangeAsync();
		IQueryable<T> GetPaginationB<T>(Expression<Func<T, bool>> filter = null, int skip = 0, int take = 10) where T : class;

	}
}
