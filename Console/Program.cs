using IoContainer;
using AutoProxy;
using Console;


var services = ServiceCollection.InitServices();
var container = services.InitContainer();

services.RegisterAsSingleton<IProductDb, ProductDb>();
services.RegisterAsSingleton<IProductService, ProductManager>(proxyUsage: true);

var ProductManager = container.GetServiceAsSingleton<IProductService>();

Product product = new Product("Mavi Kalem");

ProductManager.AddProduct(product);





