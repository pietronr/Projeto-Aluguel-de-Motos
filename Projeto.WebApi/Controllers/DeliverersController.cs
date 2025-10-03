using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.WebApi.Controllers;

/// <summary>
/// Classe controller para entregadores, orquestra as operações HTTP
/// </summary>
/// <param name="service">Serviço.</param>
[Route("entregadores")]
[ApiController]
public class DeliverersController(IDelivererService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DelivererRequest request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> PostImage(string id, [FromBody] UpdateDelivererImageRequest request)
    {
        var result = await service.UpdateLicenceImageAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }
}
