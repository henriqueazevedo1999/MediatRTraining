using MediatR;
using Project.Application.Notifications;

namespace Project.Application.EventHandlers;

public class LogEventHandler : INotificationHandler<PersonCreatedNotification>,
                                INotificationHandler<PersonChangedNotification>,
                                INotificationHandler<PersonDeletedNotification>,
                                INotificationHandler<ErrorNotification>
{
    public Task Handle(PersonCreatedNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo}'");
        });
    }

    public Task Handle(PersonChangedNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo} - {notification.IsEfetivado}'");
        });
    }

    public Task Handle(PersonDeletedNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"EXCLUSAO: '{notification.Id} - {notification.IsEfetivado}'");
        });
    }

    public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"ERRO: '{notification.ExceptionMessage} \n {notification.StackTrace}'");
        });
    }
}
