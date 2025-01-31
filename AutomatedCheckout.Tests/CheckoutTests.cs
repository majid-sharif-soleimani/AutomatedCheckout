using AutomatedCheckout.DiscountStrategies;
using AutomatedCheckout.Products;

namespace AutomatedCheckout.Tests
{
    public class CheckoutTests
    {
        [Test]
        public void AddItem_WithItemId_ThrowsNotImplementedException()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.That(() =>
            {
                try
                {
                    checkout.AddItem(1);
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }, Is.Null.Or.TypeOf<NotImplementedException>());
        }

        [Test]
        public void AddItem_WithItemIdAndWeight_EitherWorksOrThrowsNotImplementedException()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.That(() =>
            {
                try
                {
                    checkout.AddItem(2, 1);
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }, Is.Null.Or.TypeOf<NotImplementedException>());
        }

        [Test]
        public void Sum_EitherWorksOrThrowsNotImplementedException()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.That(() =>
            {
                try
                {
                    checkout.Sum();
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }, Is.Null.Or.TypeOf<NotImplementedException>());
        }

        [Test]
        public void AddItem_WithItemId_WorksFine()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.DoesNotThrow(() => checkout.AddItem(1));
        }

        [Test]
        public void AddItem_WithItemId_ThrowsAnError_WhenTheProductIsNotQuantityBased()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.Throws<ArgumentException>(() => checkout.AddItem(2));
        }

        [Test]
        public void AddItem_WithItemIdAndWeight_WorksFine()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.DoesNotThrow(() => checkout.AddItem(2, 1));
        }

        [Test]
        public void AddItem_WithItemIdAndWeight_ThrowsAnError_WhenTheProductIsNotQuantityBased()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.Throws<ArgumentException>(() => checkout.AddItem(1, 1));
        }

        [Test]
        public void AddItem_WithItemIdAndWeight_ThrowsAnError_WhenTheTheWeightIsZero()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.Throws<ArgumentOutOfRangeException>(() => checkout.AddItem(2, 0));
        }

        [Test]
        public void AddItem_WithItemIdAndWeight_ThrowsAnError_WhenTheTheWeightIsNegative()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.Throws<ArgumentOutOfRangeException>(() => checkout.AddItem(2, -1));
        }

        [Test]
        public void TheSumOfIsZero_WhenACartIsEmpty()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.That(() => checkout.Sum(), Is.EqualTo(0m));
        }

        /// <summary>
        /// When you purchase two packs of coffee, the regular price would be 22.49Kr × 2 = 44.98Kr. 
        /// However, with our special offer, you receive a discount of 4.98Kr, bringing the total cost for two packs of coffee down to 40.00Kr.
        /// </summary>
        [Test]
        public void TwoPacksOfCoffee_GetACorrectDiscount()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            checkout.AddItem(4);
            checkout.AddItem(4);
            Assert.That(() => checkout.Sum(), Is.EqualTo(40m));
            checkout.AddItem(4);
            checkout.AddItem(4);
            Assert.That(() => checkout.Sum(), Is.EqualTo(80m));
        }

        [Test]
        public void MakeSureThat_TheDiscountAppliesOnly_ToEveryTwoPacksOfCoffee()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            checkout.AddItem(4);
            Assert.That(() => checkout.Sum(), Is.EqualTo(22.49m));
            checkout.AddItem(4);
            checkout.AddItem(4);
            Assert.That(() => checkout.Sum(), Is.EqualTo(62.49m));
        }

        /// <summary>
        /// When you purchase three packs of toothpaste, the regular price would be 24.95Kr × 3 = 74.85Kr. 
        /// However, with our special offer, you only pay for two packs, saving 24.95Kr. 
        /// This brings the total cost for three packs of toothpaste down to 49.90Kr.
        /// </summary>
        [Test]
        public void ThreePacksOfToothpaste_CostsOnlyTwo()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            checkout.AddItem(1);
            checkout.AddItem(1);
            checkout.AddItem(1);
            Assert.That(() => checkout.Sum(), Is.EqualTo(49.9m));
            checkout.AddItem(1);
            checkout.AddItem(1);
            checkout.AddItem(1);
            Assert.That(() => checkout.Sum(), Is.EqualTo(99.8));
        }

        [Test]
        public void MakeSureThat_TheDiscountAppliesOnly_ToEveryThreePacksOfToothpaste()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            checkout.AddItem(1);
            checkout.AddItem(1);
            Assert.That(() => checkout.Sum(), Is.EqualTo(49.9m));
            checkout.AddItem(1);
            checkout.AddItem(1);
            Assert.That(() => checkout.Sum(), Is.EqualTo(74.85));
        }

        /// <summary>
        /// When your total purchase for other items exceeds 150Kr, you qualify for a special discount on apples. 
        /// Instead of the regular price of 32.95Kr/kg, you can purchase apples at a discounted rate of 16.95Kr/kg. 
        /// This offer allows you to enjoy significant savings on apples while shopping for other products.
        /// </summary>
        [Test]
        public void MakeSureThat_ShoppingOtherItemsForOver150kr_GetCorrectDiscountON_Apples()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            checkout.AddItem(1);
            checkout.AddItem(2, 1);
            checkout.AddItem(3);
            checkout.AddItem(4);
            checkout.AddItem(5, 1);
            checkout.AddItem(6);
            checkout.AddItem(7, 1);
            Assert.That(() => checkout.Sum(), Is.EqualTo(240.29m));
            checkout.AddItem(5, 0.5m);
            Assert.That(() => checkout.Sum(), Is.EqualTo(248.765m));
        }
    }
}