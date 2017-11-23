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
        [HttpGet]
        [Route("Get")]
        public IEnumerable<UserViewModel> GetAll()
        {
            List<UserViewModel> userDtos = new List<UserViewModel>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
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
        [Route("[action]/{id}")]
        public UserViewModel Get(int id)
        {
            UserViewModel userDto = null;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                User user = repository.Get(id);
                userDto = new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                };
            }
            return userDto;
        }

        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]UserViewModel userDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
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
        public void Update(UserViewModel userDto)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Age = userDto.Age
                };
                repository.Update(user);
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                IRepository<User> repository = new Repository<User>(context);
                repository.Delete(id);
            }
        }
    }
}