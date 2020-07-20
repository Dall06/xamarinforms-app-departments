﻿using AppDepartmentManager.ViewModels;
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
    public partial class VisitsPage : ContentPage
    {
        public VisitsPage()
        {
            InitializeComponent();
            BindingContext = new VisitsViewModel();

        }
    }
}