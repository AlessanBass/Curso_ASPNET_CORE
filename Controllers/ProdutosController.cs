using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase /* Extrari o nome da rota com base no nome da classe */
{
    /*
        Para utilizar o banco de dados precisamos fazer uma injeção de dependecia
         do nosso context.
     */

     /* 
        Agora vamos utilizar o nosso repositorio especifico
     
      */
    private readonly IProdutoRepository _produtoRepository;
    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    //Metodo GEt -> Obtem todos os produtos
    [HttpGet] /* /produtos */
    public  async Task<ActionResult<IEnumerable<Produto>>> GetAsync(){
        var produtos =await _produtoRepository.GetAll();
        return Ok(produtos);
    }

    /* Incluindo restrição nos paramentros */
    [HttpGet("id:int:min(1)", Name ="ObterProduto")]
    public async Task<ActionResult<Produto>> Get(int id){
        var produto = await _produtoRepository.Get(p=> p.ProdutoId == id);
        return Ok(produto);
    }

    [HttpGet("/ProdutosPorCategoria/{id}")]
    public async Task<ActionResult<Produto>> GetProdutoPorCategoria(int id){
        var produto = await _produtoRepository.GetProdutosPorCategoria(id);
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Produto produto){
        if(produto is null){
            return BadRequest();
        }
        
        var produtoCreate = await _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new {id = produtoCreate.ProdutoId}, produtoCreate);
    }

    [HttpPut("id")]
    public async Task<ActionResult> Put(int id, Produto produto){
        if(id != produto.ProdutoId){
            return BadRequest();
        }

       var produtoAtualizado = await _produtoRepository.Update(produto);

        return Ok(produtoAtualizado);
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete (int id){
        var produto = await _produtoRepository.Get(c=>c.ProdutoId == id);;
        if(produto is null){
            return NotFound("Produto não encontrado");
        }

        await _produtoRepository.Delete(produto);

        return Ok(produto);
    }
}
