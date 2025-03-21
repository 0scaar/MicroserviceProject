using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;

namespace NSE.Core.Mediator;

public class MediatorHandler(IMediator _mediator) : IMediatorHandler
{
    public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        => await _mediator.Send(comando);

    public async Task PublicarEvento<T>(T evento) where T : Event
        => await _mediator.Publish(evento);
}