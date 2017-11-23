using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRUD_Operations_Angular.DataAccess.Entities;
namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity: class, IEntity
    {
        TEntity Get(int id);
        TEntity Get<TProperty>(int id, Expression<Func<TEntity, TProperty>> expression);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}