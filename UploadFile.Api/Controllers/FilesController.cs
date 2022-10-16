using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadFile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilesController(IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
        }

        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                if (file.Length == 0)
                    return BadRequest();

                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fullPath = Path.Combine(path, file.FileName);
                using(var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(file);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
