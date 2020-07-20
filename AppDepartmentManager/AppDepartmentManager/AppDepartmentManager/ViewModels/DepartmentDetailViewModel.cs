using AppDepartmentManager.Models;
using AppDepartmentManager.Serivices;
using AppDepartmentManager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppDepartmentManager.ViewModels
{
    public class DepartmentDetailViewModel : BaseViewModel
    {
        private readonly int id;

        /*********************COMMANDS***********************/
        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(DeleteAction));

        Command editCommand;
        public Command EditCommand => editCommand ?? (editCommand = new Command(EditAction));

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(MapAction));

        /*********************BINDABLE PROPS***********************/
        private DepartmentModel dptoSel;
        public DepartmentModel DptoSel
        {
            get => dptoSel;
            set => SetProperty(ref dptoSel, value);
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }


        string _Picture;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
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
        public DepartmentDetailViewModel()
        {
            DptoSel = new DepartmentModel();
        }

        public DepartmentDetailViewModel(DepartmentModel dpto)
        {
            id = dpto.Id;
            Name = dpto.Name;
            Picture = dpto.Picture;
            Latitude = dpto.Location.Latitude;
            Longitude = dpto.Location.Longitude;
            DptoSel = dpto;
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
                ApiResponse response = await new ApiService().DeleteDataAsync("department", id);
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto app", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto app", response.Message, "Ok");
                    return;
                }
                DepartmentsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Dpto app", response.Message, "Ok");

                IsBusy = false;
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private async void EditAction()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DepartmentFormPage(DptoSel))
            {
                BackgroundColor = Color.FromHex("#0F1923")
            });
        }

        private void MapAction()
        {
             Application.Current.MainPage.Navigation.PushModalAsync(new DepartmentMapPage(new DepartmentModel
             {
                Id = DptoSel.Id,
                Name = DptoSel.Name, 
                Picture = DptoSel.Picture,
                Location = new PositionModel
                {
                    Latitude = DptoSel.Location.Latitude,
                    Longitude = DptoSel.Location.Longitude
                }
            }));
        }
    }

}
