using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace ShsotkaInfoV3.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "О приложении";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://m.me/monteshot")));
            Version = Environment.Version.ToString();
            Site = "ШОСТКА.INFO";

        }

        public string Version { get; set; }
        public string Site { get; set; }
        public ICommand OpenWebCommand { get; }
    }
}