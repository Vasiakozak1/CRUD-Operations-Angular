namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public class UOWFactory: IUOWFactory
    {
        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}