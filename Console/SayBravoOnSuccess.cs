using AutoProxy.Aspects;
using AutoProxy.Concrete;

public class SayBravoOnSuccess : MethodInterceptor
{
    protected override void OnSucces(MethodInvocation method, object[]? args)
    {
        var user = SolveParameter<User>(args);
        System.Console.WriteLine($"SayBravoOnSucces : Congratulations !!! User {user.Name} {user.Surname}");
    }
}