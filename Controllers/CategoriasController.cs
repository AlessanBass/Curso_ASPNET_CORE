using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    /*
        Aqui utilizamos uma Interface por nos dar mais flexibilidade, pois como é um interface
         podemos ter diversas implementações dos métodos.
     */
    private readonly IUnitOfWork _uof;
    public CategoriasController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAll(){
        var categorias = await _uof.CategoriaRepository.GetCategorias();
        if(categorias is null){
            return NotFound("Nenhuma categoria encontrada!");
        }

        return Ok(categorias); /* Retorna um 200 */
    }

    [HttpGet("id", Name ="ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetOneId(int id){
        var categoria = await _uof.CategoriaRepository.GetCategoria(id);

         if(categoria is null){
            return NotFound("Categoria não encontrada!");
        }

        return Ok(categoria); /* Retorna um 200 */
    }

   /*  [HttpGet("produtos/id_categoria")]
    public ActionResult<IEnumerable<Categoria>> GetAllCategoriaProduto(int id_categoria){
        var produtosPorCategoria = _context.Categorias.Include(p=> p.Produtos).Where(c=> c.CategoriaId == id_categoria).ToList();

        if(produtosPorCategoria is null){
            return NotFound("Pordutos não encontrados!");
        }

        return produtosPorCategoria;

    } */

    [HttpPost]
    public async Task<ActionResult> Post(Categoria categoria){
        if(categoria is null){
            return BadRequest();
        }
        //Precisamos incluir o produto no contexto
        //_context.Add(categoria);
       // _context.SaveChanges(); //Persiste os dados na tabela
       var redultado = await _uof.CategoriaRepository.Create(categoria);
       await _uof.SaveChangesAsync();


        return new CreatedAtRouteResult("ObterCategoria", new {id = redultado.CategoriaId}, redultado);
    }

    [HttpPut("id")]
    public async Task<ActionResult> Put(int id, Categoria categoria){
        if(id != categoria.CategoriaId){
            return BadRequest();
        }

       await _uof.CategoriaRepository.Update(categoria);
       await _uof.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete (int id){
        var categoria = await _uof.CategoriaRepository.GetCategoria(id);
        if(categoria is null){
            return NotFound("Categoria não encontrado");
        }

       await _uof.CategoriaRepository.Delete(id);
       await _uof.SaveChangesAsync();

        return Ok(categoria);
    }
}
