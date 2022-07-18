public class UserManager : IUserService
{
    private IDbUserService _dbService;

    public UserManager(IDbUserService dbService)
    {
        _dbService = dbService;
    }
    
    [ConcreteSayHello]
    [SayHelloOnException]
    [SayByeOnAfter]
    [SayBravoOnSuccess]
    public void AddUser(User user)
    {
        _dbService.AddUserToDataBase(user);
        System.Console.WriteLine($"UserManager : i'm doing my job for {user.Name} {user.Surname} ");
    }
}