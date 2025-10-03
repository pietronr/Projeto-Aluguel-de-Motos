using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Dtos;
using Projeto.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto.WebApi.Controllers;

[Route("api/1.0.0/motos")]
[ApiController]
public class MotorcycleController(IMotorcycleService service) : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de motos", typeof(MotorcycleDto))]
    public async Task<IActionResult> GetAll([FromQuery] string? plateNumber = null)
    {
        var result = await service.GetAllAsync(plateNumber);

        return Ok(result.Response);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Detalhes da motos", typeof(MotorcycleDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Moto não encontrada", typeof(Result))]
    public async Task<IActionResult> Get(string id)
    {
        var result = await service.GetAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Response);
    }

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Post([FromBody] MotorcycleDto request)
    {
        var result = await service.InsertAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Placa modificada com sucesso", typeof(Result))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcyclePlateDto plateRequest)
    {
        var result = await service.UpdateAsync(id, plateRequest);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos", typeof(Result))]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await service.DeleteAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok();
    }
}
