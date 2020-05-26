using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniSpaceWiki.Pages.DONKI
{
    public class CMEModel : PageModel
    {
        #region JSON Structure
        public class Instrument
        {
            public int id { get; set; }
            public string displayName { get; set; }
        }
        public class EnlilList
        {
            public string modelCompletionTime { get; set; }
            public double au { get; set; }
            public string estimatedShockArrivalTime { get; set; }
            public object estimatedDuration { get; set; }
            public object rmin_re { get; set; }
            public object kp_18 { get; set; }
            public object kp_90 { get; set; }
            public object kp_135 { get; set; }
            public object kp_180 { get; set; }
            public bool isEarthGB { get; set; }
            public string link { get; set; }
            public object impactList { get; set; }
            public List<string> cmeIDs { get; set; }
        }
        public class CmeAnalys
        {
            public string time21_5 { get; set; }
            public object latitude { get; set; }
            public object longitude { get; set; }
            public double halfAngle { get; set; }
            public object speed { get; set; }
            public string type { get; set; }
            public bool isMostAccurate { get; set; }
            public string note { get; set; }
            public int levelOfData { get; set; }
            public string link { get; set; }
            public List<EnlilList> enlilList { get; set; }
        }
        public class LinkedEvent
        {
            public string activityID { get; set; }
        }
        public class RootObject
        {
            public string activityID { get; set; }
            public string catalog { get; set; }
            public string startTime { get; set; }
            public string sourceLocation { get; set; }
            public object activeRegionNum { get; set; }
            public string link { get; set; }
            public string note { get; set; }
            public List<Instrument> instruments { get; set; }
            public List<CmeAnalys> cmeAnalyses { get; set; }
            public List<LinkedEvent> linkedEvents { get; set; }
        }
        #endregion

        public String StartDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public String EndDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public Boolean IsInput { get; set; } = false;


        public List<RootObject> list;

        public void OnGet(String StartDate, String EndDate)
        {
            String req = $"https://api.nasa.gov/DONKI/CME?api_key=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9&startDate={StartDate}&endDate={EndDate}";
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