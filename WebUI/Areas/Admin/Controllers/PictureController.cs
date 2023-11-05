using Business.Abstract;
using Core.Helper;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class PictureController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IPictureService _pictureService;
        public PictureController(IWebHostEnvironment env, IPictureService pictureService)
        {
            _env = env;
            _pictureService = pictureService;
        }

        public async Task<JsonResult> UploadPictures(List<IFormFile> PhotoUrls)
        {
            return Json(await PhotoUrls.SaveFileRangeAsync(_env.WebRootPath));
        }
        
        [HttpPost]
        public async Task<JsonResult> RemovePicture(string PhotoUrl)
        {
            await _pictureService.RemoveProductPictureAsync(PhotoUrl);
            var fileName = Path.GetFileName(PhotoUrl);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Json("");
        }
    }
}
