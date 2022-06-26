using IoContainer;
using Console;


var services = ServiceCollection.InitServices();
var container = services.InitContainer();


services.RegisterAsSingleton<IProductService,ProductManager>();

var ProductManager = container.GetServiceAsSingleton<IProductService>();

