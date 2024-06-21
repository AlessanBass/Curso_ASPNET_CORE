namespace APICatalogo.Repositories;

public interface IUnitOfWork
{
    /* Devemos colocar todos os nossos respositórios */
    /* Se utilizarmos reposiório genéricos vamos perder a possibilidade de 
       usar métodos específicos
    */
    IProdutoRepository ProdutoRepository {get;}
    ICategoriaRepository CategoriaRepository {get;}
    Task SaveChangesAsync();

}
