using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net;

namespace MiniSpaceWiki.Pages.DONKI
{
    public class FLRModel : PageModel
    {
        #region JSON Structure
        public class Instrument
        {
            public int id { get; set; }
            public string displayName { get; set; }
        }
        public class LinkedEvent
        {
            public string activityID { get; set; }
        }
        public class RootObject
        {
            public string flrID { get; set; }
            public List<Instrument> instruments { get; set; }
            public string beginTime { get; set; }
            public string peakTime { get; set; }
            public string endTime { get; set; }
            public string classType { get; set; }
            public string sourceLocation { get; set; }
            public int? activeRegionNum { get; set; }
            public List<LinkedEvent> linkedEvents { get; set; }
            public string link { get; set; }
        }
        #endregion
        public String StartDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public String EndDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public Boolean IsInput { get; set; } = false;

        public List<RootObject> list;

        public void OnGet(String StartDate, String EndDate)
        {
            String req = $"https://api.nasa.gov/DONKI/FLR?api_key=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9&startDate={StartDate}&endDate={EndDate}";
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