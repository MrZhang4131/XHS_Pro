namespace XHS_Pro.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public int goodsId { get; set; }
        public int userId { get; set; }
        public string? userName { get; set; }
        public string? picture { get; set; }
        public int count { get; set; }
        public float amount { get; set; }
        public int deleted { get; set; }

    }
}
