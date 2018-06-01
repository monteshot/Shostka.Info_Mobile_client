using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShsotkaInfoV3.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShsotkaInfoV3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		    BindingContext = new ProfilePageViewModel();
		   // NavigationPage.SetHasNavigationBar(this,false);
		}
	}
}