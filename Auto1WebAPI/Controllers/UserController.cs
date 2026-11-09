using Microsoft.AspNetCore.Mvc;

namespace Auto1API.Controllers
{
    [Route("[controller]")] // حذف api/ برای ساده‌سازی
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

        [HttpPost("upload/{subFolder}")]
        public async Task<IActionResult> UploadFile(string subFolder, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("فایلی انتخاب نشده است.");

            try
            {
                // ۱. ساخت مسیر پوشه مقصد (مثلاً: wwwroot/assets/person)
                string targetDirectory = Path.Combine(_baseAssetsPath, subFolder);

                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // ۲. ساخت نام منحصر به فرد برای فایل (Timestamp + Original Name)
                string fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{file.FileName}";
                string filePath = Path.Combine(targetDirectory, fileName);

                // ۳. ذخیره فایل
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // ۴. تولید مسیر نسبی برای استفاده در موبایل (مثلاً: /assets/person/file.jpg)
                // این مسیر دقیقاً همان چیزی است که در مرورگر کار می‌کند
                string relativePath = $"/assets/{subFolder}/{fileName}";

                return Ok(new
                {
                    success = true,
                    fileName = fileName,
                    relativePath = relativePath
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطای سرور: {ex.Message}");
            }
        }
    }
}