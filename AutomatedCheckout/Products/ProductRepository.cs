namespace AutomatedCheckout.Products
{
    internal class ProductRepository : IProductRepository
    {
        private readonly Dictionary<int, Product> products = new Dictionary<int, Product>();

        public ProductRepository()
        {
            Add(new QuantityBasedProduct("Toothpaste", 1, 24.95m));
            Add(new WeightBasedProduct("Cheese", 2, 59.00m));
            Add(new QuantityBasedProduct("Bread", 3, 11.95m));
            Add(new QuantityBasedProduct("Coffee", 4, 22.49m));
            Add(new WeightBasedProduct("Appel", 5, 32.95m));
            Add(new QuantityBasedProduct("Flour", 6, 11.95m));
            Add(new WeightBasedProduct("Ground Beef", 7, 93.00m));
            Add(new QuantityBasedProduct("Milk", 8, 9.32m));
        }

        private void Add(Product product) => products.Add(product.Id, product);
        
        public Product Get(int id) => products[id];
        
        public IReadOnlyDictionary<int, Product> GetProducts() => products.AsReadOnly();
    }
}
