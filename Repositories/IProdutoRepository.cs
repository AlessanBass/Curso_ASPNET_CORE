using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProdutoRepository: IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id);

}
