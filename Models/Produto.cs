namespace APICatalogo.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
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
