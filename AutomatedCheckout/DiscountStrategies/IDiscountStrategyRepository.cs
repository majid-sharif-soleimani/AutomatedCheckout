namespace AutomatedCheckout.DiscountStrategies
{
    internal interface IDiscountStrategyRepository
    {
        IDiscountStrategy[] GetStrategies();
    }
}