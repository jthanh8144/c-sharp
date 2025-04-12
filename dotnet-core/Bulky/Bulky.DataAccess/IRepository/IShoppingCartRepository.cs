﻿using System;
using Bulky.Models;

namespace Bulky.DataAccess.IRepository
{
	public interface IShoppingCartRepository: IRepository<ShoppingCart>
	{
		//void Update(ShoppingCart shoppingCart);
		int IncrementCount(ShoppingCart shoppingCart, int count);
		int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
