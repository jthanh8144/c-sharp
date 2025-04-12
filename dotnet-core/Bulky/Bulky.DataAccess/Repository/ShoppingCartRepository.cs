using System;
using Bulky.Models;
using Bulky.DataAccess.IRepository;
using Bulky.DataAccess.Data;

namespace Bulky.DataAccess.Repository
{
	public class ShoppingCartRepository: Repository<ShoppingCart>, IShoppingCartRepository
	{
        private ApplicationDbContext db;

		public ShoppingCartRepository(ApplicationDbContext _db) : base(_db)
		{
            db = _db;
		}

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        //public void Update(ShoppingCart shoppingCart)
        //{
        //    db.ShoppingCarts.Update(shoppingCart);
        //}
    }
}

