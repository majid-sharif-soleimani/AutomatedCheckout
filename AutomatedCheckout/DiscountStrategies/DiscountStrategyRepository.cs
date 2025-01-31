using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedCheckout.DiscountStrategies
{
    internal class DiscountStrategyRepository : IDiscountStrategyRepository
    {
        private readonly List<IDiscountStrategy> discountStrategies = new();

        public DiscountStrategyRepository()
        {
            discountStrategies.Add(new CoffeeDiscountStrategy());
            discountStrategies.Add(new ToothpasteDiscountStrategy());
            discountStrategies.Add(new AppleDiscountStrategy());
        }

        public IDiscountStrategy[] GetStrategies() => discountStrategies.ToArray();
    }
}
