using System;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
{
	public class ApplicationUserRepository: Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext db;

		public ApplicationUserRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public void Update(ApplicationUser applicationUser)
        {
            db.ApplocationUsers.Update(applicationUser);
        }
    }
}

