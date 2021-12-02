using MediatR;
using Project.Application.Commands;
using Project.Application.Models;
using Project.Application.Notifications;
using Project.Repositories;

namespace Project.Application.Handlers;

public class AlteraPessoaCommandHandler : IRequestHandler<AlteraPessoaCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;

    public AlteraPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<string> Handle(AlteraPessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = new Pessoa { Id = request.Id, Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo};

        try
        {
            await _repository.Edit(pessoa);
            await _mediator.Publish(new PersonChangedNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo, IsEfetivado = true});
            return await Task.FromResult("Pessoa alterada com sucesso!");
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new PersonChangedNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo, IsEfetivado = false });
            await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            return await Task.FromResult("Ocorreu um erro no momento da alteração.");
        }
    }
}
