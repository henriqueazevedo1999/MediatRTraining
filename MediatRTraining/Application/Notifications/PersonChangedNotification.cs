using MediatR;

namespace Project.Application.Notifications;

public class PersonChangedNotification : INotification
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public char Sexo { get; set; }
    public bool IsEfetivado { get; set; }
}
