using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Repository;
using CRUD_Operations_Angular.DataAccess.ViewModels;

namespace CRUD_Operations_Angular.DataAccess.Services
{
    public interface IService<TSource, TTarget> where TSource: class, IEntity where TTarget: IViewModel
    {
        void Create(TTarget item);
        TTarget Get(int id);
        TTarget Get<TProperty>(int id, Expression<Func<TSource, TProperty>> expression);
        IEnumerable<TTarget> GetByIds(IEnumerable<int> ids); 
        IEnumerable<TTarget> GetAll();
        IEnumerable<TTarget> GetAll(Expression<Func<TSource, bool>> predicate);
        void Update(TTarget item);
        void Delete(int id);
    }
}