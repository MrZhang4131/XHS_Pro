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
            var resultuser = await context.User.FirstOrDefaultAsync(u => u.id == result.userid);

        return Json(new InfoRes
        {
            username=result.username,
            content=result.content,
            comment=await context.Comment.Where(c=>c.noteid==id).ToListAsync(),
            pictureurl=result.PictureUrl,
            tag=result.tag,
            headphoto= resultuser.headphoto,
            title=result.title,
            collectionnum=result.collectionnum,
            praisenum=result.praisenum,

        });
        }
    }
}
