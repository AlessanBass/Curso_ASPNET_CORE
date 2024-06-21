﻿using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;
/* 
    Para garantir que o tipo T seja uma classe de foto, podemos incluir a seguinte 
    restrição
 */
public class Repository<T> : IRepository<T> where T : class
{
    /* Nesta fase devemos utilizar o modificador protected que diz respeito
        que essa classe so pode acessada através de quem a herdou.

        Ou seja, essa variavel so vai poder ser visivel na prórpia classe e 
         nas classes derivadas.
     */

    protected readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        /* Utilizamos o SET para acessar uma coleção */
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
