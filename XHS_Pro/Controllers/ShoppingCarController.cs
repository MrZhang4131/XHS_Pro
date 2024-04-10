using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    [Authorize]
    public class ShoppingCarController : Controller
    {
        private XHS_ProContext context;
        public ShoppingCarController(XHS_ProContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> add([FromBody] Car car)
        {
            var goods= await context.Goods.FirstOrDefaultAsync(g=>g.Id==car.goodsId);
            car.created = DateTime.Now;
            car.updated = DateTime.Now;
            car.price = goods.price;
            car.picture = goods.picture;
            car.delete = goods.delete;
            car.amount = car.count * goods.price;

            await context.AddAsync(car);
            await context.SaveChangesAsync();

            return (Json("保存成功"));
        }
    }
}
