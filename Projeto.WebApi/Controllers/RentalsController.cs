using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.WebApi.Controllers;

/// <summary>
/// Classe controller para locações, orquestra as operações HTTP
/// </summary>
/// <param name="service">Serviço.</param>
[Route("locacao")]
[ApiController]
public class RentalsController(IRentalService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await service.GetAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RentalRequest request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateRentalDeliveryDateRequest deliveryDateRequest)
    {
        var result = await service.CloseRentalAsync(id, deliveryDateRequest);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
