using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using RestSharp;
using ShsotkaInfoV3.Interfaces;
using Xamarin.Forms;

using ShsotkaInfoV3.Models;
using ShsotkaInfoV3.Services;
using WordPressPCL;
using WordPressPCL.Models;

namespace ShsotkaInfoV3.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public  BaseViewModel()
        {
            wclient = new WordPressClient("http://shostka.info/wp-json");
            restclient = new RestClient("http://shostka.info");
            restclient.CookieContainer = new CookieContainer();
        }
         
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IWeather<WeatherModel> WeatherStore => DependencyService.Get<IWeather<WeatherModel>>() ?? new WeatherDatastore();
        public IAttachmentsDetail<Post> AttachmentsDetail => DependencyService.Get<IAttachmentsDetail<Post>>() ?? new AttachmentsDetailPost();
        bool isBusy = false;
        public WordPressClient wclient;
        public RestClient restclient;
        public IRestResponse restresponse;
        public IRestResponse loginresponse;
        public  IToastNotifier ToastNotifier  = DependencyService.Get<IToastNotifier>();
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool _IsLoading = false;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetProperty(ref _IsLoading, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
