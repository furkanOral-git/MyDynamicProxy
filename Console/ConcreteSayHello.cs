using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoProxy.Aspects;
using AutoProxy.Concrete;


public class ConcreteSayHelloAttribute : MethodInterceptor
{
    private void SayHello(User user)
    {
        System.Console.WriteLine($"ConcreteSayHello : Helloooo {user.Name} {user.Surname}");
    }
    protected override void OnBefore(MethodInvocation method,object[]? args)
    {
        var user = SolveParameter<User>(args);
        SayHello(user);

    }
}
