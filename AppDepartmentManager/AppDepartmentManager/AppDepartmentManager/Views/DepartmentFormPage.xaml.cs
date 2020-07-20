using AppDepartmentManager.Models;
using AppDepartmentManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDepartmentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepartmentFormPage : ContentPage
    {
        public DepartmentFormPage()
        {
            InitializeComponent();
            BindingContext = new DepartmentFormViewModel();
        }

        public DepartmentFormPage(DepartmentModel dpto)
        {
            InitializeComponent();

            BindingContext = new DepartmentFormViewModel(dpto);
        }
    }
}