using System;
using System.Collections.Generic;
using System.Text;
using CRUD_Operations_Angular.DataAccess.Context;
using CRUD_Operations_Angular.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork()
        {
            this._context = new ApplicationDbContext();
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }

        public IRepository<TType> CreateRepository<TType>() where TType: class, IEntity
        {
            return new Repository<TType>(_context);
        }

        public void Dispose()
        {
            this._context.Database.CloseConnection();
            this._context.Dispose();
            this._context = null;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
