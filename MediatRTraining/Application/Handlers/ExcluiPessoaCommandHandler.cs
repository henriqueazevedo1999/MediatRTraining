using MediatR;
using Project.Application.Commands;
using Project.Application.Models;
using Project.Application.Notifications;
using Project.Repositories;

namespace Project.Application.Handlers;

public class ExcluiPessoaCommandHandler : IRequestHandler<ExcluiPessoaCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;

    public ExcluiPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<string> Handle(ExcluiPessoaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool sucesso = await _repository.Delete(request.Id);
            if (!sucesso)
            {
                await _mediator.Publish(new PersonDeletedNotification { Id = request.Id, IsEfetivado = false });
                return await Task.FromResult("Não foi possível excluir a pessoa");
            }

            await _mediator.Publish(new PersonDeletedNotification { Id = request.Id, IsEfetivado = true});
            return await Task.FromResult("Pessoa excluída com sucesso!");
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new PersonDeletedNotification { Id = request.Id, IsEfetivado = false });
            await _mediator.Publish(new ErrorNotification { ExceptionMessage = ex.Message, StackTrace = ex.StackTrace});
            return await Task.FromResult("Ocorreu um erro no momento da exclusão");
        }
    }
}
