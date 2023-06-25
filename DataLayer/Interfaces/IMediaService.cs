using CloudinaryDotNet.Actions;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IMediaService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile image);
        Task<VideoUploadResult> AddVideoAsync(IFormFile video);
        Task<DeletionResult> DeleteMediaAsync(string url);
    }
}
