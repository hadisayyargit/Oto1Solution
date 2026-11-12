using Microsoft.AspNetCore.Mvc;

namespace Auto1API.Controllers
{
    [Route("api/[controller]")] // حذف api/ برای ساده‌سازی
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _baseAssetsPath;

        public UserController(IWebHostEnvironment env)
        {
            _env = env;
            // مسیر اصلی را روی پوشه wwwroot تنظیم می‌کنیم تا عکس‌ها از طریق URL قابل دسترسی باشند
            // مسیر نهایی در هاست شما: httpdocs/wwwroot/assets
            _baseAssetsPath = Path.Combine(_env.WebRootPath, "assets");
        }

        [HttpGet("sayhello2")]
        public IActionResult SayHello2()
        {
            return Ok("Hello from Server! Connection is working.");
        }


        [HttpPost("upload/{subFolder}")]
        public async Task<IActionResult> UploadFile(string subFolder, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("فایلی انتخاب نشده است.");

            try
            {
                // مسیر فعلی پروژه: .../httpdocs/mysites/auto1api
                var currentDir = Directory.GetCurrentDirectory();

                // رفتن دو سطح به بالا برای رسیدن به httpdocs
                // سطح 1: mysites
                // سطح 2: httpdocs
                var parentDir1 = Directory.GetParent(currentDir);

                /*
                var parentDir2 = parentDir1?.Parent;

                if (parentDir2 == null)
                {
                    return BadRequest("مسیر روت اصلی هاست قابل تشخیص نیست.");
                }

                // ساخت مسیر نهایی: httpdocs/wwwroot/assets/oto1/subFolder
                var targetDirectory = Path.Combine(parentDir2.FullName, "wwwroot", "assets", "oto1", subFolder);
                */

                // ساخت مسیر نهایی: httpdocs/mysites/oto1/assets/subFolder
                var targetDirectory = Path.Combine(parentDir1.FullName, "oto1", "assets",  subFolder);

                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string fileName = file.FileName;
                string filePath = Path.Combine(targetDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // آدرس نسبی برای ذخیره در دیتابیس یا بازگرداندن به کلاینت
                string relativePath = $"/assets/oto1/{subFolder}/{fileName}";

                return Ok(new
                {
                    success = true,
                    fileName = fileName,
                    relativePath = relativePath
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "خطا در آپلود فایل",
                    error = ex.Message
                });
            }
        }



    }
}