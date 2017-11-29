using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_Angular.DataAccess.ViewModels
{
    public class UserViewModel: IViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<UsersProjectsViewModel> Projects { get; set; }
    }
}
