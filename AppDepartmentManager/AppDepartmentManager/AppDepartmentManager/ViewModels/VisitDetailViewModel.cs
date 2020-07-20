using AppDepartmentManager.Models;
using AppDepartmentManager.Serivices;
using AppDepartmentManager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppDepartmentManager.ViewModels
{
    public class VisitDetailViewModel : BaseViewModel
    {
        private readonly int id;

        /*********************COMMANDS***********************/
        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(DeleteAction));

        Command editCommand;
        public Command EditCommand => editCommand ?? (editCommand = new Command(EditAction));

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        /*********************BINDABLE PROPS***********************/
        private VisitModel visitSel;
        public VisitModel VisitSel
        {
            get => visitSel;
            set => SetProperty(ref visitSel, value);
        }

        string _supervisor;
        public string Supervisor
        {
            get => _supervisor;
            set => SetProperty(ref _supervisor, value);
        }

        string _Picture;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }

        DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        int _actualPosition;
        public int ActualPosition
        {
            get => _actualPosition;
            set => SetProperty(ref _actualPosition, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        /*********************CONSTRUCTORS***********************/
        public VisitDetailViewModel()
        {
            VisitSel = new VisitModel();
        }

        public VisitDetailViewModel(VisitModel visit)
        {
            id = visit.Id;
            Supervisor = visit.Supervisor;
            Picture = visit.Picture;
            Date = visit.Date;
            Latitude = visit.SupervisorLocation.Latitude;
            Longitude = visit.SupervisorLocation.Longitude;
            VisitSel = visit;
        }

        private async void CancelAction()
        {
            // await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void DeleteAction()
        {
            IsBusy = true;

            if (id == 0)
                await Application.Current.MainPage.DisplayAlert("Invalid Id", "Error", "Ok");
            else
            {
                ApiResponse response = await new ApiService().DeleteDataAsync("visit", id);
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit app", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit app", response.Message, "Ok");
                    return;
                }
                VisitsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Visit app", response.Message, "Ok");

                IsBusy = false;
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private async void EditAction()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new VisitFormPage(VisitSel))
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }
    }

}
