using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ShsotkaInfoV3.Models;
using WordPressPCL.Models;

[assembly: Xamarin.Forms.Dependency(typeof(ShsotkaInfoV3.Services.AttachmentsDetailPost))]
namespace ShsotkaInfoV3.Services
{
    public enum ModeAttachments
    {
        FeaturedMedia, Attachments
    }
    public class AttachmentsDetailPost : IAttachmentsDetail<Post>
    {
        AttachmentsDetailModel items;
        public Post post { get; set; }
        public string s;
        private ModeAttachments localmode;
        public AttachmentsDetailPost()
        {
            items = new AttachmentsDetailModel();

            Task t = LoadDetailItemData(post, localmode);
        }

        public async Task<AttachmentsDetailModel> LoadDetailItemData(Post item, ModeAttachments mode)
        {
            return await Task.Run(async () =>
            {
                WebClient web = new WebClient();
                if (mode == ModeAttachments.Attachments)
                {
                    s = await web.DownloadStringTaskAsync(item.Links.Attachment.ToString());
                }
                else
                {
                    s = await web.DownloadStringTaskAsync(item.Links.FeaturedMedia.ToList()[0].Href);
                }

                
                // XmlSerializer xml = new XmlSerializer(typeof(AttachmentsDetailModel));

                AttachmentsDetailModel res = JsonConvert.DeserializeObject<AttachmentsDetailModel>(s); //= xml.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(s))) as AttachmentsDetailModel;
                return res;
            }
            );
        }


    }
}