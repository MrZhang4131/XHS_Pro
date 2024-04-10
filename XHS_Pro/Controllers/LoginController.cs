using Microsoft.AspNetCore.Mvc;
using XHS_Pro.Models;
using DeveloperSharp.Redis;
using XHS_Pro.Data;
using XHS_Pro.Tools;
using XHS_Pro.Tool;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace XHS_Pro.Controllers
{
    public class Accept
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? phone { get; set; }
        public string? code { get; set; }
    }
    public class LogRes
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
    public class LoginController : Controller
    {
        private XHS_ProContext context;
        private Token_Fe tokenFe;
        public LoginController(XHS_ProContext context,Token_Fe token) 
        {
            this.tokenFe = token;
            this.context = context;
        }
        public String Index()
        {
            return "Test ok";
        }
        [HttpPost]
        public async Task<JsonResult> log([FromBody] Accept accept)
        {
            
            string sms = RedisHelper.GetStringKey(accept.phone);
            if (sms.Equals(accept.code))
            {
                var result = await context.User.FirstOrDefaultAsync(p => p.username == accept.username && p.password == accept.password);
                if (result!=null)
                {
                    string token = tokenFe.CreateToken(result.usertype);
                    result.updated=DateTime.Now;
                    context.Update(result);
                    await context.SaveChangesAsync();
                    return Json(new LogRes
                    {
                        User = result,
                        Token = token,
                    });
                }
                else
                {
                    return Json(new LogRes
                    {
                        User = null,
                        Token = "账号或密码错误",
                    });
                }
                
            }
            return Json(new LogRes
            {
                User=null,
                Token="验证码错误",
            });
        }
        [HttpPost]
        public IActionResult smsLogin([FromBody] User user)
        {
            RedisHelper.SetStringKey(user.phone, "123456");
            return Ok();
        }
        public class ImgRes
        {
            public IFormFile headphoto { get; set; }
        }
        [HttpPost]
        public  string UploadImage(IFormFile headphoto)
        {
            string currentDirectory = Environment.CurrentDirectory;
            string s = SaveImage(headphoto, currentDirectory+"\\Image\\UserImage");

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
                string uniqueFileName = "ii" + Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // 拼接保存路径
                string filePath = Path.Combine(directoryPath, uniqueFileName);

                // 将图片保存到本地
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return "https://localhost:8002/headphoto?url="+filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("/headphoto")]
        public async Task<IActionResult> ShowImage(string url)
        {

            return PhysicalFile(url, "image/jpeg");
        }
        [HttpPost]
        public async Task<JsonResult> Register([FromBody] User user)
        {
            user.updated= DateTime.Now;
            user.created= DateTime.Now;
            user.usertype = "normal";
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Json("注册成功");
        }
    }
}
