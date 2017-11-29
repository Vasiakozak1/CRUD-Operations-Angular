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

            var result = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            };
            if (user.Projects == null || user.Projects.Count == 0)
            {
                result.Projects = new List<UsersProjectsViewModel>();
            }
            else
            {
                IList<UsersProjectsViewModel> usersProjectsList = new List<UsersProjectsViewModel>();
                foreach (var userProject in user.Projects)
                {
                    usersProjectsList.Add(new UsersProjectsViewModel
                    {
                        UserId = userProject.UserId,
                        ProjectId = userProject.ProjectId
                    });
                }
                result.Projects = usersProjectsList;
            }
            return result;
        }
    }
}