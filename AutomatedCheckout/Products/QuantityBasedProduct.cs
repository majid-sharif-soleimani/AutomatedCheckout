namespace AutomatedCheckout.Products
{
    internal record QuantityBasedProduct : Product
    {
        public QuantityBasedProduct(string name, int id, decimal price)
            : base(name, id, price)
        {
        }
    }
}
