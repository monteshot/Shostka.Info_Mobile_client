using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Linq;
using ShsotkaInfoV3.Models;
using ShsotkaInfoV3.Views;
using WordPressPCL;
using WordPressPCL.Models;
using System.Collections.Generic;
using System.Net;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using ShsotkaInfoV3.Services;
using ShsotkaInfoV3.ViewModels;
using WordPressPCL.Utility;
using System.Collections;
using Plugin.Settings;

namespace ShsotkaInfoV3.ViewModels
{

    //public partial class ModItemClass : Post
    //{


    //}

    public static class EnumerableExtensions
    {


        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(new[] { item });
        }
    }
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Post> Items { get; set; }
        List<Post> _Items = new List<Post>();

        public Command LoadItemsCommand { get; set; }
        public Command LoadMoreItemsCommand { get; set; }
     //   public WordPressClient wclient;
        public static bool LoadMoreButtonVisibility { get; set; }// true - button is disappeared
        public string ItemsTextSize { get; set; }
        public string ItemsImageSize { get; set; }
        public string ImageURL { get; set; }
        string imageUrl = "http://shostka.info/wp-content/themes/pt-shostka/img/headpiece-red.jpg";
        public ItemsViewModel()
        {
            OnPropertyChanged("");
            Title = "Загрузка..";
            OnPropertyChanged("");
            // Title = "Browse";
            // countPage = 1;
            Items = new ObservableCollection<Post>();
            ItemsTextSize = CrossSettings.Current.GetValueOrDefault("ItemsTextSize",25).ToString();
            ItemsImageSize = CrossSettings.Current.GetValueOrDefault("ItemsImageSize", 1000).ToString();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ImageURL = imageUrl;
            LoadMoreButtonVisibility = true;
            LoadMoreItemsCommand = new Command(async () => await ExecuteLoadMoreItemsCommand());
         //   wclient = new WordPressClient("http://shostka.info/wp-json");
            URLImageCollReady += ItemsViewModel_URLImageCollReady;
            OnPropertyChanged("");
        }


        private void ItemsViewModel_URLImageCollReady()
        {
            //if (IsBusy) return;
            //IsBusy = true;
            int counter = 0;

            foreach (var item in Items)
            {

                try
                {
                    //_Items[counter].Links.FeaturedMedia.Add(new HttpsApiWOrgFeaturedmedia { Href = URLImageColl[counter] });
                    // Items[counter].Links.FeaturedMedia.ToList()[0].Href = URLImageColl[counter];
                    if (item.Links.FeaturedMedia != null)


                        // if (string.IsNullOrEmpty(item.Links.FeaturedMedia.ToList()[1].Href))

                        //дублируется коллекция
                        Items[counter].Links.FeaturedMedia = _Items[counter].Links.FeaturedMedia.ConcatSingle(new HttpsApiWOrgFeaturedmedia { Href = URLImageColl[counter] });

                    counter++;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Crashes.TrackError(e);
                    //  throw;
                }
                finally
                {
                 //   ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Images", "Images loaded", TimeSpan.Zero);
                    IsBusy = false;
                    //  LoadMoreButtonVisibility = false;
                }

            }

            ToastNotifier.Notify(Interfaces.ToastNotificationType.Success, "Images", "Картинки загружены. Скоро начнется отображение", TimeSpan.Zero);
            MessagingCenter.Send(this, "ImagesLoaded");
            IsLoading = false;
            //IsBusy = false;
            OnPropertyChanged("");
            //   URLImageColl.Clear();
        }

        int countPage = 2;

        async Task ExecuteLoadMoreItemsCommand()
        {



            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //  Items.Clear();
                Title = $"Загрузка страницы №{countPage}..";
                LoadMoreButtonVisibility = false;
                OnPropertyChanged("");
                var queryBuilder = new PostsQueryBuilder { PerPage = 10, Page = countPage++ };

                var next10post = await wclient.Posts.Query(queryBuilder);
                Task T = GetImageUrlCollection(next10post);

                if (next10post?.Count() != 0)
                {
                    OnPropertyChanged("");
                    _Items.AddRange(next10post.ToList());
                    OnPropertyChanged("");
                    Items = new ObservableCollection<Post>(_Items);
                    OnPropertyChanged("");
                }


                LoadMoreButtonVisibility = true;

                //  TestString = _Items[0].Title.Rendered;
                // return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {

                Title = "ШОСТКА.INFO";
                OnPropertyChanged("");
                IsBusy = false;

            }
        }
        async Task ExecuteLoadItemsCommand()
        {


            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                URLImageColl.Clear();
                countPage = 2;
                Items.Clear();
                _Items.Clear();
                var latest10post = await wclient.Posts.Get();


                _Items = latest10post.ToList();
                ToastNotifier.Notify(Interfaces.ToastNotificationType.Info, "Images", "Загрузка картинок началась", TimeSpan.Zero);
                Task T = GetImageUrlCollection(latest10post);



                Items = new ObservableCollection<Post>(latest10post.ToList());

               
                  LoadMoreButtonVisibility = false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Crashes.TrackError(ex);
                //Items = new ObservableCollection<Post> { Title = new Title { Rendered = "Не удалось загрузить список новостей" } };//{"Не удалось загрузить список новостей"}
            }
            finally
            {
                OnPropertyChanged("");
                Title = "ШОСТКА.INFO";
                OnPropertyChanged("");
                IsBusy = false;
            }
        }
        public delegate void URLImageCollHandler();

        public static event URLImageCollHandler URLImageCollReady;

        public static Collection<string> URLImageColl = new Collection<string>();
        public async Task GetImageUrlCollection(IEnumerable<object> values)
        {
            //   if (IsBusy) return;
            IsBusy = true;
            string s;

            AttachmentsDetailModel res = new AttachmentsDetailModel();
            List<AttachmentsDetailModel> res1 = new List<AttachmentsDetailModel>();
            var item = values as Links;
            WebClient web = new WebClient();
            //Task.Run(Function);

            //async Task<Collection<string>> Function()
            //{
            try
            {
                foreach (Post value in values)
                {
                    // if (value.Links.FeaturedMedia.Count() <= 1)
                    {


                        if (value.Links.FeaturedMedia != null)
                        {
                            s = await web.DownloadStringTaskAsync(value.Links.FeaturedMedia.ToList()[0].Href);

                            res = JsonConvert.DeserializeObject<AttachmentsDetailModel>(s);


                            URLImageColl.Add(res.SourceURL);
                        }

                        //else if (false /*value.FeaturedMedia == 0*/)
                        //{
                        //    if (value.Links.Attachment.Count() != 0)
                        //    {
                        //        s = await web.DownloadStringTaskAsync(value.Links.Attachment.ToList()[0].Href);
                        //        if (s == "[]")
                        //        {
                        //            continue;
                        //        }

                        //        res1 = JsonConvert.DeserializeObject<List<AttachmentsDetailModel>>(s);
                        //        URLImageColl.Add(res1[0].SourceURL);
                        //    }

                        //}
                        else
                        {
                            res.SourceURL = imageUrl;
                            URLImageColl.Add(res.SourceURL);
                        }
                    }
                }

                //   return URLImageColl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Crashes.TrackError(e);
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("");
                URLImageCollReady?.Invoke(); //.BeginInvoke(null,null);
            }
            //return URLImageColl;
        }

        private void AsyncCallbackComplete(IAsyncResult ar)
        {
            MessagingCenter.Send(this, "ImagesLoaded");
            OnPropertyChanged("");
            // throw new NotImplementedException();
        }
    }
}