using Lumini.Core.Cqrs.Notifications;

namespace Example.Cqrs.Notifications;

internal class AnotherNotification : INotification
{
    public string Message { get; set; }
}

internal class AnotherNotificationHandler : INotificationHandler<AnotherNotification>
{
    public Task Handle(AnotherNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notification Handler: {this.GetType()}");
        Console.WriteLine($"Received notification with message: {notification.Message}");
        Console.WriteLine($"Notification hash {notification.GetHashCode()}");
        return Task.CompletedTask;
    }
}
