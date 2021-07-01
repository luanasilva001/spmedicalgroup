using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using senai.sp_medicals.webApi.ViewModel;
using System;
using System.IO;

namespace senai.sp_medicals.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public ImageUploadController(IWebHostEnvironment webHostEnvironmet)
        {
            _webHostEnvironment = webHostEnvironmet;
        }

        [HttpPost]
        public IActionResult PostPhoto([FromForm] ImageUpload objectFile)
        {
            try
            {
                if(objectFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.files.FileName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return Ok("Uploaded!");
                    }
                }
                    Console.WriteLine("nao upado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return BadRequest("Nao Upado!");
        }
    }
}
