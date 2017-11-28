using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using CRUD_Operations_Angular.DataAccess.Entities;
namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
        void Rollback();
        IRepository<TType> CreateRepository<TType>() where TType : class, IEntity;
    }
}
