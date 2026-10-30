using BeachAula4.Entidades;

namespace BeachAula4.Interfaces;

public interface IQuadraRepository
{
    Task<IEnumerable<Quadra>> ObterTodasAsync();
    Task<Quadra?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Quadra quadra);
    Task AtualizarAsync(Quadra quadra);
    Task DeletarAsync(Quadra quadra);
    // Consulta avançada com LINQ
    Task<IEnumerable<Quadra>> ObterFiltradasAsync(bool? coberta, decimal valorMaximo);
}