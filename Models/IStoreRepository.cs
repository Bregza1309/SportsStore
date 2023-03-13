namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        Task SaveProduct(Product p);
        Task CreateProduct(Product p);
        Task DeleteProduct(Product p);
    }
}
