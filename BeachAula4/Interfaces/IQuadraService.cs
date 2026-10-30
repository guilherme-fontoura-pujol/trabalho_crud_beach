using BeachAula4.Dtos;

namespace BeachAula4.Interfaces;

public interface IQuadraService
{
    Task<IEnumerable<QuadraDto>> ObterTodasAsync();
    Task<QuadraDto?> ObterPorIdAsync(int id);
    Task<QuadraDto> AdicionarAsync(CriarQuadraDto dto);
    Task<bool> AtualizarAsync(int id, CriarQuadraDto dto);
    Task<bool> DeletarAsync(int id);
    Task<IEnumerable<QuadraDto>> ObterFiltradasAsync(bool? coberta, decimal valorMaximo);
}