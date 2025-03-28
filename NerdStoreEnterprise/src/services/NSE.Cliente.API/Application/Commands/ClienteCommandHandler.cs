using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Models;
using NSE.Core.Messages;

namespace NSE.Cliente.API.Application.Commands;

public class ClienteCommandHandler : CommandHandler,
    IRequestHandler<RegistrarClienteCommand, ValidationResult>
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }
        
    public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
    {
        if (!message.EhValido()) return message.ValidationResult;

        var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);

        var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

        if (clienteExistente != null)
        {
            AdicionarErro("Este CPF já está em uso.");
            return ValidationResult;
        }

        _clienteRepository.Adicionar(cliente);

        cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

        return await PersistirDados(_clienteRepository.UnitOfWork);
    }
}