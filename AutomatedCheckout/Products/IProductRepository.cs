namespace AutomatedCheckout.Products
{
    internal interface IProductRepository
    {
        Product Get(int id);
    }
}