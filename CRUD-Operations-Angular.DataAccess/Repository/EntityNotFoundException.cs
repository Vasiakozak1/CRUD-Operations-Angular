using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Operations_Angular.DataAccess.Repository
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException() : base("Entity not found")
        { }
    }
}
