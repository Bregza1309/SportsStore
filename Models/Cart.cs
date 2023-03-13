namespace SportsStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new();
        public virtual void AddItem(Product product,int quantity)
        {
            CartLine? line = Lines.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if(line == null)
            {
                Lines.Add(new CartLine { Product = product,Qunatity = quantity});
            }
            else
            {
                line.Qunatity += quantity; 
            }
        }
        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(line => line.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue() =>
            Lines.Sum(line => line.Product.Price * line.Qunatity);

        public virtual void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; } = new();
        public int Qunatity { get; set; }
    }
}
