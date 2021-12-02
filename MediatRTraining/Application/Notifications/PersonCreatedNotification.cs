using MediatR;

namespace Project.Application.Notifications;

public class PersonCreatedNotification : INotification
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public char Sexo { get; set; }
}
