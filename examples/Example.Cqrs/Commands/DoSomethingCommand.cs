using Lumini.Core.Cqrs.Commands;

namespace Example.Cqrs.Commands;

internal class DoSomethingCommand : ICommand
{
}

internal class DoSomethingCommandHandler : ICommandHandler<DoSomethingCommand>
{
    public async Task Handle(DoSomethingCommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Command : {command.GetType()} | Handler: {this.GetType()}");
        Console.WriteLine("From Handler -> Doing something...");
    }
}

// TODO: Implement this exception and test it in the examples,
// to ensure that the behavior is as expected when multiple handlers are registered for the same command.

// CQRS Sender will throw an exception if multiple handlers are registered for the same command,
// so this handler will not be registered and will not be executed when the command is sent.

// Will not be registered, just for testing the behavior when multiple handlers are registered for the same command
//internal class DoSomethingCommandHandler2 : ICommandHandler<DoSomethingCommand>
//{
//    public async Task Handle(DoSomethingCommand command, CancellationToken cancellationToken)
//    {
//        Console.WriteLine($"Command : {command.GetType()} | Handler: {this.GetType()}");
//        Console.WriteLine("From Handler -> Doing something2...");
//    }
//}