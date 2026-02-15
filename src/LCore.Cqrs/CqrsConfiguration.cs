using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lumini.Core.Cqrs;

public class CqrsConfiguration(IServiceCollection services)
{
    public CqrsConfiguration AddCqrsForAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        services.AddCqrsForAssembly(assembly);

        return this;
    }

    public CqrsConfiguration AddDecorator(Type decoratorType)
    {
        ArgumentNullException.ThrowIfNull(decoratorType);

        services.AddDecorator(decoratorType);

        return this;
    }
}