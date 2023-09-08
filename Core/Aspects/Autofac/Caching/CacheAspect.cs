using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterception
{
    private int _duration;
    private IMemoryCache _cache;

    public CacheAspect(int duration = 60)
    {
        _duration = duration;
        _cache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
    }

    public override void Intercept(IInvocation invocation)
    {
        MethodInfo methodInfo = invocation.Method;

        // Cache key: method name + arguments
        var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
        var arguments = invocation.Arguments.ToList();
        var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

        if (_cache.TryGetValue(key,out var items))
        {
            invocation.ReturnValue = _cache.Get(key);
            return;
        }

        invocation.Proceed();
        _cache.Set(key, invocation.ReturnValue , TimeSpan.FromMinutes(_duration));
    }
}