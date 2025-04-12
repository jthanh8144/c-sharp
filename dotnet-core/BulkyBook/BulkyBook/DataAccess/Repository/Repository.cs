using System;
using System.Linq.Expressions;
using BulkyBook.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T: class
	{
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext _db)
		{
            db = _db;
            //db.ShoppingCarts.AsNoTracking();
            //db.Products.Include(p => p.Category).Include(p => p.CoverType);
            dbSet = db.Set<T>();
		}

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var prop in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefaultNull(Expression<Func<T, bool>> filter, string? includeProps = null, bool tracked = true)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (includeProps != null)
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
 