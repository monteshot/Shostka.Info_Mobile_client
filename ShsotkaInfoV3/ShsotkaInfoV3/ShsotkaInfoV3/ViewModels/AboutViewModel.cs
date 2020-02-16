using System;
using System.Windows.Input;
using ShsotkaInfoV3.Resx;
using Xamarin.Forms;

namespace ShsotkaInfoV3.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            //Title = "О приложении";
            Title = Resource.AboutApp;
            IdPage ="1";
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://m.me/monteshot")));
            Version = Environment.Version.ToString();
            Site = Resource.ShostkaInfo;
            OnPropertyChanged("");
        }

        public string Version { get; set; }
        public string Site { get; set; }
        public ICommand OpenWebCommand { get; }
    }
}