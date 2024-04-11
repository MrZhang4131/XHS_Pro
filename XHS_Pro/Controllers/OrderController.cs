using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private XHS_ProContext context;
        public OrderController(XHS_ProContext context)
        {
            this.context = context;
        }
        public async Task<JsonResult> Index(int userid)
        {
            var result = await context.Order.Where(o=>o.userId==userid).ToListAsync();
            return Json(result);
        }
        public async Task<JsonResult> comment(int goodsId,int userId,string comment)
        {
            var user =await context.User.FirstOrDefaultAsync(u=>u.id==userId);
            await context.Comment.AddAsync(new Comment { 
                created=DateTime.Now,
                updated=DateTime.Now,
                userid=userId,
                comment=comment,
                goodsid=goodsId,
                username=user.username,
                headphoto=user.headphoto,
                deleted=0,
            });
            await context.SaveChangesAsync();
            return Json("评论成功");
        }
    }
}
