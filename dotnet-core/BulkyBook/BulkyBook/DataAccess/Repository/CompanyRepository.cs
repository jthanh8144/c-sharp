using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext db;

		public CompanyRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(Company company)
        {
            db.Companies.Update(company);
        }
    }
}

