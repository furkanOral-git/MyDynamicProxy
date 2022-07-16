using AutoProxy.Concrete;


namespace Console
{
    //Sample of virtual proxy class which will be define at compile time
    public class ProductManagerProxy : IProductService
    {
        private AutoProxyMethodHandler _methodHandler;
        
        public ProductManagerProxy(AutoProxyMethodHandler methodHandler)
        {
           _methodHandler = methodHandler;
        }

        public void AddProduct(Product product)
        {
            _methodHandler.Handle(new object[] {product});
        }
    }
}