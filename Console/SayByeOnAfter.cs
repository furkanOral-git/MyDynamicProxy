using AutoProxy.Aspects;
using AutoProxy.Concrete;

public class SayByeOnAfter : MethodInterceptor
{
    protected override void OnAfter(MethodInvocation method, object[]? args)
    {
        var user = SolveParameter<User>(args);
        System.Console.WriteLine($"SayByeOnAfter : Bye Byeee user {user.Name} {user.Surname}");
    }
}