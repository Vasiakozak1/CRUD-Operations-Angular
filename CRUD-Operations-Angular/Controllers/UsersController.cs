using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Angular.DataAccess.ViewModels;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.Services;
using CRUD_Operations_Angular.DataAccess.Mappers;
namespace CRUD_Operations_Angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IService<User, UserViewModel> _service;

        public UsersController(IService<User, UserViewModel> service)
        {
            this._service = service;
            this._service.Mapper = new UserMapper();
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<UserViewModel> GetAll()
        {
            return _service.GetAll();
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
            return _service.Get(id, user => user.Projects);
        }


        [HttpPost]
        [Route("getusersbyids")]
        public IEnumerable<UserViewModel> GetUsersByIds([FromBody]IEnumerable<int> ids)
        {
            return _service.GetByIds(ids);
        }

        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]UserViewModel userDto)
        {
            _service.Create(userDto);
        }

        [HttpPost]
        [Route("[action]")]
        public void Attach([FromBody]AttachUsersViewModel projectUser)
        {
            _service.Attach(projectUser.userId, projectUser.projId);
        }

        [HttpPost]
        [Route("[action]")]
        public void Detach([FromBody] AttachUsersViewModel projectUser)
        {
            _service.Detach(projectUser.userId, projectUser.projId);
        }

        [HttpPost]
        [Route("[action]")]
        public void Update([FromBody]UserViewModel user)
        {
            _service.Update(user);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}