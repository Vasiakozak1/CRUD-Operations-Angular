using System;
using System.Collections.Generic;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.ViewModels;

namespace CRUD_Operations_Angular.DataAccess.Mappers
{
    public class UserMapper : BaseMapper
    {
        protected override IEntity GetEntity(IViewModel viewModel)
        {
            var userViewModel = viewModel as UserViewModel;
            if (userViewModel == null)
            {
                throw new Exception();
            }
            return new User
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Age = userViewModel.Age,
                Projects = new List<UsersProjects>()
            };
        }

        protected override IViewModel GetViewModel(IEntity entity)
        {
            var user = entity as User;
            if (user == null)
            {
                throw new Exception();
            }
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Projects = new List<UsersProjectsViewModel>()
            };
        }
    }
}