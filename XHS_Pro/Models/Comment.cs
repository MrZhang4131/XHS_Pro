namespace XHS_Pro.Models
{
    public class Comment
    {
        public DateTime created {  get; set; }
        public DateTime updated { get; set; }

        public int Id { get; set; }
        public int userid { get; set; }
        public int noteid { get; set; }
        public string? comment { get; set; }
        public string? username { get; set; }
        public int deleted { get; set; }
        public string? headphoto { get; set; }
    }
}
