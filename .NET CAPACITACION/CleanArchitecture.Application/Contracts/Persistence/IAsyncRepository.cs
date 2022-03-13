using System;
using System.Linq.Expressions;
using CleanAcrchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        //Devolvere una colección de datos de una clase en particular de forma asincrona
        Task<IReadOnlyList<T>> GetAllAsync();
        //le pasaremos un objeto de tipo expression para buscar datos den la BD
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> Predicate);
        //este emedtodo me permitira hacer un get de datos hacia la BD y agregar multiples entidades a la consulta
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> Predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        //este emedtodo me permitira hacer un get de datos hacia la BD y me permitirá ordenarlo además de agregar la paginación
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> Predicate = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
                                List<Expression<Func<T, object>>> includes = null,
                                bool disableTracking = true);
        //este metodo me permitira buscar un dato en particular desde su identificador
        Task<T> GetByIdAsync(int Id);
        //este metodo me permitira agregar una entidad
        Task<T> AddAsync(T Entity);
        //este metodo me permitira actualizar una entidad
        Task<T> UpdateAsync(T Entity);
        //este metodo me permitira eliminar una entidad
        Task<T> DeleteAsync(T Entity);
    }
}