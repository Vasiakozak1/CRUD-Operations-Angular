using System.Collections.Generic;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.ViewModels;
namespace CRUD_Operations_Angular.DataAccess.Mappers
{
    public interface IMapper
    {
        IViewModel Map(IEntity entity);
        IEntity Map(IViewModel viewModel);
        IEnumerable<IViewModel> MapEnumerable(IEnumerable<IEntity> entities);
        IEnumerable<IEntity> MapEnumerable(IEnumerable<IViewModel> viewModels);
    }
}