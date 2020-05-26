using System;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniSpaceWiki.Pages
{
    public class MarsRoverModel : PageModel
    {
        #region JSON Structure
        public class Cameras
        {
            public string name { get; set; }
            public string full_name { get; set; }
        }
        public class Camera
        {
            public int id { get; set; }
            public string name { get; set; }
            public int rover_id { get; set; }
            public string full_name { get; set; }
        }
        public class Rover
        {
            public bool show_gallery { get; set; } = false;
            public int id { get; set; }
            public string name { get; set; }
            public string landing_date { get; set; }
            public string launch_date { get; set; }
            public string status { get; set; }
            public int max_sol { get; set; }
            public string max_date { get; set; }
            public int total_photos { get; set; }
            public List<Cameras> cameras { get; set; }
        }
        public class Photo
        {
            public int id { get; set; }
            public int sol { get; set; }
            public Camera camera { get; set; }
            public string img_src { get; set; }
            public string earth_date { get; set; }
            public Rover rover { get; set; }

        }
        public class RootObjectRovers
        {
            public List<Rover> rovers { get; set; }
        }
        public class RootObjectPhotos
        {
            public List<Photo> photos { get; set; }

        }
        #endregion

        public List<Rover> rovers = new List<Rover>();
        public List<Photo> photos = new List<Photo>();


        public String CurrentDate { get; } = DateTime.Now.ToString("yyyy-MM-dd");

        public void SetRovers()
        {
            String req = "https://api.nasa.gov/mars-photos/api/v1/rovers/?API_KEY=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9";
            using (var webClient = new WebClient())
            {
                String resp = webClient.DownloadString(req);
                rovers = JsonSerializer.Deserialize<RootObjectRovers>(resp).rovers;
            }
        }
        public void SetPhotos(string date, string name)
        {
            String req = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{name.ToLower()}/photos?earth_date={date}&api_key=iWTMrwrE6srpoGzZVv75yUhRQM3jFsiJcZqLztw9";
            using (var webClient = new WebClient())
            {
                String resp = webClient.DownloadString(req);
                photos = JsonSerializer.Deserialize<RootObjectPhotos>(resp).photos;
            }
        }
        public void OnGet(string Date,string Name)
        {
            SetRovers();
            if (Date != null && Name != null)
            {
                SetPhotos(Date, Name);
                switch (Name)
                {
                    case "Curiosity":
                        rovers[0].show_gallery = true;
                        break;
                    case "Spirit":
                        rovers[1].show_gallery = true;
                        break;
                    case "Opportunity":
                        rovers[2].show_gallery = true;
                        break;
                }
            }
        }
    }
}