using IoContainer;


var services = ServiceProvider.InitServices();
var container = services.InitContainer();


services.RegisterAsSingleton<IProductService,ProductManager>();

var ProductManager = container.GetServiceAsSingleton<IProductService>();

