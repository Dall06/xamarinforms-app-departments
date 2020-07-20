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
    public class VisitsViewModel : BaseViewModel
    {
        static VisitsViewModel _instance;

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        //Command which refresh the data
        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ?? (refreshCommand = new Command(RefreshDrivers));

        Command createCommand;
        public Command CreateCommand => createCommand ?? (createCommand = new Command(CreateAction));

        VisitModel visitSelected;
        public VisitModel VisitSelected
        {
            get => visitSelected;
            set => SetProperty(ref visitSelected, value);
        }

        ObservableCollection<VisitModel> _Visits;
        public ObservableCollection<VisitModel> Visits
        {
            get => _Visits;
            set => SetProperty(ref _Visits, value);
        }

        public VisitsViewModel()
        {
            Title = "Visits";
            _instance = this;
            LoadVisit();
        }

        private async void LoadVisit()
        {
            IsBusy = true;
            ApiResponse response = await new ApiService().GetDataAsync<VisitModel>("visit");

            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error loading visits", response.Message, "Ok");
                return;
            }

            Visits = (ObservableCollection<VisitModel>)response.Result;
            IsBusy = false;
        }

        public static VisitsViewModel GetInstance()
        {
            if (_instance == null) _instance = new VisitsViewModel();
            return _instance;
        }

        private async void SelectAction()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new VisitDetailPage(VisitSelected))
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }

        private async void CreateAction()
        {
            //Application.Current.MainPage.Navigation.PushAsync(new DriverDetailPage(DriverSelected));
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new VisitFormPage())
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }

        public void RefreshDrivers()
        {
            LoadVisit();
        }
    }
}
