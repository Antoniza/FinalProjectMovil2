//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("FinalProject.Views.AdminViews.UsersPanel.xaml", "Views/AdminViews/UsersPanel.xaml", typeof(global::FinalProject.Views.AdminViews.UserForm))]

namespace FinalProject.Views.AdminViews {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\AdminViews\\UsersPanel.xaml")]
    public partial class UserForm : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.SearchBar SearchBar;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView UsersListView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(UserForm));
            SearchBar = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.SearchBar>(this, "SearchBar");
            UsersListView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "UsersListView");
        }
    }
}
