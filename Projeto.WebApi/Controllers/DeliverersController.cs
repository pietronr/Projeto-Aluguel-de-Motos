using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto.WebApi.Controllers;

/// <summary>
/// Classe controller para entregadores, orquestra as operações HTTP
/// </summary>
/// <param name="service">Serviço.</param>
[SwaggerTag("entregadores")]
[Route("entregadores")]
[ApiController]
public class DeliverersController(IDelivererService service) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Cadastrar entregador")]
    [SwaggerResponse(StatusCodes.Status201Created, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Post([FromBody] DelivererRequest request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPost("{id}/cnh")]
    [SwaggerOperation("Enviar foto da CNH")]
    [SwaggerResponse(StatusCodes.Status201Created, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> PostImage(string id, [FromBody] UpdateDelivererImageRequest request)
    {
        var result = await service.UpdateLicenceImageAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }
}
