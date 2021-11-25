using FileUpload.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using FileUpload.Model;

namespace FileUpload.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        //private readonly ILogger<FileController> _logger;
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }


        public IActionResult Index()
        {
            return Ok("File Upload Solution");
        }


        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> FileImport(IFormFile FormFile)
        {
            ILog fileLog = LogManager.GetLogger("FileUpload");


            //var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            var filename = FormFile.FileName.Trim('"');


            fileLog.InfoFormat($"Upload file API Called for file name  {1}", filename);


            //get extension
            string extension = Path.GetExtension(filename);

            if (extension.Equals(".txt") || extension.Equals(".text"))
            {
                //get path
                var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

                //create directory "Uploads" if it doesn't exists
                if (!Directory.Exists(MainPath))
                {
                    Directory.CreateDirectory(MainPath);
                }

                //get file path 
                var filePath = Path.Combine(MainPath, FormFile.FileName);

                using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await FormFile.CopyToAsync(stream);
                }

                var result = await fileService.FileUpload(filePath);

                if (result.Response.StatusCode == MessageStatusCode.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);

            }
            else
            {
                return BadRequest("Invalid file extension");
            }
        }
    }
}
