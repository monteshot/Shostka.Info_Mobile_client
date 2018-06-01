using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShsotkaInfoV3.Interfaces;
using Xamarin.Forms;
[assembly: Dependency(typeof(ShsotkaInfoV3.Droid.DependencyService.ToastMessage))]
namespace ShsotkaInfoV3.Droid.DependencyService
{
   
    public class ToastMessage : IToastNotifier
    {
        public ToastMessage( )
        {
            
        }
        public Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            Toast.MakeText(Forms.Context, description, ToastLength.Short).Show();

            return taskCompletionSource.Task;
        }

        public void HideAll()
        {
        }
    }

   
}