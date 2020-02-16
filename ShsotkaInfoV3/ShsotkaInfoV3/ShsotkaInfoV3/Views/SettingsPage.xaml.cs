using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShsotkaInfoV3.Services;
using ShsotkaInfoV3.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShsotkaInfoV3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, IPageItem
    {
        public int IdPage { get; set; }
        public SettingsPage()
        {
            InitializeComponent();
            IdPage = 2;
            BindingContext = new SettingsViewModel();
        }
    }
}