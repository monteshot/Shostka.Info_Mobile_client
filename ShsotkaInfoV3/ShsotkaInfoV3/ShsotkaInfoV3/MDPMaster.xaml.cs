using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ShsotkaInfoV3.Resx;
//using ShostkaInfo.ViewModels;
using ShsotkaInfoV3.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShsotkaInfoV3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDPMaster : ContentPage
    {
        public ListView ListView;

        public MDPMaster()
        {
            InitializeComponent();

            BindingContext = new MDPMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MDPMasterViewModel : BaseViewModel, INotifyPropertyChanged
        {
            public Command LoadItemsCommand { get; set; }
            public ObservableCollection<MDPMenuItem> MenuItems { get; set; }
            public string Temperature { get; set; }
            public string CityName { get; set; }

            public MDPMasterViewModel()
            {
                LoadItemsCommand = new Command(async () => await ExecuteLoadWeatherCommand());
                Temperature = "-273°C";
                CityName = "Shostka";
                MenuItems = new ObservableCollection<MDPMenuItem>(new[]
                {
                    new MDPMenuItem { Id = 0, Title = Resource.NewsLabel },
                    new MDPMenuItem { Id = 3, Title = Resource.ProfileLabel },
                    new MDPMenuItem { Id = 2, Title = Resource.SettingsLabel },

                    new MDPMenuItem { Id = 1, Title = Resource.AboutApp }

                }); ;
                Task t = ExecuteLoadWeatherCommand();
            }
            async Task ExecuteLoadWeatherCommand()
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                try
                {

                    var items = await WeatherStore.LoadDetailItemData(); //inf loading 
                    string[] _Temperature = items.Forecast.Time[0].Temperature.Value.Split(new char[] { '.' });
                    this.Temperature = $"{_Temperature[0]}°C";
                    if (items.Forecast.Time[0].Clouds.Value != items.Forecast.Time[0].Symbol.WeatherCond)
                    {
                        this.CityName =
                            $"{Resource.NowInShostka} {items.Forecast.Time[0].Clouds.Value} и {items.Forecast.Time[0].Symbol.WeatherCond}";
                    }
                    else
                    {
                        this.CityName =
                            $"{Resource.NowInShostka} {items.Forecast.Time[0].Clouds.Value}";

                    }

                    OnPropertyChanged("");
                }
                catch (Exception ex)
                {
                    this.CityName = $"{Resource.UnableToUpdateWeather}";
                }
                finally
                {
                    IsBusy = false;

                }
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}