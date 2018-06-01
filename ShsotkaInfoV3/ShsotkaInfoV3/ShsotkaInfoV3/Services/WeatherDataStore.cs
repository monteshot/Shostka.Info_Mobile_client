using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ShsotkaInfoV3.Models;
using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(ShsotkaInfoV3.Services.WeatherDatastore))]
namespace ShsotkaInfoV3.Services
{
    public class WeatherDatastore : IWeather<WeatherModel>
    {
        WeatherModel items;

        public WeatherDatastore()
        {
            items = new WeatherModel();

            Task t = LoadDetailItemData();
        }

        public async Task<WeatherModel> LoadDetailItemData()
        {
            return await Task.Run(async () =>
            {
                WebClient web = new WebClient();
                string s = await web.DownloadStringTaskAsync("http://api.openweathermap.org/data/2.5/forecast?id=693942&appid=bf68d76e65f3bf23e0aadef41d72d628&mode=xml&units=metric&lang=ru");

                XmlSerializer xml = new XmlSerializer(typeof(WeatherModel));

                WeatherModel res = xml.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(s))) as WeatherModel;
                return res;
            }
            );
        }


    }
}