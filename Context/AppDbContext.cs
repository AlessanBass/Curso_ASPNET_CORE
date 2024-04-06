using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;
/* Classe responsavel por realizar a comunição entre as minhas entidades
    e o banco de dados relacional.
 */
public class AppDbContext : DbContext
{
    /* Atalho ctor cria um construtor */
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    /* 
        Deixando a propriedade como nulable, podemos garanntir que a propriedade
         seja opcional
     */
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Produto> Produtos {get; set;}

}
