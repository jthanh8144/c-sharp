using System;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.IRepository
{
	public interface ICompanyRepository: IRepository<Company>
	{
		void Update(Company company);
	}
}
