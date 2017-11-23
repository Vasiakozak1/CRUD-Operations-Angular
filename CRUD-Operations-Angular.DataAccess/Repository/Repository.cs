using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Entities;
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


        public void Update(T entity)
        {
            var element = _entities.Find(entity.Id);
            if (element != null)
            {
                _entities.Update(entity);
            }

            _context.SaveChanges();
        }
    }
}
