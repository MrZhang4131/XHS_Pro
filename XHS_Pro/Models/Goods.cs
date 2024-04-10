namespace XHS_Pro.Models
{
    public class Goods
    {
        public DateTime created {  get; set; }
        public DateTime updated { get; set; }
        public int Id { get; set; }
        public string? goodsName { get; set; }
        public float price { get; set; }
        public string? goodsContent { get; set; }
        public string? goodsTag { get; set; }
        public string? picture { get; set; }
        public int delete {  get; set; }

    }
}
