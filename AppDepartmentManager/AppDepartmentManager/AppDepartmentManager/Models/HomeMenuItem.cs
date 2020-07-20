using System;
using System.Collections.Generic;
using System.Text;

namespace AppDepartmentManager.Models
{
    public enum MenuItemType
    {
        Departament,
        Visit,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
