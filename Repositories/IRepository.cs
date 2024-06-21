using System.Linq.Expressions;

namespace APICatalogo.Repositories;

/* 
    Para utilizar o repositório generico devemos definir a interface como genérica.
    Geralmente utilizando para implementar o CRUD básico
 */
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);

}
