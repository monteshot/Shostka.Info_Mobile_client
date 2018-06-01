using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using WordPressPCL;
using WordPressPCL.Models;
using Xamarin.Forms;

namespace ShsotkaInfoV3.ViewModels
{
    class ProfilePageViewModel : BaseViewModel
    {
        public HtmlWebViewSource WebContent { get; set; }

        public ProfilePageViewModel()
        {

            Task t = RequestToken();
            BlogsLoadCommand = new Command(async () => await LoadBlogs());

        }
       
        IRestResponse blogsresponse;
        private async Task LoadBlogs()
        {
            ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Auth", "Loading blogs", TimeSpan.Zero);
            var blogsRequest = new RestRequest($"http://shostka.info/profile/?inset=blogs", Method.POST);
            blogsRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            foreach (var cookie in loginresponse.Cookies)
            {
                blogsRequest.AddCookie(cookie.Name, cookie.Value);
            }
            restresponse = restclient.Execute(blogsRequest);
            blogsresponse = restresponse;
        }

        public Command BlogsLoadCommand { get; set; }


        public async Task RequestToken()
        {
            ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Auth", "Signing in", TimeSpan.Zero);
            var loginRequest = new RestRequest($"{restclient.BaseUrl}login", Method.POST);
            loginRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            loginRequest.AddParameter("log", "testuser");
            loginRequest.AddParameter("pwd", "testuser");
            //   loginRequest.
            restclient.FollowRedirects = false;
            restresponse = restclient.Execute(loginRequest);
            loginresponse = restresponse;
            ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Auth", "Cookie recieved", TimeSpan.Zero);
            //  restresponse
            //         await wclient.GetSettings();
            // await wclient.RequestJWToken("testUser", "testuser");

            // var isValidToken = await wclient.IsValidJWToken();


        }
    }
}
