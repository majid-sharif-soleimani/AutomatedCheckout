using AutomatedCheckout.DiscountStrategies;
using AutomatedCheckout.Products;

namespace AutomatedCheckout.Checkout
{
    internal class Checkout
    {
        private readonly IProductRepository productRepository;
        private readonly IDiscountStrategy[] discountStrategies;
        private readonly Cart cart = new Cart();

        public Checkout(IProductRepository productRepository, IDiscountStrategyRepository discountStrategyRepository)
        {
            this.productRepository = productRepository;
            this.discountStrategies = discountStrategyRepository.GetStrategies();
        }

        public void AddItem(int itemId)
        {
            if (cart.TryGetValue(itemId, out var lineItem))
                lineItem.Amount += 1;
            else
            {
                var product = productRepository.Get(itemId);
                if (product is not QuantityBasedProduct)
                    throw new ArgumentException("Quantity-based product is expected!");
                lineItem = new LineItem(product, 1);
                cart.Add(itemId, lineItem);
            }
        }

        public void AddItem(int itemId, decimal weight)
        {
            if (weight <= 0)
                throw new ArgumentOutOfRangeException("Weight cannot be zero or negative!");

            if (cart.TryGetValue(itemId, out var lineItem))
                lineItem.Amount += weight;
            else
            {
                var product = productRepository.Get(itemId);
                if (product is not WeightBasedProduct)
                    throw new ArgumentException("Weight-based product is expected!");
                lineItem = new LineItem(product, weight);
                cart.Add(itemId, lineItem);
            }
        }

        public decimal Sum()
        {
            var totalDiscount = discountStrategies.Sum(a => a.GetDiscount(cart));
            var totalPrice = cart.Sum(a => a.Value.Amount * a.Value.Product.Price);
            return totalPrice - totalDiscount;
        }
    }
}
