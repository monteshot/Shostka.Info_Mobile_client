using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ShsotkaInfoV3.Models;
using ShsotkaInfoV3.Views;
using ShsotkaInfoV3.ViewModels;
using WordPressPCL.Models;
using ShsotkaInfoV3.Services;

namespace ShsotkaInfoV3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage, INotifyPropertyChanged, IPageItem
    {
        ItemsViewModel viewModel;
        public int IdPage { get; set; }
        public ItemsPage()
        {
            InitializeComponent();
            IdPage = 0;
            LoadMore.IsVisible = true;
            ItemsListView.PropertyChanged += ItemsListView_PropertyChanged;
            ItemsListView.Refreshing += ItemsListView_Refreshing;
            VisibilyatorAsync(false, 0);
            MessagingCenter.Subscribe<ItemsViewModel>(this, "ImagesLoaded", UpdateListview);
            BindingContext = viewModel = new ItemsViewModel();
            OnPropertyChanged("");

        }

        private void ItemsListView_Refreshing(object sender, EventArgs e)
        {
            LoadMore.IsVisible = true;
            MoreItemIndicator.IsVisible = true; MoreItemIndicator.IsRunning = true;
        }

        private void ItemsListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            LoadMore.IsVisible = true;
            MoreItemIndicator.IsVisible = false; MoreItemIndicator.IsRunning = false;
        }
        int counter = 0;
        public void UpdateListview(ItemsViewModel obj)
        {
            ItemsListView.ItemsSource = obj.Items;
            OnPropertyChanged("");
        }
        private async void VisibilyatorAsync(bool flag, int mode)
        {

            if (mode == 0)
            {
                double opacityListView;
                double opacityIndicator;
                if (flag)
                {
                    opacityIndicator = 1;// true = loading
                    opacityListView = 0;
                }
                else
                {
                    opacityIndicator = 0;
                    opacityListView = 1;
                }

                await ItemIndicator.FadeTo(opacityIndicator, App.AnimationSpeed, Easing.SinIn);
                await ItemsListView.FadeTo(opacityListView, App.AnimationSpeed, Easing.SinIn);
                this.ItemIndicator.IsEnabled = flag; this.ItemIndicator.IsVisible = flag; this.ItemIndicator.IsRunning = flag; this.ItemsListView.IsVisible = !flag;
            }

            if (mode == 1)
            {
                double opacityIndicator;
                if (flag)
                {
                    opacityIndicator = 1;// true = loading

                }
                else
                {
                    opacityIndicator = 0;

                }

                await ItemIndicator.FadeTo(opacityIndicator, App.AnimationSpeed, Easing.SinIn);

                this.ItemIndicator.IsEnabled = flag; this.ItemIndicator.IsVisible = flag; this.ItemIndicator.IsRunning = flag;
            }

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Post;
            if (item == null)
                return;
            //  VisibilyatorAsync(true,0);
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
            VisibilyatorAsync(false, 0);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VisibilyatorAsync(false, 0);
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
            OnPropertyChanged("");
        }



        private void LoadMore_OnClicked(object sender, EventArgs e)
        {
            // VisibilyatorAsync(true,1);

            MoreItemIndicator.IsVisible = true; MoreItemIndicator.IsRunning = true;
            var v = ItemsListView.ItemsSource.Cast<Post>().LastOrDefault();
            ItemsListView.ScrollTo(v, ScrollToPosition.MakeVisible, true);

            // throw new NotImplementedException();

        }
    }
}