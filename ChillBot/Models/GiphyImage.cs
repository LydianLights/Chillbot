using System;
using System.Collections.Generic;
using System.Text;

namespace ChillBot.Models
{
    public class GiphyImage
    {
        public string Url { get; set; }
        public string Image_Url { get; set; }
        public string Title { get; set; }
        public DateTime Create_DateTime { get; set; }
        public string Username { get; set; }
    }
}
