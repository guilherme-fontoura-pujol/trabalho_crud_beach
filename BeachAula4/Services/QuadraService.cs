using BeachAula4.Dtos;
using BeachAula4.Entidades;
using BeachAula4.Interfaces;

namespace BeachAula4.Services;

public class QuadraService : IQuadraService
{
    private readonly IQuadraRepository _quadraRepository;

    public QuadraService(IQuadraRepository quadraRepository)
    {
        _quadraRepository = quadraRepository;
    }

    public async Task<IEnumerable<QuadraDto>> ObterTodasAsync()
    {
        var quadras = await _quadraRepository.ObterTodasAsync();
        return quadras.Select(q => new QuadraDto { Id = q.Id, Nome = q.Nome, Coberta = q.Coberta, ValorHora = q.ValorHora });
    }

    public async Task<QuadraDto?> ObterPorIdAsync(int id)
    {
        var q = await _quadraRepository.ObterPorIdAsync(id);
        if (q == null) return null;
        return new QuadraDto { Id = q.Id, Nome = q.Nome, Coberta = q.Coberta, ValorHora = q.ValorHora };
    }

    public async Task<QuadraDto> AdicionarAsync(CriarQuadraDto dto)
    {
        var quadra = new Quadra { Nome = dto.Nome, Coberta = dto.Coberta, ValorHora = dto.ValorHora };
        await _quadraRepository.AdicionarAsync(quadra);
        return new QuadraDto { Id = quadra.Id, Nome = quadra.Nome, Coberta = quadra.Coberta, ValorHora = quadra.ValorHora };
    }

    public async Task<bool> AtualizarAsync(int id, CriarQuadraDto dto)
    {
        var quadra = await _quadraRepository.ObterPorIdAsync(id);
        if (quadra == null) return false;

        quadra.Nome = dto.Nome;
        quadra.Coberta = dto.Coberta;
        quadra.ValorHora = dto.ValorHora;

        await _quadraRepository.AtualizarAsync(quadra);
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var quadra = await _quadraRepository.ObterPorIdAsync(id);
        if (quadra == null) return false;

        await _quadraRepository.DeletarAsync(quadra);
        return true;
    }

    public async Task<IEnumerable<QuadraDto>> ObterFiltradasAsync(bool? coberta, decimal valorMaximo)
    {
        var quadras = await _quadraRepository.ObterFiltradasAsync(coberta, valorMaximo);
        return quadras.Select(q => new QuadraDto { Id = q.Id, Nome = q.Nome, Coberta = q.Coberta, ValorHora = q.ValorHora });
    }
}