using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetAll(){
        var categorias = _context.Categorias.ToList();
        if(categorias is null){
            return NotFound("Nenhuma categoria encontrada!");
        }

        return categorias;
    }

    [HttpGet("id", Name ="ObterCategoria")]
    public ActionResult<Categoria> GetOneId(int id){
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

         if(categoria is null){
            return NotFound("Categoria não encontrada!");
        }

        return categoria;
    }

    [HttpGet("produtos/id_categoria")]
    public ActionResult<IEnumerable<Categoria>> GetAllCategoriaProduto(int id_categoria){
        var produtosPorCategoria = _context.Categorias.Include(p=> p.Produtos).Where(c=> c.CategoriaId == id_categoria).ToList();

        if(produtosPorCategoria is null){
            return NotFound("Pordutos não encontrados!");
        }

        return produtosPorCategoria;

    }

    [HttpPost]
    public ActionResult Post(Categoria categoria){
        if(categoria is null){
            return BadRequest();
        }
        //Precisamos incluir o produto no contexto
        _context.Add(categoria);
        _context.SaveChanges(); //Persiste os dados na tabela

        return new CreatedAtRouteResult("ObterCategoria", new {id = categoria.CategoriaId}, categoria);
    }

    [HttpPut("id")]
    public ActionResult Put(int id, Categoria categoria){
        if(id != categoria.CategoriaId){
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("id")]
    public ActionResult Delete (int id){
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        if(categoria is null){
            return NotFound("Categoria não encontrado");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}
