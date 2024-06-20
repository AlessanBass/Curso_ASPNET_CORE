using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository
{   /* Na interface por natureza todos os metodos são publicos */
    /* Listar os métodos que deverão ser implementados */
    Task<IEnumerable<Categoria>> GetCategorias(); /* Para metodos e classe usar PascalCase */
    Task<Categoria> GetCategoria(int id);
    Task<Categoria> Create(Categoria categoria);
    Task<Categoria> Update(Categoria categoria);
    Task<Categoria> Delete(int id);

}
