using AutomatedCheckout.Products;

namespace AutomatedCheckout.Checkout
{
    internal class Cart : Dictionary<int, LineItem>
    {
    }

    internal record LineItem
    {
        public Product Product { get; set; }

        public decimal Amount { get; set; }

        public LineItem(Product product, decimal amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}
