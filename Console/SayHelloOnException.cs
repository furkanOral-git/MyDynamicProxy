using AutoProxy.Aspects;
using AutoProxy.Concrete;

public class SayHelloOnException : MethodInterceptor
{
    protected override void OnException(MethodInvocation method, Exception e)
    {
        System.Console.WriteLine($"SayHelloOnException : Hello :( exception {e.Message}");
    }
}