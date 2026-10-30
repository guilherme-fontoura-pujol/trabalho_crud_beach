namespace BeachAula4.Dtos;

public class QuadraDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ValorHora { get; set; }
    public bool Ativa { get; set; }
    public string? Descricao { get; set; }
}