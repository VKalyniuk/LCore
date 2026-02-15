using Lumini.Core.Cqrs.Decorators;
using Lumini.Core.Cqrs.Notifications;
using Lumini.Core.Cqrs.Requests;
using Lumini.Core.Cqrs.Senders;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lumini.Core.Cqrs;

public static class CqrsServicesConfiguration
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddScoped<ISender, Sender>();

        return services;
    }

    public static IServiceCollection AddCqrs(this IServiceCollection services, Action<CqrsConfiguration> configuration)
    {
        services.AddCqrs();
        configuration(new CqrsConfiguration(services));

        return services;
    }

    public static IServiceCollection AddHandler<TRequest, THandler>(this IServiceCollection services)
        where TRequest : IRequest
        where THandler : class, IRequestHandler<TRequest>
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IRequestHandler<TRequest>, THandler>();

        return services;
    }

    public static IServiceCollection AddHandler<TResponse, TRequest, THandler>(this IServiceCollection services)
        where TRequest : IRequest<TResponse>
        where THandler : class, IRequestHandler<TRequest, TResponse>
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IRequestHandler<TRequest, TResponse>, THandler>();

        return services;
    }

    public static IServiceCollection AddDecorator(this IServiceCollection services, Type decoratorType)
    {
        ArgumentNullException.ThrowIfNull(services, "services");
        ArgumentNullException.ThrowIfNull(decoratorType, "decoratorType");

        services.AddScoped(typeof(IHandlerDecorator<,>), decoratorType);

        return services;
    }

    public static IServiceCollection AddCqrsForAssembly(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(assembly);

        services.AddCqrs();
        
        AddRequestHandlersForAssembly(services, assembly);
        AddNotificationHandlersForAssembly(services, assembly);

        return services;
    }

    private static IServiceCollection AddRequestHandlersForAssembly(IServiceCollection services, Assembly assembly)
    {
        var handlerGenerics = new[] { typeof(IRequestHandler<>), typeof(IRequestHandler<,>) };
        AddHandlersForAssembly(services, assembly, handlerGenerics);

        return services;
    }

    private static IServiceCollection AddNotificationHandlersForAssembly(IServiceCollection services, Assembly assembly)
    {
        var notitficationGenerics = new[] { typeof(INotificationHandler<>) };
        AddHandlersForAssembly(services, assembly, notitficationGenerics);

        return services;
    }

    private static IServiceCollection AddHandlersForAssembly(IServiceCollection services, Assembly assembly, Type[] generics)
    {
        var handlerTypes = GetTypes(assembly, generics);
        foreach (var handler in handlerTypes)
        {
            var iface = handler.Interface;
            var openGeneric = iface.GetGenericTypeDefinition();
            var args = iface.GetGenericArguments();
            var serviceType = openGeneric.MakeGenericType(args);

            services.AddScoped(serviceType, handler.Type);
        }

        return services;
    }

    private static IEnumerable<TypeInterfacePair> GetTypes(Assembly assembly, Type[] generics)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new TypeInterfacePair { Type = t, Interface = i })
            .Where(x => x.Interface.IsGenericType &&
                        generics.Contains(x.Interface.GetGenericTypeDefinition()));

        return types;
    }

    private class TypeInterfacePair
    {
        public Type Type { get; set; }
        public Type Interface { get; set; }
    }
}
