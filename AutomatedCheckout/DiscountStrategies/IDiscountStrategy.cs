using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedCheckout.Checkout;

namespace AutomatedCheckout.DiscountStrategies
{
    internal interface IDiscountStrategy
    {
        decimal GetDiscount(Cart cart);
    }
}
