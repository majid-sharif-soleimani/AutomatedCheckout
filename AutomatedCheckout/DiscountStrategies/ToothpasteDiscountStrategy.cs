using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedCheckout.Checkout;

namespace AutomatedCheckout.DiscountStrategies
{
    internal class ToothpasteDiscountStrategy : IDiscountStrategy
    {
        public decimal GetDiscount(Cart cart)
        {
            decimal discount = 0;
            if (cart.TryGetValue(1, out var lineItem))
            {
                var toothpasteCount = (int)lineItem.Amount / 3;                
                discount = toothpasteCount * lineItem.Product.Price;
            }
            return discount;
        }
    }
}
