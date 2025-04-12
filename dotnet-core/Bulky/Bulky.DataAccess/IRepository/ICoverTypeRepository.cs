using System;
using Bulky.Models;

namespace Bulky.DataAccess.IRepository
{
	public interface ICoverTypeRepository : IRepository<CoverType>
	{
		void Update(CoverType coverType);
	}
}
