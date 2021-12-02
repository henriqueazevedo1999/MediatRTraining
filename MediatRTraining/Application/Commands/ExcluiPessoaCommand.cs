using MediatR;

namespace Project.Application.Commands;

public class ExcluiPessoaCommand : IRequest<string>
{
    public int Id { get; set; }
}
