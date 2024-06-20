using APICatalogo.Context;
using APICatalogo.Models;
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
    private readonly AppDbContext _context;
    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    //Metodo GEt -> Obtem todos os produtos
    [HttpGet] /* /produtos */
    public  async Task<ActionResult<IEnumerable<Produto>>> GetAsync(){
        var produtos = await  _context.Produtos.ToListAsync();
        if(produtos is null){
            return NotFound("Produtos não encontrados");
        }
        return produtos;
    }

    /* Incluindo restrição nos paramentros */
    [HttpGet("id:int:min(1)", Name ="ObterProduto")]
    public ActionResult<Produto> Get(int id){
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if(produto is null){
            return NotFound("Produto não encontrado");
        }
        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto){
        if(produto is null){
            return BadRequest();
        }
        //Precisamos incluir o produto no contexto
        _context.Add(produto);
        _context.SaveChanges(); //Persiste os dados na tabela

        return new CreatedAtRouteResult("ObterProduto", new {id = produto.ProdutoId}, produto);
    }

    [HttpPut("id")]
    public ActionResult Put(int id, Produto produto){
        if(id != produto.ProdutoId){
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("id")]
    public ActionResult Delete (int id){
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if(produto is null){
            return NotFound("Produto não encontrado");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
