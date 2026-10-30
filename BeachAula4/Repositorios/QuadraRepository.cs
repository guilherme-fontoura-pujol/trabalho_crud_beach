using BeachAula4.Data;
using BeachAula4.Entidades;
using BeachAula4.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeachAula4.Repositorios;

public class QuadraRepository : IQuadraRepository
{
    private readonly AppDbContext _context;

    public QuadraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Quadra>> ObterTodasAsync() =>
        await _context.Quadras.ToListAsync();

    public async Task<Quadra?> ObterPorIdAsync(int id) =>
        await _context.Quadras.FindAsync(id);

    public async Task AdicionarAsync(Quadra quadra)
    {
        await _context.Quadras.AddAsync(quadra);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Quadra quadra)
    {
        _context.Quadras.Update(quadra);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Quadra quadra)
    {
        _context.Quadras.Remove(quadra);
        await _context.SaveChangesAsync();
    }

    // Consulta avançada usando LINQ
    public async Task<IEnumerable<Quadra>> ObterFiltradasAsync(bool? coberta, decimal valorMaximo)
    {
        return await _context.Quadras
            .Where(q => (!coberta.HasValue || q.Coberta == coberta.Value) && q.ValorHora <= valorMaximo)
            .OrderBy(q => q.ValorHora)
            .ToListAsync();
    }
}