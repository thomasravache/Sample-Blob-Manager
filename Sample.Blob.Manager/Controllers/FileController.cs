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
    }
}
