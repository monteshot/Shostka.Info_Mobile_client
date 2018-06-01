using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ShsotkaInfoV3.Views;
using Xamarin.Forms;

namespace ShsotkaInfoV3
{
	public partial class App : Application
	{
	    public static uint AnimationSpeed = 250;
        public App ()
		{
			InitializeComponent();


            MainPage = new MDP();
        }

		protected override void OnStart ()
		{
            AppCenter.Start("android=18f76760-8caa-4feb-a117-d137319a68b2;" , typeof(Analytics), typeof(Crashes));
            // Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
