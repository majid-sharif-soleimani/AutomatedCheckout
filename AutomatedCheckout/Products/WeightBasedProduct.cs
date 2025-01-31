namespace AutomatedCheckout.Products
{
    internal record WeightBasedProduct : Product
    {
        public WeightBasedProduct(string name, int id, decimal price)
            : base(name, id, price)
        {
        }
    }
}
