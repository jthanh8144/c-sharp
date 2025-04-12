using System;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.IRepository
{
	public interface IShoppingCartRepository: IRepository<ShoppingCart>
	{
		//void Update(ShoppingCart shoppingCart);
		int IncrementCount(ShoppingCart shoppingCart, int count);
		int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
