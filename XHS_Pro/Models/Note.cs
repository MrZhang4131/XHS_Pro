namespace XHS_Pro.Models
{
    public class Note
    {
        public int id {  get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int userid { get; set; }
        public string? username { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? tag { get; set; }
        public string? surfacePicture { get; set; }
        public int praisenum { get; set; }
        public int collectionnum { get; set; }
        public int deleted {  get; set; }
        public string[]? PictureUrl { get; set; }
        public string? videourl { get; set; }



    }
}
