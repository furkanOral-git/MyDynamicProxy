public class DbUserManager : IDbUserService
{
    public void AddUserToDataBase(User user)
    {
        System.Console.WriteLine($"DbUserManager : '{user.Name} {user.Surname}' has added to database");
    }
}