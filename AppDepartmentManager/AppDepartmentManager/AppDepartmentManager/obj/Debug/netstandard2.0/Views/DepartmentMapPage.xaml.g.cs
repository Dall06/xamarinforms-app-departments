//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("AppDepartmentManager.Views.DepartmentMapPage.xaml", "Views/DepartmentMapPage.xaml", typeof(global::AppDepartmentManager.Views.DepartmentMapPage))]

namespace AppDepartmentManager.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\DepartmentMapPage.xaml")]
    public partial class DepartmentMapPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::AppDepartmentManager.Renderers.CustomMap MapDepartment;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label Title;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(DepartmentMapPage));
            MapDepartment = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::AppDepartmentManager.Renderers.CustomMap>(this, "MapDepartment");
            Title = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "Title");
        }
    }
}
