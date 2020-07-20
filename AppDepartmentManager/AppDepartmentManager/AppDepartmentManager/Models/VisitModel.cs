using System;
using System.Collections.Generic;
using System.Text;

namespace AppDepartmentManager.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Picture { get; set; }
        public string Supervisor { get; set; }
        public PositionModel SupervisorLocation { get; set; }

    }
}
