using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public float  Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    /* Precisamos relacionar, dizendo que essa tabela vai ter a chave estrangeira
        de uma categoria.
        E precisamos de uma propriedade de navegação para indica que Produto se relaciona
         com uma categoria.
     */
     public int CategoriaId {get; set;} // Mapeia a chave estrangeira
     public Categoria? Categoria {get; set;} // Propeirdade de navegação que indica relacionamento
}
