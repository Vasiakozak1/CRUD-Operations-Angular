using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Angular.DataAccess.Repository;
using CRUD_Operations_Angular.Web.ViewModels;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Entities;
namespace CRUD_Operations_Angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        [HttpGet]
        [Route("Get")]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            List<ProjectViewModel> projectsDtos = new List<ProjectViewModel>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                var projects = repository.GetAll();
                foreach (Project project in projects)
                {
                    List<UsersProjectsViewModel> usersProjectsForViewModel = new List<UsersProjectsViewModel>();
                    foreach (var userProj in project.Users)
                    {
                        usersProjectsForViewModel.Add(new UsersProjectsViewModel()
                        {
                            ProjectId = userProj.ProjectId,
                            UserId = userProj.UserId
                        });
                    }

                    ProjectViewModel dto = new ProjectViewModel()
                    {
                        id = project.Id,
                        name = project.Name,
                        description = project.Description,
                        
                        startYear = project.StartDate.Year,
                        startMonth = project.StartDate.Month,
                        startDay = project.StartDate.Day,

                        endYear = project.EndDate.Year,
                        endMonth = project.EndDate.Month,
                        endDay = project.EndDate.Day,
                        users = usersProjectsForViewModel
                    };
                    projectsDtos.Add(dto);
                }
            }
            return projectsDtos;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ProjectViewModel Get(int id)
        {
            ProjectViewModel projDTO = null;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                Project project = repository.Get(id, proj => proj.Users);
                if (project == null)
                    return null;

                List<UsersProjectsViewModel> projectsUsers = new List<UsersProjectsViewModel>();
                foreach (UsersProjects userProject in project.Users)
                {
                    projectsUsers.Add(new UsersProjectsViewModel()
                    {
                        UserId = userProject.UserId,
                        ProjectId = userProject.ProjectId
                    });
                }
                projDTO = new ProjectViewModel()
                {
                    id = project.Id,
                    name = project.Name,
                    description = project.Description,

                    startYear = project.StartDate.Year,
                    startMonth = project.StartDate.Month,
                    startDay = project.StartDate.Day,

                    endYear = project.EndDate.Year,
                    endMonth = project.EndDate.Month,
                    endDay = project.EndDate.Day,
                    users = projectsUsers
                };
            }
            return projDTO;
        }
        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]ProjectViewModel projectDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                Project project = new Project()
                {
                    Name = projectDto.name,
                    Description = projectDto.description,
                    StartDate = new DateTime(projectDto.startYear, projectDto.startMonth, projectDto.startDay),
                    EndDate = new DateTime(projectDto.endYear, projectDto.endMonth, projectDto.endDay),
                    Users = new List<UsersProjects>()
                };
                repository.Create(project);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void Update([FromBody]ProjectViewModel projectDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                List<UsersProjects> usersProjectsList = new List<UsersProjects>();
                foreach (UsersProjectsViewModel model in projectDto.users)
                {
                    usersProjectsList.Add(new UsersProjects()
                    {
                        UserId   = model.UserId,
                        ProjectId = model.ProjectId                     
                    });
                }

                Project project = new Project()
                {
                    Id = projectDto.id,
                    Name = projectDto.name,
                    Description = projectDto.description,
                    StartDate = new DateTime(projectDto.startYear, projectDto.startMonth, projectDto.startDay),
                    EndDate = new DateTime(projectDto.endYear, projectDto.endMonth, projectDto.endDay),
                    Users = usersProjectsList                    
                };
                repository.Update(project);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void AttachUser([FromBody] AttachUsersViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> userRepository = new Repository<User>(context);
                IRepository<Project> projectRepository = new Repository<Project>(context);
                User user = userRepository.Get(model.userId);
                Project project = projectRepository.Get(model.projId);
                project.Users.Add(new UsersProjects()
                {
                    User = user,
                    UserId = user.Id,
                    ProjectId = project.Id,
                    Project = project
                });

                projectRepository.Update(project);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void DetachUser([FromBody] AttachUsersViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> projectRepository = new Repository<Project>(context);
                Project project = projectRepository.Get(model.projId, proj => proj.Users);
                foreach (UsersProjects usersProject in project.Users)
                {
                    if (usersProject.UserId == model.userId)
                    {
                        project.Users.Remove(usersProject);
                        break;
                    }
                }

                projectRepository.Update(project);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<Project> repository = new Repository<Project>(context);
                repository.Delete(id);
            }
        }
    }
}