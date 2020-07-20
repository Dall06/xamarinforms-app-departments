using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppDepartmentManager.Views;

namespace AppDepartmentManager
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage()
            {
                BackgroundColor = (Color)App.Current.Resources["primaryColor"],
                Title = "App Departments"
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
