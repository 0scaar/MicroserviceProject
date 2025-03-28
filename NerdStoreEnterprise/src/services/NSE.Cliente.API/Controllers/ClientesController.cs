using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Cliente.API.Controllers;

public class ClientesController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public ClientesController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> Index()
    {
        var resultado = await _mediatorHandler.EnviarComando(
            new RegistrarClienteCommand(Guid.NewGuid(), "Oscar", "oscar@edu.com", "30314299076"));

        return CustomResponse(resultado);
    }
}