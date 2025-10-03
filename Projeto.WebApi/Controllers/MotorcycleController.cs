using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;

namespace Projeto.WebApi.Controllers;

/// <summary>
/// Classe controller para motos, orquestra as operações HTTP
/// </summary>
/// <param name="service">Serviço.</param>
[Route("motos")]
[ApiController]
public class MotorcycleController(IMotorcycleService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? plateNumber = null)
    {
        var result = await service.GetAllAsync(plateNumber);

        return Ok(result.Response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await service.GetAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MotorcycleDto request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcyclePlateDto plateRequest)
    {
        var result = await service.UpdatePlateAsync(id, plateRequest);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await service.DeleteAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok();
    }
}
