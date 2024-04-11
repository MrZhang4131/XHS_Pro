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
        public async Task<JsonResult> shopcar(int userid)
        {
            var reslut = await context.Car.Where(c=>c.userId==userid && c.delete==0).ToListAsync();
            return Json(reslut);
        }
        public async Task<JsonResult> addCount(int id,int count)
        {
            var car = await context.Car.FirstOrDefaultAsync(c=>c.Id==id);
            car.count += count;
            car.amount = car.count * car.price;
            car.updated = DateTime.Now;
            context.Car.Update(car);
            await context.SaveChangesAsync();
            return Json("商品数量加"+ count.ToString());
        }
        public async Task<JsonResult> reduceCount(int id, int count)
        {
            var car = await context.Car.FirstOrDefaultAsync(c => c.Id == id);
            car.count += count;
            car.amount = car.count * car.price;
            car.updated = DateTime.Now;
            context.Car.Update(car);
            await context.SaveChangesAsync();
            return Json("商品数量减" + count.ToString());
        }
        public async Task<JsonResult> remove(int id)
        {
            var car = await context.Car.FirstOrDefaultAsync(c => c.Id == id);
            car.delete = 1;
            car.updated = DateTime.Now;
            context.Car.Update(car);
            await context.SaveChangesAsync();
            return Json("删除成功");
        }
        
        public async Task<JsonResult> settlement([FromQuery(Name = "id[]")] List<int> id)
        {
            foreach (var item in id)
            {
                var car = await context.Car.FirstOrDefaultAsync(c => c.Id == item);
                car.delete = 1;
                car.updated = DateTime.Now;
                var user=await context.User.FirstOrDefaultAsync(u => u.id == car.userId);
                await context.Order.AddAsync(new Order
                {
                    created = DateTime.Now,
                    updated = DateTime.Now,
                    goodsId = car.goodsId,
                    userId = car.userId,
                    userName = user.username,
                    picture = car.picture,
                    count = car.count,
                    amount = car.amount,
                    deleted = 0,
                });
                context.Car.Update(car);
                await context.SaveChangesAsync();
            }
            return Json("结算成功");
        }
    }
}
