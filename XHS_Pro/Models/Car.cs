namespace XHS_Pro.Models
{
    public class Car
    {
        public DateTime created {  get; set; }
        public DateTime updated { get; set; }

        public int Id { get; set; }
        public int userId { get; set; }
        public int goodsId { get; set; }
        public string? goodsName { get; set; }
        public float price { get; set; }
        public string? picture { get; set;}
        public int count { get; set; }
        public float amount { get; set; }
        public int delete { get; set; }
    }
}
