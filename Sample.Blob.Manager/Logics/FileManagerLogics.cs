using Azure.Storage.Blobs;
using Sample.Blob.Manager.Models;

namespace Sample.Blob.Manager.Logics
{
    public class FileManagerLogics : IFileManagerLogics
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileManagerLogics(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(FileModel model)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("upload-file");

            var blobClient = blobContainer.GetBlobClient(model.MyFile.FileName); // é bom colocar o timestamp no final do arquivo
            await blobClient.UploadAsync(model.MyFile.OpenReadStream());

            // se não quiser que o arquivo com o mesmo nome seja sobrescrito utilizar desta forma abaixo:
            // await blobClient.UploadAsync(model.MyFile.OpenReadStream());
        }

        public async Task<byte[]> Read(string filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("upload-file");

            var blobClient = blobContainer.GetBlobClient(filename); // é bom colocar o timestamp no final do arquivo

            var fileDownloaded = await blobClient.DownloadAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                await fileDownloaded.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
