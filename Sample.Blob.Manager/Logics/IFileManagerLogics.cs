using Sample.Blob.Manager.Models;

namespace Sample.Blob.Manager.Logics
{
    public interface IFileManagerLogics
    {
        Task Upload(FileModel model);
        Task<byte[]> Read(string fileName);
        Task Delete(string fileName);
    }
}
