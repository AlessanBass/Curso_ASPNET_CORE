using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
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
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    //Metodo GEt -> Obtem todos os produtos
    [HttpGet] /* /produtos */
    public  async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync(){
        var produtos = await _uof.ProdutoRepository.GetAll();
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDTO);
    }

    /* Incluindo restrição nos paramentros */
    [HttpGet("id:int:min(1)", Name ="ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id){
        var produto = await _uof.ProdutoRepository.Get(p=> p.ProdutoId == id);
        return Ok(produto);
    }

    [HttpGet("/ProdutosPorCategoria/{id}")]
    public async Task<ActionResult<ProdutoDTO>> GetProdutoPorCategoria(int id){
        var produto = await _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        var produtoDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);
        return Ok(produtoDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDTO){
        if(produtoDTO is null){
            return BadRequest();
        }
        var produto = _mapper.Map<Produto>(produtoDTO);
        var produtoCreate = await _uof.ProdutoRepository.Create(produto);
        await _uof.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterProduto", new {id = produtoCreate.ProdutoId}, produtoCreate);
    }

    [HttpPut("id")]
    public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDTO){
        if(id != produtoDTO.ProdutoId){
            return BadRequest();
        }

       var produto = _mapper.Map<Produto>(produtoDTO);
       var produtoAtualizado = await _uof.ProdutoRepository.Update(produto);
       await _uof.SaveChangesAsync();

        return Ok(produtoAtualizado);
    }

    [HttpDelete("id")]
    public async Task<ActionResult<ProdutoDTO>> Delete (int id){
        var produto = await _uof.ProdutoRepository.Get(c=>c.ProdutoId == id);;
        if(produto is null){
            return NotFound("Produto não encontrado");
        }

        await _uof.ProdutoRepository.Delete(produto);
        await _uof.SaveChangesAsync();

        return Ok(produto);
    }
}
