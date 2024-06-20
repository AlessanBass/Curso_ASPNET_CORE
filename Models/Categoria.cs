using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;
/* 
    Aplicando Data Notations
 */

[Table("Categorias")] //não era necessário utilizar
public class Categoria
{
    /* É uma boa prática sempre incializar a coleção */
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    /* Para chave primário ao usar EF usar nomes específicos */
    [Key]
    public int CategoriaId {get; set;}

    [Required]
    [StringLength(80)]
    public string?  Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; } /* Armazena o caminho da string */

    /* Para relacionar, precisamos incluir uma propriedade de navegação que indica
        que categoria pode ter uma coleção de produtos
     */

     [JsonIgnore]
     public ICollection<Produto>? Produtos {get; set;} // Isso já seria o suficente
}
