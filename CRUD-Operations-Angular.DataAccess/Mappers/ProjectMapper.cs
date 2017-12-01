using System;
using System.Collections.Generic;
using System.Text;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.ViewModels;

namespace CRUD_Operations_Angular.DataAccess.Mappers
{
    public class ProjectMapper : BaseMapper
    {
        protected override IEntity GetEntity(IViewModel viewModel)
        {
            var projectViewModel = viewModel as ProjectViewModel;
            if (projectViewModel == null)
            {
                throw new Exception();
            }
            var result = new Project
            {
                Id = projectViewModel.Id,
                Name = projectViewModel.Name,
                Description = projectViewModel.Description,
                StartDate = new DateTime(projectViewModel.StartYear, projectViewModel.StartMonth,
                    projectViewModel.StartDay),
                EndDate = new DateTime(projectViewModel.EndYear, projectViewModel.EndMonth, projectViewModel.EndDay)
            };
            result.Users = new List<UsersProjects>();

            if (projectViewModel.Users != null && projectViewModel.Users.Count != 0)
            {
                result.Users = new List<UsersProjects>();
                foreach (var userProject in projectViewModel.Users)
                {
                    result.Users.Add(new UsersProjects
                    {
                        UserId = userProject.UserId,
                        ProjectId = userProject.ProjectId
                    });
                }
            }

            return result;
        }

        protected override IViewModel GetViewModel(IEntity entity)
        {
            var project = entity as Project;
            if (project == null)
            {
                throw new Exception();
            }

            var result = new ProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,

                StartDay = project.StartDate.Day,
                StartMonth = project.StartDate.Month,
                StartYear = project.StartDate.Year,

                EndDay = project.EndDate.Day,
                EndMonth = project.EndDate.Month,
                EndYear = project.EndDate.Year
            };
            if (project.Users == null || project.Users.Count == 0)
            {
                result.Users = new List<UsersProjectsViewModel>();
            }
            else
            {
                IList<UsersProjectsViewModel> usersProjectsList = new List<UsersProjectsViewModel>();
                foreach (var userProject in project.Users)
                {
                    usersProjectsList.Add(new UsersProjectsViewModel
                    {
                        ProjectId = userProject.ProjectId,
                        UserId = userProject.UserId
                    });
                }
                result.Users = usersProjectsList;
            }
            return result;
        }
    }
}
