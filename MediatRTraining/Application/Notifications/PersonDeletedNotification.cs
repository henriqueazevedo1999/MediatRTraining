using MediatR;

namespace Project.Application.Notifications;

public class PersonDeletedNotification : INotification
{
    public int Id { get; set; }
    public bool IsEfetivado { get; set; }
}
