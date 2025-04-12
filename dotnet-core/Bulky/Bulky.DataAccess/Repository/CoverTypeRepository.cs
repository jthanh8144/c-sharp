using System;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
{
	public class CoverTypeRepository: Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext db;

		public CoverTypeRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(CoverType coverType)
        {
            db.CoverTypes.Update(coverType);
        }
    }
}

