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
        public void SumOfEmptyCartIsZero()
        {
            var checkout = new Checkout.Checkout(new ProductRepository(), new DiscountStrategyRepository());
            Assert.That(() => checkout.Sum(), Is.EqualTo(0m));
        }

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