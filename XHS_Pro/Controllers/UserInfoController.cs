using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    public class UserInfoController : Controller
    {
        private XHS_ProContext context;
        public UserInfoController(XHS_ProContext context)
        {
            this.context = context;
        }
        public async Task<JsonResult> Index(int userid)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.id == userid);
            return Json(user);
        }
        public class UpRes{
            public string? headphoto {  get; set; }
            public string? username { get; set; }
            public string? introduction {  get; set; }
            public string? password {  get; set; }
            public int gender {  get; set; } 
            public int age { get; set; }

            public int id { get; set; }
        }
        public async Task<JsonResult> update([FromBody] UpRes upRes)
        {
            var user = await context.User.FirstOrDefaultAsync(u=>u.id==upRes.id);
            user.updated=DateTime.Now;
            user.headphoto = upRes.headphoto;
            user.username = upRes.username;
            user.introduction = upRes.introduction;
            user.password = upRes.password;
            user.gender = upRes.gender;
            user.age = upRes.age;
            context.User.Update(user);
            await context.SaveChangesAsync();
            return Json("修改成功");
        }
        public class Praise
        {
            public Note note { get; set; }
            public User user { get; set; }
        }
        public async Task<JsonResult> praise(int userid)
        {
            var praise= await context.Zan.Where(p=>p.userid==userid && p.enable==1).ToListAsync();
            List<Praise> result = new List<Praise>();
            foreach (var item in praise)
            {
                var n = await context.Note.FirstOrDefaultAsync(n => n.id == item.noteid);
                result.Add(new Praise
                {
                    note=n,
                    user=await context.User.FirstOrDefaultAsync(u=>u.id==n.userid),
                });
            }
            return Json(result);
        }
        public async Task<JsonResult> collection(int userid)
        {
            var praise = await context.Start.Where(p => p.userid == userid && p.enable == 1).ToListAsync();
            List<Praise> result = new List<Praise>();
            foreach (var item in praise)
            {
                var n = await context.Note.FirstOrDefaultAsync(n => n.id == item.noteid);
                result.Add(new Praise
                {
                    note = n,
                    user = await context.User.FirstOrDefaultAsync(u => u.id == n.userid),
                });
            }
            return Json(result);
        }
        public async Task<JsonResult> Payouts(int userid)
        {
            var user = await context.User.FirstOrDefaultAsync(u=>u.id== userid);
            user.money += user.returnMoney;
            user.returnMoney = 0;
            await context.SaveChangesAsync();
            return Json("提现成功");
        }
    }
}
