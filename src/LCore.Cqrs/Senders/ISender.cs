using Lumini.Core.Cqrs.Notifications;
using Lumini.Core.Cqrs.Requests;

namespace Lumini.Core.Cqrs.Senders;

public interface ISender
{
    
    Task Send(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Notify(INotification notification, CancellationToken cancellationToken = default);
}
