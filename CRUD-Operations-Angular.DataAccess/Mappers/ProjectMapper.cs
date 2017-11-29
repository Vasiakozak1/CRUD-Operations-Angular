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
            return new Project
            {
                Name = projectViewModel.name,
                Description = projectViewModel.description,
                StartDate = new DateTime(projectViewModel.startYear, projectViewModel.startMonth,
                    projectViewModel.startDay),
                EndDate = new DateTime(projectViewModel.endYear, projectViewModel.endMonth, projectViewModel.endDay),
                Users = new List<UsersProjects>()
            };
        }

        protected override IViewModel GetViewModel(IEntity entity)
        {
            var project = entity as Project;
            if (project == null)
            {
                throw new Exception();
            }
            return new ProjectViewModel
            {
                name = project.Name,
                description = project.Description,

                startDay = project.StartDate.Day,
                startMonth = project.StartDate.Month,
                startYear = project.StartDate.Year,

                endDay = project.EndDate.Day,
                endMonth = project.EndDate.Month,
                endYear = project.EndDate.Year,
                users = new List<UsersProjectsViewModel>()
            };

        }
    }
}
