using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_Angular.DataAccess.ViewModels
{
    public class ProjectViewModel: IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int StartDay { get; set; }
        public int StartMonth { get; set; }
        public int StartYear { get; set; }

        public int EndDay { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public ICollection<UsersProjectsViewModel> Users { get; set; }
    }
}
