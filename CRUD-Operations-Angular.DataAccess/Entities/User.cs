﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Operations_Angular.DataAccess.Entities
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<UsersProjects> Projects { get; set; } = new List<UsersProjects>();
}
}
