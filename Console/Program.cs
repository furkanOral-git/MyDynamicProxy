using IoContainer;



var services = ServiceCollection.InitServices();
var container = services.InitContainer();

//i don't have to call this class. it's parameter of UserManager and it'll to be called and instansiate when UserManager class to be called
services.RegisterAsSingleton<IDbUserService, DbUserManager>();
//i'll use method interceptors that i created and so i have to use proxy service also. proxyUsage: True
services.RegisterAsSingleton<IUserService, UserManager>(proxyUsage: true);


var manager = container.GetServiceAsSingleton<IUserService>();
User user = new User("Furkan", "Oral");
manager.AddUser(user);








