using MediatR;
using Project.Application.Commands;
using Project.Application.Models;
using Project.Application.Notifications;
using Project.Repositories;

namespace Project.Application.Handlers;

public class CadastraPessoaCommandHandler : IRequestHandler<CadastraPessoaCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;

    public CadastraPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<string> Handle(CadastraPessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };

        try
        {
            await _repository.Add(pessoa);
            //Posso criar um construtor no PessoaCriadaNotification que recebe uma pessoa? Ou uma notification não deve conhecer uma model?
            await _mediator.Publish(new PersonCreatedNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo});
            return await Task.FromResult("Pessoa criada com sucesso!");
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new PersonCreatedNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
            await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
            return await Task.FromResult("Ocorreu um erro no momento da criação");
        }
    }
}
