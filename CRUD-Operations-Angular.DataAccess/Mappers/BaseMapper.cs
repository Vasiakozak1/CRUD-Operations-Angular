using System.Collections.Generic;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.ViewModels;

namespace CRUD_Operations_Angular.DataAccess.Mappers
{
    public abstract class BaseMapper : IMapper
    {
        public IViewModel Map(IEntity entity)
        {
            object viewModel = GetViewModel(entity);
            return (IViewModel)viewModel;
        }

        public IEntity Map(IViewModel viewModel)
        {
            object entity = GetEntity(viewModel);
            return (IEntity) entity;
        }

        public IEnumerable<IViewModel> MapEnumerable(IEnumerable<IEntity> entities)
        {
            List<IViewModel> viewModelList = new List<IViewModel>();
            foreach (var entity in entities)
            {
                viewModelList.Add(this.Map(entity));
            }
            return viewModelList;
        }

        public IEnumerable<IEntity> MapEnumerable(IEnumerable<IViewModel> viewModels)
        {
            List<IEntity> entityList = new List<IEntity>();
            foreach (var viewModel in viewModels)
            {
                entityList.Add(this.Map(viewModel));
            }
            return entityList;
        }

        protected abstract IViewModel GetViewModel(IEntity entity);
        protected abstract IEntity GetEntity(IViewModel viewModel);
    }
}