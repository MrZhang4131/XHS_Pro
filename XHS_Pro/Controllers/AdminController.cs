using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    public class AdminController : Controller
    {
        private XHS_ProContext context;
        public AdminController(XHS_ProContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> getnote(int page,int pageSize,int id)
        {
            var records = await context.Note.Where(n=>n.userid==id).Skip((page-1)*pageSize).Take(pageSize).ToArrayAsync();
            return Json(new {records});
        }
        public async Task<JsonResult> deletenote(int noteid)
        {
            var note = await context.Note.FirstOrDefaultAsync(n=>n.id==noteid);
            if(note != null) {
                context.Remove(note);
                await context.SaveChangesAsync();
            }
            var comment = await context.Comment.Where(n=>n.noteid==noteid).ToListAsync();
            if(comment != null)
            {
                context.Remove(comment);
                await context.SaveChangesAsync();
            }
            var zan = await context.Zan.Where(z => z.noteid == noteid).ToListAsync();
            if(zan != null)
            {
                context.Remove(zan);
                await context.SaveChangesAsync();
            }
            var collection = await context.Start.Where(s => s.noteid == noteid).ToListAsync();
            if(collection != null)
            {
                context.Remove(collection);
                await context.SaveChangesAsync();
            }
            return Json("删除成功");
        }
        public async Task<JsonResult> getgoods(int page, int pageSize)
        {
            var records = await context.Goods.ToArrayAsync();
            return Json(new { records });
        }
        public async Task<JsonResult> deletegoods(int id)
        {
            var note = await context.Goods.FirstOrDefaultAsync(n => n.Id == id);
            if (note != null)
            {
                context.Remove(note);
                await context.SaveChangesAsync();
            }

            return Json("删除成功");
        }
    }
}
