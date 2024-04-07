using Microsoft.AspNetCore.Mvc;

namespace XHS_Pro.Tools
{
    public class MyResponse : IActionResult
    {
        public int StatusCode { get; set; }
        public Object? Data { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new { Data })
            {
                StatusCode = StatusCode
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
