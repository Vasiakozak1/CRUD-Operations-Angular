using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_Operations_Angular.DataAccess.Repository;
using CRUD_Operations_Angular.DataAccess.ViewModels;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.Mappers;
using CRUD_Operations_Angular.DataAccess.Services;
namespace CRUD_Operations_Angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private IService<Project, ProjectViewModel> _service;    
        public ProjectsController(IService<Project,ProjectViewModel> service)
        {
            this._service = service;
            this._service.Mapper = new ProjectMapper();
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<ProjectViewModel> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ProjectViewModel Get(int id)
        {
            return _service.Get(id, proj => proj.Users);
        }

        [HttpPost]
        [Route("getbyids")]
        public IEnumerable<ProjectViewModel> GetByIds([FromBody]IEnumerable<int> projectsIds)
        {
            return _service.GetByIds(projectsIds);
        }

        [HttpPost]
        [Route("[action]")]
        public void Create([FromBody]ProjectViewModel projectViewModel)
        {
            _service.Create(projectViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        public void Update([FromBody]ProjectViewModel projectViewModel)
        {           
           _service.Update(projectViewModel);
        }

        

        [HttpDelete]
        [Route("[action]/{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}