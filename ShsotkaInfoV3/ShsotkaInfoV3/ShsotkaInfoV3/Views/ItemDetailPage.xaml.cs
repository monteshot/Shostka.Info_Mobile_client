using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ShsotkaInfoV3.Models;
using ShsotkaInfoV3.ViewModels;
using ShsotkaInfoV3.Services;

namespace ShsotkaInfoV3.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage//, IPageItem
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            //var item = new Item
            //{
            //    Text = "Item 1",
            //    Description = "This is an item description."
            //};

            //viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}