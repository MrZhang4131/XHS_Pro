using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XHS_Pro.Data;
using XHS_Pro.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XHS_Pro.Controllers
{
    public class ReleaseController : Controller
    {
        private XHS_ProContext context;
        public ReleaseController(XHS_ProContext context)
        {
            this.context = context;
        }
        ////public async Task<IActionResult> Index()
        ////{
        ////    return View();
        ////}
        public class VideoNote
        {
            public string? title { get; set; }
            public string? content { get; set; }
            public string? surfacePicture { get; set; }
            public string? tag { get; set; }
            public int userid { get; set; }
            public string? videourl { get; set; }
        }
        public class PicNote
        {
            public string? content { get; set; }
            public string[]? pictureurl { get; set; }
            public string? surfacePicture { get; set; }
            public string? tag { get; set; }
            public string? title { get; set; }
            public int userid { get; set; }
        }
        public async Task<JsonResult> picturenote([FromBody] PicNote picNote)
        {
            var user = await context.User.FirstOrDefaultAsync(p=>p.id==picNote.userid);
            await context.AddAsync(new Note
            {
                created=DateTime.Now,
                updated=DateTime.Now,
                userid=user.id,
                username=user.username,
                content=picNote.content, 
                surfacePicture=picNote.surfacePicture,
                tag=picNote.tag,
                title=picNote.title,
                PictureUrl=picNote.pictureurl,
                collectionnum=0,
                praisenum=0,
            });
            await context.SaveChangesAsync();
            return Json("上传成功");

        }
        public async Task<JsonResult> note([FromBody] VideoNote videoNote)
        {
            var user = await context.User.FirstOrDefaultAsync(p => p.id == videoNote.userid);
            
            await context.AddAsync(new Note
            {
                created = DateTime.Now,
                updated = DateTime.Now,
                userid = user.id,
                username = user.username,
                content = videoNote.content,
                surfacePicture = videoNote.surfacePicture,
                tag = videoNote.tag,
                videourl = videoNote.videourl,
                PictureUrl = [],
                title = videoNote.title,
                collectionnum = 0,
                praisenum = 0,
        });
            await context.SaveChangesAsync();
            return Json("上传成功");
        }
        [Route("/video")]
        public async Task<IActionResult> ShowVideo(string url)
        {

            return  PhysicalFile(url, "video/mp4");
        }
        public string uploadPicture(IFormFile headphoto)
        {
                string currentDirectory = Environment.CurrentDirectory;
                string s = SaveImage(headphoto, currentDirectory + "\\Image\\NoteImage");
                return s;
        }
        private string SaveImage(IFormFile image, string directoryPath)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return "未上传图片";
                }

                // 确保目标目录存在
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // 生成唯一的文件名，可以使用 Guid 或者其他方式
                string uniqueFileName = "vv"+Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // 拼接保存路径
                string filePath = Path.Combine(directoryPath, uniqueFileName);

                // 将图片保存到本地
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return "https://localhost:8002/headphoto?url=" + filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UploadVideo(IFormFile video)
        {
            string currentDirectory = Environment.CurrentDirectory;
            string videoUrl = SaveVideo(video, currentDirectory + "\\Video");
            return videoUrl;
        }

        private string SaveVideo(IFormFile video, string directoryPath)
        {
            try
            {
                if (video == null || video.Length == 0)
                {
                    return "未上传视频";
                }

                // 确保目标目录存在
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // 生成唯一的文件名，可以使用 Guid 或者其他方式
                string uniqueFileName = "vv"+Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);

                // 拼接保存路径
                string filePath = Path.Combine(directoryPath, uniqueFileName);

                // 将视频保存到本地
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    video.CopyTo(stream);
                }

                // 返回视频的 URL
                return "https://localhost:8002/video?url=" + filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
