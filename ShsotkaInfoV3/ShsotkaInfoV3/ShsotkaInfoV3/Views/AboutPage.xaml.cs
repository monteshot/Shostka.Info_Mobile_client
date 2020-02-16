using ShsotkaInfoV3.Services;
using ShsotkaInfoV3.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShsotkaInfoV3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage, IPageItem
    {
        public int IdPage { get; set; }
        public AboutPage()
        {
            InitializeComponent();
            IdPage = 1;
            BindingContext = new AboutViewModel();
        }
    }
}