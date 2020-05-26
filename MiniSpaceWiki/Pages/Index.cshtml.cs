using System;
using System.Net;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Cloud.Translation.V2;

namespace MiniSpaceWiki.Pages
{
    public class IndexModel : PageModel
    {
        public class APOD
        {
            public string explanation;
            public string name;
            public string img_url;
            public string reqAPOD = "https://api.nasa.gov/planetary/apod?api_key=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9";
        }

        public APOD apod = new APOD();
        public string TranslateText(string _text)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TranslationClient client = TranslationClient.Create();
            var response = client.TranslateText(
                text: _text,
                targetLanguage: "ru",  // Russian
                sourceLanguage: "en");  // English
            return response.TranslatedText;
        }
        public void SetAPOD()
        {
            using (var webClient = new WebClient())
            {
                string resp = webClient.DownloadString(apod.reqAPOD);
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(resp);
                apod.img_url = dict["url"];
                apod.name = dict["title"];
                apod.explanation = dict["explanation"];
            }
        }
        public void OnGet()
        {
            SetAPOD();
        }
    }
}