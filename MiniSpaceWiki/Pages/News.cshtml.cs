using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net;

namespace MiniSpaceWiki.Pages
{
    public class NewsModel : PageModel
    {
        #region JSON Structure
        public class Feed
        {
            public string url { get; set; }
            public string title { get; set; }
            public string link { get; set; }
            public string author { get; set; }
            public string description { get; set; }
            public string image { get; set; }
        }
        public class Enclosure
        {
            public string link { get; set; }
            public string type { get; set; }
        }
        public class Item
        {
            public string title { get; set; }
            public string pubDate { get; set; }
            public string link { get; set; }
            public string guid { get; set; }
            public string author { get; set; }
            public string thumbnail { get; set; }
            public string description { get; set; }
            public string content { get; set; }
            public Enclosure enclosure { get; set; }
            public List<string> categories { get; set; }
        }
        public class RootObject
        {
            public string status { get; set; }
            public Feed feed { get; set; }
            public List<Item> items { get; set; }
        }
        #endregion

        public RootObject news = new RootObject();
        public void OnGet()
        {
            String req = $"https://api.rss2json.com/v1/api.json?rss_url=http://k.img.com.ua/rss/ru/space.xml";
            //String req = $"https://www.spaceweatherlive.com/rss_news.xml";
            using (var webClient = new WebClient())
            {
                String resp = webClient.DownloadString(req);
                news = JsonSerializer.Deserialize<RootObject>(resp);
            }
            foreach (var article in news.items)
            {
                article.thumbnail = article.thumbnail.Replace("190x120", "610x385");
            }
        }
        public string DateAndTime(string input)
        {
            string[] arr = input.Split(" ");
            if (arr[0] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                arr[0] = "Сегодня";
            }
            else if (arr[0] == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"))
            {
                arr[0] = "Вчера";
            }
            else
            {
                string[] arr2 = arr[0].Split("-");
                DateTime dt = new DateTime(Convert.ToInt32(arr2[0]), Convert.ToInt32(arr2[1]), Convert.ToInt32(arr2[2]));
                arr[0] = dt.ToString("M");
            }
            arr[1] = arr[1].Remove(5);
            return arr[0] + " в " + arr[1];
        }
    }
}