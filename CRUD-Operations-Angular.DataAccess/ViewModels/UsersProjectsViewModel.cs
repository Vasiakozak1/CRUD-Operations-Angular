﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_Angular.DataAccess.ViewModels
{
    public class UsersProjectsViewModel: IViewModel
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
