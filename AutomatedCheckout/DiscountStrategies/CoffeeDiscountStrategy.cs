using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedCheckout.Checkout;

namespace AutomatedCheckout.DiscountStrategies
{
    internal class CoffeeDiscountStrategy : IDiscountStrategy
    {
        public decimal GetDiscount(Cart cart)
        {
            decimal discount = 0;
            if (cart.TryGetValue(4, out var lineItem))
            {
                var coffeePairsCount = (int)lineItem.Amount / 2;
                discount = coffeePairsCount * 4.98m;
            }
            return discount;
        }
    }
}
