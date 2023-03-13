using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext db;
        public EFStoreRepository(StoreDbContext db)
        {
            this.db = db;
        }
        public IQueryable<Product> Products => db.Products;

        public async Task CreateProduct(Product p)
        {
            await db.Products.AddAsync(p);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product p)
        {
            Product? existingProduct = await db.Products.FirstOrDefaultAsync(P => P.ProductId == p.ProductId);
            if (existingProduct != null)
            {
                db.Products.Remove(p);
                await db.SaveChangesAsync();
            }
        }


        public async Task SaveProduct(Product p)
        {
            await db.SaveChangesAsync();
        }
    }
}
