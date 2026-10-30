using BeachAula4.Dtos;
using BeachAula4.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeachAula4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuadrasController : ControllerBase
{
    private readonly IQuadraService _quadraService;

    public QuadrasController(IQuadraService quadraService)
    {
        _quadraService = quadraService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuadraDto>>> Get() =>
        Ok(await _quadraService.ObterTodasAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<QuadraDto>> GetById(int id)
    {
        var quadra = await _quadraService.ObterPorIdAsync(id);
        if (quadra == null) return NotFound();
        return Ok(quadra);
    }

    // Consulta avançada LINQ via Query String (ex: /api/quadras/filtrar?coberta=true&valorMaximo=150)
    [HttpGet("filtrar")]
    public async Task<ActionResult<IEnumerable<QuadraDto>>> GetFiltered(
        [FromQuery] bool? coberta, 
        [FromQuery] decimal valorMaximo = 500) =>
        Ok(await _quadraService.ObterFiltradasAsync(coberta, valorMaximo));

    [HttpPost]
    public async Task<ActionResult<QuadraDto>> Post([FromBody] CriarQuadraDto dto)
    {
        var novaQuadra = await _quadraService.AdicionarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = novaQuadra.Id }, novaQuadra);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CriarQuadraDto dto)
    {
        var sucesso = await _quadraService.AtualizarAsync(id, dto);
        if (!sucesso) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sucesso = await _quadraService.DeletarAsync(id);
        if (!sucesso) return NotFound();
        return NoContent();
    }
}