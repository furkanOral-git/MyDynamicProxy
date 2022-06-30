using IoContainer;
using Console;


var services = ServiceCollection.InitServices();
var container = services.InitContainer();

services.RegisterAsSingleton<IProductDb,ProductDb>();
services.RegisterAsSingleton<IProductService,ProductManager>();

var ProductManager = container.GetServiceAsSingleton<IProductService>();

Product product = new Product("Mavi Kalem");
ProductManager.AddProduct(product);

