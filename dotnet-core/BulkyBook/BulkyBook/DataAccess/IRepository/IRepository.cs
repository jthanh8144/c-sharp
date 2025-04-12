using System;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.IRepository
{
	public interface IRepository<T> where T : class
	{
		T GetFirstOrDefaultNull(Expression<Func<T, bool>> filter, string? includeProps = null, bool tracked = true);

		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null);

		void Add(T entity);

		void Remove(T entity);

		void RemoveRange(IEnumerable<T> entities);
    }
}
