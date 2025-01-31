using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedCheckout.Checkout;

namespace AutomatedCheckout.DiscountStrategies
{
    internal class AppleDiscountStrategy : IDiscountStrategy
    {
        public decimal GetDiscount(Cart cart)
        {
            decimal discount = 0;
            if (cart.TryGetValue(5, out var appleLineItem))
            {
                var otherLineItemsTotalPrice = cart.Where(a => a.Key != 5).Sum(a => a.Value.Amount * a.Value.Product.Price);
                if (otherLineItemsTotalPrice > 150)
                    discount = appleLineItem.Amount * 16;
            }
            return discount;
        }
    }
}
