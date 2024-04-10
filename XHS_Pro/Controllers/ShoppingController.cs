using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    public class ShoppingController : Controller
    {
        private XHS_ProContext context;
        public ShoppingController(XHS_ProContext context)
        {
            this.context = context;
        }
        public class ShopRes
        {
            public List<Goods> goods {  get; set; }
            public List<Goods> suggest { get; set; }
        }
        public async Task<JsonResult> Index()
        {
            var result = await context.Goods.ToListAsync();
            return Json(new ShopRes
            {
                goods = result,
                suggest = result,
            });
        }
        public class ContentRes
        {
            public Goods goods { get; set; }
            public List<Comment> goodsComment { get; set; }
        }
        public async Task<JsonResult> content(int id)
        {
            return Json(new ContentRes
            {
                goods= await context.Goods.FirstOrDefaultAsync(p=>p.Id==id),
                goodsComment = await context.Comment.Where(p=>p.goodsid==id).ToListAsync(),
            });
        }
        public async Task<JsonResult> search(string param)
        {
            var result = await context.Goods.Where(n => n.goodsName.Contains(param) || n.goodsContent.Contains(param) || n.goodsTag.Contains(param)).ToListAsync();
            return Json(result);
        }
    }
}
