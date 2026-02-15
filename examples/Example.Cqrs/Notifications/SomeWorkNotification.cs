using Lumini.Core.Cqrs.Notifications;

namespace Example.Cqrs.Notifications;

internal class SomeWorkNotification : INotification
{
    public string Message { get; set; }
}

internal class SomeWorkNotificationHandler1 : INotificationHandler<SomeWorkNotification>
{
    public Task Handle(SomeWorkNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notification Handler: {this.GetType()}");
        Console.WriteLine($"Received notification with message: {notification.Message}");
        Console.WriteLine($"Notification hash {notification.GetHashCode()}");
        return Task.CompletedTask;
    }
}

internal class SomeWorkNotificationHandler2 : INotificationHandler<SomeWorkNotification>
{
    public Task Handle(SomeWorkNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notification Handler: {this.GetType()}");
        Console.WriteLine($"Received notification with message: {notification.Message}");
        Console.WriteLine($"Notification hash {notification.GetHashCode()}");
        return Task.CompletedTask;
    }
}
