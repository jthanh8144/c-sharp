using System;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
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

