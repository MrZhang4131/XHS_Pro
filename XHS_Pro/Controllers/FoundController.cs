using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;

namespace XHS_Pro.Controllers
{
    public class FoundController : Controller
    {
        private XHS_ProContext context;
        public FoundController(XHS_ProContext context)
        {
            this.context = context;
        }
        public class FoundRes
        {
            public Note? note { get; set; }
            public User? user { get; set; }
        }
        public class AllRes
        {
            public List<FoundRes>? Suggest { get; set; }
            public List<FoundRes>? AllList { get; set; }
        }
        public void Add(int id)
        {
            var result = context.Note.FirstOrDefault(p => p.id == id);
            string[] s = { "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png" };
            result.PictureUrl = s;
            context.SaveChanges();
        }
        public async Task<JsonResult> Index()
        {
            List<FoundRes> founds = new List<FoundRes>();
            var result = await context.Note.ToListAsync();
            foreach( var item in  result )
            {
                founds.Add(new FoundRes()
                {
                    note = item,
                    user = await context.User.FirstOrDefaultAsync(p => p.id == item.userid)
                });
            }
            return Json(new AllRes
            {
                Suggest=founds,
                AllList=founds
            });
        }
        public class InfoRes
        {

            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string? username { get; set; }
            public string? content { get; set; }
            public List<Comment>? comment { get; set; }

            public string[]? pictureurl { get; set; }
            public string? videourl { get; set; }
            public string? tag { get; set; }
            public string? headphoto { get; set; }
            public string? title { get; set; }
            public int collectionnum { get; set; }
            public int praisenum { get; set; }
            public DateTime commentTime { get; set; }

        }

        public async Task<JsonResult> content(int id)
        {
            var result= await context.Note.FirstOrDefaultAsync(p => p.id == id);
            //return Json(result);
            var resultuser = await context.User.FirstOrDefaultAsync(u => u.id == result.userid);

        return Json(new InfoRes
        {
            created = result.created,
            updated = result.updated,
            username=result.username,
            content=result.content,
            comment=await context.Comment.Where(c=>c.noteid==id).ToListAsync(),
            pictureurl=result.PictureUrl,
            tag=result.tag,
            headphoto= resultuser.headphoto,
            title=result.title,
            collectionnum=result.collectionnum,
            praisenum=result.praisenum,
            videourl = result.videourl,
        });
        }
        public async Task<JsonResult> search(string param)
        {
            var result = await context.Note.Where(n => n.content.Contains(param) || n.title.Contains(param) || n.tag.Contains(param)).ToListAsync();
            List<FoundRes> suggest = new List<FoundRes>();
            foreach (var item in result)
            {
                suggest.Add(new FoundRes
                {
                    note=item,
                    user=await context.User.FirstOrDefaultAsync(p=>p.id==item.userid),
                });
            }
            return Json(suggest);
        }
        public async Task<JsonResult> comment(string comment,int noteid,int userid)
        {
            var user= await context.User.FirstOrDefaultAsync(u=>u.id==userid);
            var note= await context.Note.FirstOrDefaultAsync(n=>n.id==noteid);
            await context.AddAsync(new Comment
            {
                created=DateTime.Now,
                userid=userid,
                comment=comment,
                noteid=noteid,
                username=user.username,
                headphoto=user.headphoto,
                deleted=0,
            });
            await context.SaveChangesAsync();
            string str = "保存成功";
            return Json(str);
        }
        public async Task<JsonResult> collection(int userid,int noteid)
        {
            var result = await context.Start.FirstOrDefaultAsync(m=>m.noteid==noteid && m.userid==userid);
            if (result == null)
            {
                await context.Start.AddAsync(new Start
                {
                    userid=userid,
                    noteid=noteid,
                    enable=1
                });
                var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                note.collectionnum += 1;
                await context.SaveChangesAsync();
                return Json("收藏成功");
            }
            else
            {
                if(result.enable == 1)
                {
                    result.enable = 0;
                    var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                    note.collectionnum -= 1;
                    await context.SaveChangesAsync();
                    return Json("取消收藏");
                }
                else
                {
                    result.enable = 1;
                    var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                    note.collectionnum -= 1;
                    await context.SaveChangesAsync();
                    return Json("收藏成功");
                }
            }
            
        }
        public async Task<JsonResult> praise(int userid, int noteid)
        {
            var result = await context.Zan.FirstOrDefaultAsync(m => m.noteid == noteid && m.userid == userid);
            if (result == null)
            {
                await context.Zan.AddAsync(new Zan
                {
                    userid = userid,
                    noteid = noteid,
                    enable = 1
                });
                var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                note.praisenum += 1;
                await context.SaveChangesAsync();
                return Json("点赞成功");
            }
            else
            {
                if (result.enable == 1)
                {
                    result.enable = 0;
                    var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                    note.praisenum -= 1;
                    await context.SaveChangesAsync();
                    return Json("取消点赞");
                }
                else
                {
                    result.enable = 1;
                    var note = await context.Note.FirstOrDefaultAsync(n => n.id == noteid);
                    note.praisenum -= 1;
                    await context.SaveChangesAsync();
                    return Json("点赞成功");
                }
            }

        }

    }
}
