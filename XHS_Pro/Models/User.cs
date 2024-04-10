using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace XHS_Pro.Models
{
    public class User
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }

        public string? phone { get; set; }
        public string? headphoto { get; set; }
        public int gender { get; set; }
        public int age { get; set; }
        public string? introduction { get; set; }
        public string? usertype { get; set; }
    }
}
