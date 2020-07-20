using AppDepartmentManager.Models;
using AppDepartmentManager.Serivices;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppDepartmentManager.ViewModels
{
    public class VisitFormViewModel : BaseViewModel
    {
        private int id;

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(SaveAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

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

        ImageSource imageSource_;
        public ImageSource ImageSource_
        {
            get => imageSource_;
            set => SetProperty(ref imageSource_, value);
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
        public VisitFormViewModel()
        {
            VisitSel = new VisitModel();
        }

        public VisitFormViewModel(VisitModel visit)
        {
            id = visit.Id;
            Date = visit.Date;
            Picture = visit.Picture;
            Supervisor = visit.Supervisor;
            Longitude = visit.SupervisorLocation.Longitude;
            Latitude = visit.SupervisorLocation.Latitude;

           // ActualPosition = 2;//visit.ActualPosition;
        }


        /*********************ACTIONS***********************/
        private async void SaveAction()
        {
            IsBusy = true;

            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("visit", new VisitModel
                {
                    Supervisor = this.Supervisor,
                    Picture = this.Picture,
                    Date = DateTime.Now,
                    SupervisorLocation = new PositionModel
                    {
                        Longitude = this.Longitude,
                        Latitude = this.Latitude
                    }
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit App", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit App", response.Message, "Ok");
                    return;
                }
                VisitsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Visit App", response.Message, "Ok");
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("visit/" + id, new VisitModel
                {
                    Id = id,
                    Supervisor = this.Supervisor,
                    Picture = this.Picture,
                    Date = this.Date,
                    SupervisorLocation = new PositionModel
                    {
                        Longitude = this.Longitude,
                        Latitude = this.Latitude
                    }
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit App", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Visit App", response.Message, "Ok");
                    return;
                }
                VisitsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Visit App", response.Message, "Ok");
            }

            IsBusy = false;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            Picture = await new ImageService().ConvertImageFileToBase64(file.Path);

        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccionar fotografías no soportada", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            Picture = await new ImageService().ConvertImageFileToBase64(file.Path);
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }

        private async void CancelAction()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }

}
