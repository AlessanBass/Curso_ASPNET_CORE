using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Categoria
{
    /* É uma boa prática sempre incializar a coleção */
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    /* Para chave primário ao usar EF usar nomes específicos */
    public int CategoriaId {get; set;}
    public string?  Nome { get; set; }
    public string? ImageUrl { get; set; } /* Armazena o caminho da string */

    /* Para relacionar, precisamos incluir uma propriedade de navegação que indica
        que categoria pode ter uma coleção de produtos
     */
     public ICollection<Produto>? Produtos {get; set;} // Isso já seria o suficente
}
