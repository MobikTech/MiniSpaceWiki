using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniSpaceWiki.Pages.DONKI
{
    public class GSTModel : PageModel
    {
        #region JSON Structure
        public class AllKpIndex
        {
            public string observedTime { get; set; }
            public int kpIndex { get; set; }
            public string source { get; set; }
        }

        public class LinkedEvent
        {
            public string activityID { get; set; }
        }

        public class RootObject
        {
            public string gstID { get; set; }
            public string startTime { get; set; }
            public List<AllKpIndex> allKpIndex { get; set; }
            public List<LinkedEvent> linkedEvents { get; set; }
            public string link { get; set; }
        }
        #endregion
        public String CurrentDate { get; } = DateTime.Now.ToString("yyyy-MM-dd");
        public String StartDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public String EndDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public Boolean IsInput { get; set; } = false;

        public List<RootObject> list;


        public void OnGet(String StartDate,String EndDate)
        {
            String req = $"https://api.nasa.gov/DONKI/GST?api_key=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9&startDate={StartDate}&endDate={EndDate}";
            if (StartDate == null && EndDate == null)
            {
                return;
            }
            else
            {
                IsInput = true;
                this.StartDate = StartDate;
                this.EndDate = EndDate;

                using (var webClient = new WebClient())
                {
                    String resp = webClient.DownloadString(req);
                    if (resp != "")
                    {
                        list = JsonSerializer.Deserialize<List<RootObject>>(resp);
                    }
                }
            }
            
        }
    }
}