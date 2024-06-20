using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> Create(Categoria categoria)
    {
        if (categoria is null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;

    }

    public async Task<Categoria> Delete(int id)
    {
        var reultado = _context.Categorias.Find(id); /* Find não procura no banco, ele vai direto na memória */
        if (reultado is null)
        {
            throw new ArgumentNullException(nameof(reultado));
        }

         _context.Categorias.Remove(reultado);
         await _context.SaveChangesAsync();
         return reultado;
    }

    public async Task<Categoria> GetCategoria(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
        if (categoria is null)
        {
            throw new InvalidOperationException("Nenhuma categoria encontrada!");
        }
        return categoria;
    }

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> Update(Categoria categoria)
    {
        if (categoria is null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Entry(categoria).State = EntityState.Modified; /* Aqui é em memória, não usar await */
        await _context.SaveChangesAsync();

        return categoria;

    }
}
