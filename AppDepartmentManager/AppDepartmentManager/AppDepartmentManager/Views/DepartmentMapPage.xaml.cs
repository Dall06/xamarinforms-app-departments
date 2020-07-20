using AppDepartmentManager.Models;
using AppDepartmentManager.Serivices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppDepartmentManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepartmentMapPage : ContentPage
    {
        public DepartmentMapPage(DepartmentModel department)
        {
            InitializeComponent();

            MapDepartment.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(department.Location.Latitude, department.Location.Longitude),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(department.Picture, department.Id);
            department.Picture = imagePath;

            MapDepartment.Department = department;

            MapDepartment.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = department.Name,
                    Position = new Position(department.Location.Latitude, department.Location.Longitude)
                }
            );

            Title.Text = department.Name;
        }
    }
}