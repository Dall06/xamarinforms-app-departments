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
    public class DepartmentFormViewModel : BaseViewModel
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

        ImageSource imageSource_;
        public ImageSource ImageSource_
        {
            get => imageSource_;
            set => SetProperty(ref imageSource_, value);
        }

        /*********************CONSTRUCTORS***********************/
        public DepartmentFormViewModel()
        {
            DptoSel = new DepartmentModel();
        }

        public DepartmentFormViewModel(DepartmentModel dpto)
        {
            id = dpto.Id;
            Name = dpto.Name;
            Picture = dpto.Picture;
            Longitude = dpto.Location.Longitude;
            Latitude = dpto.Location.Latitude;


           
        }

        /*********************ACTIONS***********************/
        private async void SaveAction()
        {
            IsBusy = true;

            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("department", new DepartmentModel
                {
                    Name = this.Name,
                    Picture = this.Picture,
                    Location = new PositionModel
                    {
                        Longitude = this.Longitude,
                        Latitude = this.Latitude
                    }
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto App", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto App", response.Message, "Ok");
                    return;
                }
                DepartmentsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Dpto App", response.Message, "Ok");
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("department", new DepartmentModel
                {
                    Id = id,
                    Name = this.Name,
                    Picture = this.Picture,
                    Location = new PositionModel
                    {
                        Longitude = this.Longitude,
                        Latitude = this.Latitude
                    }
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto App", "Error", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Dpto App", response.Message, "Ok");
                    return;
                }
                DepartmentsViewModel.GetInstance().RefreshDrivers();
                await Application.Current.MainPage.DisplayAlert("Dpto App", response.Message, "Ok");
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

