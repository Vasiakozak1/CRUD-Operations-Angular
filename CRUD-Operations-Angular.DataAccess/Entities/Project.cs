using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Operations_Angular.DataAccess.Entities
{
    public class Project: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<UsersProjects> Users { get; set; } = new List<UsersProjects>();
}
}
