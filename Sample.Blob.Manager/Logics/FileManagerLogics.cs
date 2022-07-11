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

        public BlobClient GetBlobClient(string containerName, string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);

             return blobContainer.GetBlobClient(fileName); // é bom colocar o timestamp no final do arquivo
        }

        public async Task Upload(FileModel model)
        {
            var blobClient = GetBlobClient("upload-file", model.MyFile.FileName); // é bom colocar o timestamp no final do arquivo

            await blobClient.UploadAsync(model.MyFile.OpenReadStream());

            // se não quiser que o arquivo com o mesmo nome seja sobrescrito utilizar desta forma abaixo:
            // await blobClient.UploadAsync(model.MyFile.OpenReadStream());
        }

        public async Task<byte[]> Read(string fileName)
        {
            var blobClient = GetBlobClient("upload-file", fileName);

            var fileDownloaded = await blobClient.DownloadAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                await fileDownloaded.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task Delete(string fileName)
        {
            var blobClient = GetBlobClient("upload-file", fileName);

            await blobClient.DeleteAsync();
        }
    }
}
