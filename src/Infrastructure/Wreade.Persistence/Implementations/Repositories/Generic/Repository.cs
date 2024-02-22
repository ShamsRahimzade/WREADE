using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories.Generic;
using Wreade.Domain.Entities.Common;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public IQueryable<T> GetAll(bool IgnoreQuery = false, bool IsTracking = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (IgnoreQuery) query = query.IgnoreQueryFilters();
            if (!IsTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool IsTracking = false, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (expression != null) query = query.Where(expression);

            query = _addIncludes(query, includes);

            return IsTracking ? query : query.AsNoTracking();
        }
        public async Task<T> GetByIdAsync(int id, bool IgnoreQuery = false, bool IsTracking = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x => x.Id == id);
            if (IgnoreQuery) query = query.IgnoreQueryFilters();
            if (!IsTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool IgnoreQuery = false, bool IsTracking = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            if (IgnoreQuery) query = query.IgnoreQueryFilters();
            if (!IsTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
       
        public IQueryable<T> GetPagination(int skip = 0, int take = 0, bool IgnoreQuery = true, Expression<Func<T, object>>? orderExpression = null, bool IsDescending = false, Expression<Func<T, bool>> expression=null, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
			if (expression != null) query = query.Where(expression);
			if (orderExpression != null)
			{
				if (IsDescending)
				{
					query = query.OrderByDescending(orderExpression);
				}
				else
				{
					query = query.OrderBy(orderExpression);
				}

			}
			query = _addIncludes(query, includes);
			if (IgnoreQuery)
            {
                query = query.IgnoreQueryFilters();
            }
            return query;
        }

        public IQueryable<T> GetOrderBy(Expression<Func<T, object>>? orderExpression = null, bool IsDescending = false)
        {
            IQueryable<T> query = _context.Set<T>();
            if (orderExpression != null)
            {
                if (IsDescending)
                {
                    query = query.OrderByDescending(orderExpression);
                }
                else
                {
                    query = query.OrderBy(orderExpression);
                }

            }
            return query;
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {

            _table.Remove(entity);

        }

        public void SoftDelete(T entity)
        {
            entity.Isdeleted = true;
            Update(entity);
        }

        public void ReverseDelete(T entity)
        {
            entity.Isdeleted = false;
            Update(entity);
        }

        

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
        private IQueryable<T> _addIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

		public IQueryable<T> GetPaginationB<T>(Expression<Func<T, bool>> filter = null, int skip = 0, int take = 10) where T : class
		{
			// Başlangıç sorgusu
			IQueryable<T> query = _context.Set<T>();

			// Filtreleme
			if (filter != null)
			{
				query = query.Where(filter);
			}

			// Sayfalama
			query = query.Skip(skip).Take(take);

			return query;
		}
	}
}
