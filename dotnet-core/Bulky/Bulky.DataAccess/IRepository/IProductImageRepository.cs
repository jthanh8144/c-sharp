using System;
using Bulky.Models;

namespace Bulky.DataAccess.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage productImage);
    }
}
