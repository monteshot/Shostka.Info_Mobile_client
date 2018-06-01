using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ShsotkaInfoV3.Models;
using WordPressPCL.Models;
using HtmlAgilityPack;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using ShsotkaInfoV3.Services;
using Xamarin.Forms;

namespace ShsotkaInfoV3.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        // public bool IsLoading { get; set; }
        public Post Item { get; set; }
        public string FrontImage { get; set; }
        public HtmlWebViewSource DecodedContent { get; set; }
        string[] ReadyHtmlArray;
        string ReadyHtml;
        AttachmentsDetailModel AttachmentsDetails;
        public ItemDetailViewModel(Post item = null)
        {



            Title = item?.Title.Rendered;
            Item = item;
            IsLoading = true;
            IsBusy = true;
            OnPropertyChanged("");

            Task t = DecodingItem(item);
            IsBusy = false;
            OnPropertyChanged("");
        }

        async Task DecodingItem(Post item)
        {
            try
            {
                


                await Task.Run(async () =>
                {
                    WebClient web = new WebClient();
                    List<AttachmentsDetailModel> res1 = new List<AttachmentsDetailModel>();
                    HtmlDocument fdoc = new HtmlDocument();
                    var decodedContent = new HtmlWebViewSource();


                    string imageUrl = "http://shostka.info/wp-content/themes/pt-shostka/img/headpiece-red.jpg";
                    string image = "";

                    try
                    {
                        if (item.FeaturedMedia != 0)
                        {
                            AttachmentsDetails = AttachmentsDetail.LoadDetailItemData(item, ModeAttachments.FeaturedMedia).Result;
                            imageUrl = AttachmentsDetails.SourceURL;
                        }

                        if (item.FeaturedMedia == 0)
                        {
                            if (item.Links.Attachment.Count() != 0)
                            {
                                string s = await web.DownloadStringTaskAsync(item.Links.Attachment.ToList()[0].Href);
                                if (s == "[]")
                                {
                                   
                                }
                                else
                                {

                                    res1 = JsonConvert.DeserializeObject<List<AttachmentsDetailModel>>(s);
                                    imageUrl = res1[0].SourceURL;
                                }
                            }

                        }


                        fdoc.LoadHtml($"<html><body>{item.Content.Rendered}</body></html>");



                        if (fdoc.DocumentNode.SelectNodes("/html/body/p[*]/img").Count != 0)
                        {
                            foreach (HtmlNode node in fdoc.DocumentNode.SelectNodes("/html/body/p[*]/img"))
                            {
                                string imgsrc = node.Attributes["src"].Value;
                                node.Attributes.RemoveAll();
                                node.Attributes.Add("src", imgsrc);

                            }


                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Crashes.TrackError(e);
                    }
                    finally
                    {

                        //   var attachments= AttachmentsDetail.LoadDetailItemData(item,ModeAttachments.Attachments);

                        image = $"<div class=\"fimg\"><img src=\"{imageUrl}\"</div>";
                        string FormatedTitle = $"<H2 style=\"color:#999999\">{item.Title.Rendered}</H2>";
                        decodedContent.Html = $"<html><head>" +
                                              $" <style type=\"text/css\">img {{\r\n display: block;\r\n    max-width: 100% !important;\r\n       margin: 0 auto;\r\n    text-align: center;\r\n }} iframe{{\r\n\t width: 99%;\r\n}}</style> </head><body><p>{FormatedTitle}</p><p>{image}</p>{fdoc.DocumentNode.OuterHtml}";
                        DecodedContent = decodedContent;
                        OnPropertyChanged("");
                        IsLoading = false;
                        OnPropertyChanged("");
                    }
                }
                   );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Crashes.TrackError(e);
            }
        }

    }
}
