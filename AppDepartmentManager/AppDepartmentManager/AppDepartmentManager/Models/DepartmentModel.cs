using System;
using System.Collections.Generic;
using System.Text;

namespace AppDepartmentManager.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public PositionModel Location { get; set; }
    }
}
