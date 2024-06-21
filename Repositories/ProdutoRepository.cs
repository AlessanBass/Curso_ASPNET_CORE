using System.Linq.Expressions;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

/* 
    Essa classe Herda de Repository<Produto> e implementa IProdutoRepository
 */

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    /* Utilizaremos o base() que indica que vamos utilizae membros da classe derivada */
    public ProdutoRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = await GetAll();
        return produtos.Where(c=> c.CategoriaId == id);
    }
}
