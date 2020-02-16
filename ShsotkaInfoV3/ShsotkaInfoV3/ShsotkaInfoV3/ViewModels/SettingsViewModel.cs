using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using RestSharp;
using Xamarin.Forms;
using HtmlAgilityPack;
using ShsotkaInfoV3.Resx;

namespace ShsotkaInfoV3.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public int ItemsTextSize { get; set; }
        public int ItemsImageSize { get; set; }
        public int DetailTextSize { get; set; }
        public Command SaveSettings { get; set; }

        public SettingsViewModel()
        {
            Title = Resource.SettingsLabel;
            IdPage = "2";
            Task t = RequestToken();
            SaveSettings = new Command(ExecuteSaveSettings);
            //ToastNotifier.Notify(Interfaces.ToastNotificationType.Info,"Настройки загружены", "Настройки успешно загружены",TimeSpan.Zero);
            OnPropertyChanged("");
        }

        public void ExecuteSaveSettings()
        {

            ToastNotifier.Notify(Interfaces.ToastNotificationType.Success, Resource.SettingsSaved, Resource.SettingsSavedSuccessfully, TimeSpan.Zero);
            OnPropertyChanged("");

        }

        private IRestResponse postloginResponse;
        public async Task RequestToken()
        {
            ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Auth", Resource.SigningIn, TimeSpan.Zero);
            var loginRequest = new RestRequest($"{restclient.BaseUrl}login", Method.POST);
            loginRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            loginRequest.AddParameter("log", "testuser");
            loginRequest.AddParameter("pwd", "testuser");
            //   loginRequest.
            restclient.FollowRedirects = false;
            restresponse = restclient.Execute(loginRequest);
            loginresponse = restresponse;
            ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Auth", Resource.CookieRecieved, TimeSpan.Zero);
            var postloginRequest = new RestRequest($"{restclient.BaseUrl}profile");
            foreach (var cookie in loginresponse.Cookies)
            {
                postloginRequest.AddCookie(cookie.Name, cookie.Value);
            }

            restclient.FollowRedirects = true;
            restresponse = restclient.Execute(postloginRequest);

            postloginResponse = restresponse;
            await Task.Run((Action)DecodeRestResponse);




        }

        private void DecodeRestResponse()
        {
            //    throw new NotImplementedException();
            //Title
            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(postloginResponse.Content);
            Title = " Настройки пользователя " + htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"profile\"]/div[1]/div/p").InnerText;
            OnPropertyChanged("");


            ToastNotifier.Notify(Interfaces.ToastNotificationType.Success, "Auth", "Вход выполнен", TimeSpan.Zero);
        }
    }
}
