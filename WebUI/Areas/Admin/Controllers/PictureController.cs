using Core.Helper;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class PictureController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public PictureController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<JsonResult> UploadPictures(List<IFormFile> PhotoUrls)
        {
            return Json(await PhotoUrls.SaveFileRangeAsync(_env.WebRootPath));
        }
        [HttpPost]
        public async Task<JsonResult> RemovePicture(string PhotoUrl)
        {
            try
            {
                var filePath = Path.Combine(_env.WebRootPath, PhotoUrl); // Combine the web root path with the relative URL
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Delete the file
                    return Json(new { success = true, message = "Image deleted successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Image not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
