using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_Angular.Web.ViewModels
{
    public class ProjectViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int startDay { get; set; }
        public int startMonth { get; set; }
        public int startYear { get; set; }

        public int endDay { get; set; }
        public int endMonth { get; set; }
        public int endYear { get; set; }
        public ICollection<UsersProjectsViewModel> users { get; set; }
    }
}
