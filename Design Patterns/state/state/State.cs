using System;
namespace state
{
    public interface State
    {
        public void Consume(Product product);
    }
}
