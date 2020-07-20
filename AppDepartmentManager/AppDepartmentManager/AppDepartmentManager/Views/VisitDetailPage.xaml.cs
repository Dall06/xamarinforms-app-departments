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
    public partial class VisitDetailPage : ContentPage
    {
        public VisitDetailPage()
        {
            InitializeComponent();

            BindingContext = new VisitDetailViewModel();
        }

        public VisitDetailPage(VisitModel visit)
        {
            InitializeComponent();

            BindingContext = new VisitDetailViewModel(visit);
        }
    }
}