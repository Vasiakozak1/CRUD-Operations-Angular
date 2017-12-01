using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Entities;
using CRUD_Operations_Angular.DataAccess.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T: class, IEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this._entities = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var element = _entities.Find(id);

            if (element != null)
            {
                _entities.Remove(element);
            }
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public T Get<TProperty>(int id, Expression<Func<T, TProperty>> expression)
        {
            return _entities.Include(expression).First(element => element.Id == id);
        }

        public IEnumerable<T> GetAll()
        {

            return _entities.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public void Attach(int userId, int projectId)
        {
            User user = _context.Users.First(u => u.Id == userId);
            Project project = _context.Projects.First(proj => proj.Id == projectId);
            if (user == null || project == null)
            {
                throw new EntityNotFoundException();
            }
            _context.UsersProjects.Add(new UsersProjects
            {
                User = user,
                UserId = userId,
                Project = project,
                ProjectId = projectId
            });
            _context.SaveChanges();
        }
        public void Detach(int userId, int projectId)
        {
            var userProjectDelete = _context.UsersProjects.First(userProj => userProj.ProjectId == projectId && userProj.UserId == userId);
            _context.UsersProjects.Remove(userProjectDelete);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Update1(T entity)
        {
            bool own = entity is User;

            switch(own)
            {
                case true:
                    User updatedUser = entity as User;
                    var usersProjects = new List<UsersProjects>();
                    foreach (var element in updatedUser.Projects)
                    {
                        usersProjects.Add(element);
                    }
                    _context.Users.Update(updatedUser);
                    User userToUpdate = _context.Users.Include(u => u.Projects).First(u => u.Id == updatedUser.Id);
                    userToUpdate.FirstName = updatedUser.FirstName;
                    userToUpdate.LastName = updatedUser.LastName;
                    userToUpdate.Age = updatedUser.Age;

                    if (updatedUser.Projects == null || updatedUser.Projects.Count == 0)
                    {
                        foreach (var userProject in updatedUser.Projects)
                        {
                            if (userToUpdate.Projects.Any(usrProj => usrProj.ProjectId == userProject.ProjectId)) // if contains a project with such id
                                continue;
                            Project projectToAdd = _context.Projects.First(proj => proj.Id == userProject.ProjectId);

                            if (projectToAdd == null)
                            {
                                throw new EntityNotFoundException();
                            }
                            userToUpdate.Projects.Add(new UsersProjects
                            {
                                User = userToUpdate,
                                UserId = userToUpdate.Id,
                                Project = projectToAdd,
                                ProjectId = projectToAdd.Id
                            });
                        }
                    }
                    else
                    {
                        userToUpdate.Projects = usersProjects;
                    }
                    break;

                case false:
                    Project updatedProject = entity as Project;
                    var projectsUsers = new List<UsersProjects>();
                    foreach (var element in updatedProject.Users) 
                    {
                        projectsUsers.Add(element);
                    }
                    Project projectToUpdate = _context.Projects.Include(proj => proj.Users).First(proj => proj.Id == updatedProject.Id); 
                    projectToUpdate.Name = updatedProject.Name;
                    projectToUpdate.Description = updatedProject.Description;
                    projectToUpdate.StartDate = updatedProject.StartDate;
                    projectToUpdate.EndDate = updatedProject.EndDate;
                    if (updatedProject.Users == null || updatedProject.Users.Count == 0)
                    {
                        foreach (var projectUser in updatedProject.Users)
                        {
                            if (projectToUpdate.Users.Any(projUser => projUser.UserId == projectUser.UserId)) // if contains a user with such id
                                continue;
                            User userToAdd = _context.Users.First(u => u.Id == projectUser.UserId);

                            if (userToAdd == null)
                            {
                                throw new EntityNotFoundException();
                            }
                            projectToUpdate.Users.Add(new UsersProjects
                            {
                                Project = projectToUpdate,
                                ProjectId = projectToUpdate.Id,
                                User = userToAdd,
                                UserId = userToAdd.Id
                            });
                        }
                    }
                    else
                    {
                        projectToUpdate.Users = projectsUsers;
                    }
                    break;
            }
            _context.SaveChanges();
        }
        
    }
}
