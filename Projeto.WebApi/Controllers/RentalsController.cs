using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto.WebApi.Controllers;

/// <summary>
/// Classe controller para locações, orquestra as operações HTTP
/// </summary>
/// <param name="service">Serviço.</param>
[SwaggerTag("locacao")]
[Route("locacao")]
[ApiController]
public class RentalsController(IRentalService service) : ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation("Consultar locação por id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da locação", typeof(RentalResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Dados não encontrados", typeof(Result))]
    public async Task<IActionResult> Get(string id)
    {
        var result = await service.GetAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Response);
    }

    [HttpPost]
    [SwaggerOperation("Alugar uma moto")]
    [SwaggerResponse(StatusCodes.Status201Created, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Post([FromBody] RentalRequest request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPut("{id}/devolucao")]
    [SwaggerOperation("Informar data de devolução e calcular valor")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data de devolução informada com sucesso", typeof(Result))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateRentalDeliveryDateRequest deliveryDateRequest)
    {
        var result = await service.CloseRentalAsync(id, deliveryDateRequest);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result.Response);
    }
}
