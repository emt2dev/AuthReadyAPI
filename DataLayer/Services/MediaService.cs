using AuthReadyAPI.DataLayer.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Policy;

namespace AuthReadyAPI.DataLayer.Services
{
    public class MediaService : IMediaService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        public MediaService(IConfiguration configuration)
        {
            _configuration = configuration;

            var ourAccount = new Account(_configuration["CloudinarySettings:cloud_name"], _configuration["CloudinarySettings:api_key"], _configuration["CloudinarySettings:api_secret"]);
            _cloudinary = new Cloudinary(ourAccount);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile image)
        {
            var beginUpload = new ImageUploadResult();

            if (image.Length > 0)
            {
                using var stream = image.OpenReadStream();
                var continueUpload = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                beginUpload = await _cloudinary.UploadAsync(continueUpload);
            }

            return beginUpload;
        }

        public async Task<VideoUploadResult> AddVideoAsync(IFormFile video)
        {
            var beginUpload = new VideoUploadResult();

            if (video.Length > 0)
            {
                using var stream = video.OpenReadStream();
                var continueUpload = new VideoUploadParams
                {
                    File = new FileDescription(video.FileName, stream),
                    EagerTransforms = new List<Transformation>()
                        {
                            new EagerTransformation().Width(300).Height(300).Crop("pad").AudioCodec("none"),
                            new EagerTransformation().Width(160).Height(100).Crop("crop").Gravity("south").AudioCodec("none")
                        },
                    EagerAsync = true,
                    EagerNotificationUrl = "https://mysite.example.com/my_notification_endpoint"
                };

                beginUpload = await _cloudinary.UploadAsync(continueUpload);
            }

            return beginUpload;
        }

        public async Task<DeletionResult> DeleteMediaAsync(string url)
        {
            var parsedURL = url.Split('/').Last().Split('.')[0];

            var beginDelete = new DeletionParams(parsedURL);

            return await _cloudinary.DestroyAsync(beginDelete);
        }
    }
}
