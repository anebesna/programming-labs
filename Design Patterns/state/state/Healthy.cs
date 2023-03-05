using System;
namespace state
{
    public class Healthy : State
    {
        public void Consume(Product product)
        {
            if (product.daysToExpire > 0)
            {
                Console.WriteLine($"{product.name} is safe to consume. It expires in {product.daysToExpire} day(s)");
                product.daysToExpire--;
            }
            else
            {
                product.state = new Harmful();
            }
        }
    }
}
