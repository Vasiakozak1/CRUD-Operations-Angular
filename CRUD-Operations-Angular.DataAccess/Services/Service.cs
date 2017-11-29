using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.Mappers;
using CRUD_Operations_Angular.DataAccess.Repository;
using CRUD_Operations_Angular.DataAccess.ViewModels;

namespace CRUD_Operations_Angular.DataAccess.Services
{
    public class Service<TSource, TTarget> : IService<TSource, TTarget> where TSource : class, IEntity where TTarget: IViewModel
    {
        private IUOWFactory _uowFactory;
        public BaseMapper _mapper;
        public Service(IUOWFactory factory)
        {
            this._uowFactory = factory;
        }

        public void Create(TTarget item)
        {
            TSource entity = (TSource)_mapper.Map(item);
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                repository.Create(entity);
            }
        }

        public void Delete(int id)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                repository.Delete(id);
            }
        }

        public TTarget Get(int id)
        {
            TTarget viewModel;
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                var entity = repository.Get(id);
                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }
                viewModel = (TTarget)_mapper.Map(entity);
            }
            
            return viewModel;
        }

        public TTarget Get<TProperty>(int id, Expression<Func<TSource, TProperty>> expression)
        {
            TTarget viewModel;
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                var entity = repository.Get(id, expression);

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }
                viewModel = (TTarget)_mapper.Map(entity);
                
            }           
            return viewModel;
        }

        public IEnumerable<TTarget> GetAll()
        {
            IList<TTarget> targetEntities = new List<TTarget>();
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                var elements = repository.GetAll();
                var tempModelsList = _mapper.MapEnumerable(elements);
                foreach (var model in tempModelsList) 
                {
                    targetEntities.Add((TTarget)model);
                }
            }
            return targetEntities;
        }

        public IEnumerable<TTarget> GetAll(Expression<Func<TSource, bool>> predicate)
        {
            IList<TTarget> targetEntities = new List<TTarget>();
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                var elements = repository.GetAll(predicate);
                var tempModelsList = _mapper.MapEnumerable(elements);
                foreach (var model in tempModelsList)
                {
                    targetEntities.Add((TTarget) model);
                }
            }
            return targetEntities;
        }

        public IEnumerable<TTarget> GetByIds(IEnumerable<int> ids)
        {
            IList<TTarget> targetEntities = new List<TTarget>();
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                var elements = repository.GetAll(element => ids.Contains(element.Id));
                var tempModelsList = _mapper.MapEnumerable(elements);
                foreach (var model in tempModelsList)
                {
                    targetEntities.Add((TTarget) model);
                }
            }
            return targetEntities;
        }

        public void Update(TTarget item)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                IRepository<TSource> repository = uow.CreateRepository<TSource>();
                TSource itemToUpdate = (TSource)_mapper.Map(item);
                repository.Update(itemToUpdate);
            }
        }
    }

}
