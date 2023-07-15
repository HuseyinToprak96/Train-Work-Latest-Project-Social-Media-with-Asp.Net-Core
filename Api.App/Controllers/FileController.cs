using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Image()
        {
            var imagesUzantilar = new string[] { "jpg", "jpeg" };
            var file = Request.Form.Files.FirstOrDefault();
            // Check if a file is actually provided
            if (file == null || file.Length == 0)
            {
                return Ok("Images/default.jpg");
            }
            else
            {
                var uzanti = file.FileName.Split(".")[file.FileName.Split('.').Length-1];
                string folder = "";
                if (imagesUzantilar.Contains(uzanti))
                    folder = "Images/";
                else if (uzanti == "mp4")
                    folder = "Video";
                else return Ok();
                folder += Guid.NewGuid().ToString() + file.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                return Ok(folder);
            }

        }
    }
}
