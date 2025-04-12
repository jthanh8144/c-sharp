using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class CoverTypeRepository: Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext db;

		public CoverTypeRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        //public void Save()
        //{
        //    db.SaveChanges();
        //}

        public void Update(CoverType coverType)
        {
            db.CoverTypes.Update(coverType);
        }
    }
}

