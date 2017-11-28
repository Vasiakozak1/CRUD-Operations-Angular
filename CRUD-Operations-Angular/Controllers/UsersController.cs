using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Angular.Web.ViewModels;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Repository;
namespace CRUD_Operations_Angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IUOWFactory _factory;

        public UsersController(IUOWFactory factory)
        {
            this._factory = factory;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<UserViewModel> GetAll()
        {
            List<UserViewModel> userDtos = new List<UserViewModel>();
            using (var uow = _factory.CreateUnitOfWork())
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                var users = repository.GetAll();
                foreach (User user in users)
                {
                    List<UsersProjectsViewModel> usersProjectsForViewModel = new List<UsersProjectsViewModel>();
                    foreach (var projUser in user.Projects)
                    {
                        usersProjectsForViewModel.Add(new UsersProjectsViewModel()
                        {
                            ProjectId = projUser.ProjectId,
                            UserId = projUser.UserId
                        });
                    }
                    UserViewModel userDto = new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Projects = usersProjectsForViewModel
                    };
                    userDtos.Add(userDto);
                }
            }
            return userDtos;
        }

        [HttpGet]
        [Route("GetError")]
        public IActionResult GetTestError()
        {
            return BadRequest(StatusCode(401, "very bad response"));
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public UserViewModel Get(int id)
        {
            UserViewModel userDto = null;
            using (var uow = _factory.CreateUnitOfWork())
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                List<UsersProjectsViewModel> usersProjectsList = new List<UsersProjectsViewModel>();                
                User user = repository.Get(id, u => u.Projects);
                foreach (UsersProjects element in user.Projects)
                {
                    usersProjectsList.Add(new UsersProjectsViewModel()
                    {
                        ProjectId = element.ProjectId,
                        UserId = element.UserId
                    });
                }
                userDto = new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Projects = usersProjectsList
                };
            }
            return userDto;
        }


        [HttpPost]
        [Route("getusersbyids")]
        public IEnumerable<UserViewModel> GetUsersByIds([FromBody]IEnumerable<int> ids)
        {
            IList<UserViewModel> resultUsersList = new List<UserViewModel>();
            IList<int> listOfIds = ids.ToList();
            using (var uow = _factory.CreateUnitOfWork())
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                var users = repository.GetAll();
                foreach (var user in users)
                {
                    if (listOfIds.Contains(user.Id))
                    {
                        resultUsersList.Add(new UserViewModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Age = user.Age
                        });
                    }
                }
            }
            return resultUsersList;
        }

        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]UserViewModel userDto)
        {
            using (var uow = _factory.CreateUnitOfWork())
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };
                repository.Create(user);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public void Update([FromBody]UserViewModel user)
        {
            using (var uow = _factory.CreateUnitOfWork()) 
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                User userWithProjects = repository.Get(user.Id, u => u.Projects);
                userWithProjects.Age = user.Age;
                userWithProjects.FirstName = user.FirstName;
                userWithProjects.LastName = user.LastName;
                repository.Update(userWithProjects);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            using (var uow = _factory.CreateUnitOfWork()) 
            {
                IRepository<User> repository = uow.CreateRepository<User>();
                repository.Delete(id);
            }
        }
    }
}