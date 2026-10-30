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
        return quadras.Select(q => new QuadraDto 
        { 
            Id = q.Id, 
            Name = q.Name, 
            ValorHora = q.ValorHora, 
            Ativa = q.Ativa, 
            Descricao = q.Descricao 
        });
    }

    public async Task<QuadraDto?> ObterPorIdAsync(int id)
    {
        var q = await _quadraRepository.ObterPorIdAsync(id);
        if (q == null) return null;
        return new QuadraDto 
        { 
            Id = q.Id, 
            Name = q.Name, 
            ValorHora = q.ValorHora, 
            Ativa = q.Ativa, 
            Descricao = q.Descricao 
        };
    }

    public async Task<QuadraDto> AdicionarAsync(CriarQuadraDto dto)
    {
        var quadra = new Quadra 
        { 
            Name = dto.Name, 
            ValorHora = dto.ValorHora, 
            Ativa = dto.Ativa, 
            Descricao = dto.Descricao 
        };
        await _quadraRepository.AdicionarAsync(quadra);
        return new QuadraDto 
        { 
            Id = quadra.Id, 
            Name = quadra.Name, 
            ValorHora = quadra.ValorHora, 
            Ativa = quadra.Ativa, 
            Descricao = quadra.Descricao 
        };
    }

    public async Task<bool> AtualizarAsync(int id, CriarQuadraDto dto)
    {
        var quadra = await _quadraRepository.ObterPorIdAsync(id);
        if (quadra == null) return false;

        quadra.Name = dto.Name;
        quadra.ValorHora = dto.ValorHora;
        quadra.Ativa = dto.Ativa;
        quadra.Descricao = dto.Descricao;

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

    public async Task<IEnumerable<QuadraDto>> ObterFiltradasAsync(bool? ativa, decimal valorMaximo)
    {
        var quadras = await _quadraRepository.ObterFiltradasAsync(ativa, valorMaximo);
        return quadras.Select(q => new QuadraDto 
        { 
            Id = q.Id, 
            Name = q.Name, 
            ValorHora = q.ValorHora, 
            Ativa = q.Ativa, 
            Descricao = q.Descricao 
        });
    }
}