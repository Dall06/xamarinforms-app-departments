using AppDepartmentManager.Models;
using AppDepartmentManager.Serivices;
using AppDepartmentManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AppDepartmentManager.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {
        static DepartmentsViewModel _instance;

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        //Command which refresh the data
        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ?? (refreshCommand = new Command(RefreshDrivers));

        Command createCommand;
        public Command CreateCommand => createCommand ?? (createCommand = new Command(CreateAction));

        DepartmentModel dptoSelected;
        public DepartmentModel DptoSelected
        {
            get => dptoSelected;
            set => SetProperty(ref dptoSelected, value);
        }

        ObservableCollection<DepartmentModel> _Dptos;
        public ObservableCollection<DepartmentModel> Dptos
        {
            get => _Dptos;
            set => SetProperty(ref _Dptos, value);
        }

        public DepartmentsViewModel()
        {
            Title = "Departments";
            _instance = this;
            LoadDpto();
        }

        private async void LoadDpto()
        {
            IsBusy = true;
            ApiResponse response = await new ApiService().GetDataAsync<DepartmentModel>("department");

            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error loading dptos", response.Message, "Ok");
                return;
            }

            Dptos = (ObservableCollection<DepartmentModel>)response.Result;
            IsBusy = false;
        }

        public static DepartmentsViewModel GetInstance()
        {
            if (_instance == null) _instance = new DepartmentsViewModel();
            return _instance;
        }

        private async void SelectAction()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DepartmentDetailPage(DptoSelected))
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }

        private async void CreateAction()
        {
            //Application.Current.MainPage.Navigation.PushAsync(new DriverDetailPage(DriverSelected));
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DepartmentFormPage())
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }

        public void RefreshDrivers()
        {
            LoadDpto();
        }
    }

}
