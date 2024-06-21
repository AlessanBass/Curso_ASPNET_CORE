using APICatalogo.Context;
using APICatalogo.DTOs;
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
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
    {
        var categorias = await _uof.CategoriaRepository.GetCategorias();
        if (categorias is null)
        {
            return NotFound("Nenhuma categoria encontrada!");
        }

        return Ok(categorias); /* Retorna um 200 */
    }

    [HttpGet("id", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> GetOneId(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetCategoria(id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada!");
        }

        /* Antes de retorna devo fazer o mapeamento do DTO que será retornado */
        var categoriaDTO = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            ImageUrl = categoria.ImageUrl,
            Nome = categoria.Nome
        };

        return Ok(categoriaDTO); /* Retorna um 200 */
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
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
        {
            return BadRequest();
        }
        /* Agora precisamos converter para uma entidade */
        var categoria = new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            ImageUrl = categoriaDTO.ImageUrl,
            Nome = categoriaDTO.Nome
        };
        var redultado = await _uof.CategoriaRepository.Create(categoria);
        await _uof.SaveChangesAsync();
        var novaCaegoria = new CategoriaDTO()
        {
            CategoriaId = redultado.CategoriaId,
            ImageUrl = redultado.ImageUrl,
            Nome = redultado.Nome
        };



        return new CreatedAtRouteResult("ObterCategoria", new { id = novaCaegoria.CategoriaId }, novaCaegoria);
    }

    [HttpPut("id")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }
        var categoria = new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            ImageUrl = categoriaDTO.ImageUrl,
            Nome = categoriaDTO.Nome
        };

        var retorno = await _uof.CategoriaRepository.Update(categoria);
        await _uof.SaveChangesAsync();

        var categoriaAtualizada = new CategoriaDTO{
            CategoriaId = retorno.CategoriaId,
            ImageUrl = retorno.ImageUrl,
            Nome = retorno.Nome
        };

        return Ok(categoriaAtualizada);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetCategoria(id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrado");
        }

        var retorno = await _uof.CategoriaRepository.Delete(id);
        await _uof.SaveChangesAsync();

        var categoriaDeletada = new CategoriaDTO{
            CategoriaId = retorno.CategoriaId,
            ImageUrl = retorno.ImageUrl,
            Nome = retorno.Nome
        };

        return Ok(categoriaDeletada);
    }
}
