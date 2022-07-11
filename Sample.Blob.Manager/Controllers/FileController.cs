using Microsoft.AspNetCore.Mvc;
using Sample.Blob.Manager.Logics;
using Sample.Blob.Manager.Models;

namespace Sample.Blob.Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileManagerLogics _fileManagerLogic;
        public FileController(IFileManagerLogics fileManagerLogic)
        {
            _fileManagerLogic = fileManagerLogic;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] FileModel model)
        {
            if (model.MyFile != null)
            {
               await _fileManagerLogic.Upload(model);
            }

            return Ok();
        }

        [HttpGet]
        [Route("read")]
        public async Task<IActionResult> Read(string fileName)
        {
            var fileData = await _fileManagerLogic.Read(fileName);

            return File(fileData, "application/pdf");
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            var fileData = await _fileManagerLogic.Read(fileName);

            //return File(fileData, "application/octet-stream");
            return new FileContentResult(fileData, "application/octet-stream")
            {
                FileDownloadName = Guid.NewGuid().ToString() + ".pdf"
            };
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string fileName)
        {
            await _fileManagerLogic.Delete(fileName);

            return NoContent();
        }
    }
}
