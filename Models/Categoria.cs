namespace APICatalogo.Models;

public class Categoria
{
    /* Para chave primário ao usar EF usar nomes específicos */
    public int CategoriaId {get; set;}
    public string?  Nome { get; set; }
    public string? ImageUrl { get; set; } /* Armazena o caminho da string */
}
