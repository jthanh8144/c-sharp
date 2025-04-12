using System;
using BulkyBook.Models;
using BulkyBook.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class ApplicationUserRepository: Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext db;

		public ApplicationUserRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        //public void Update(ApplicationUser applicationUser)
        //{
        //    db.ApplocationUsers.Update(applicationUser);
        //}
    }
}

