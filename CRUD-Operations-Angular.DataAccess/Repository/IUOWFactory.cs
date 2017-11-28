namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public interface IUOWFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}