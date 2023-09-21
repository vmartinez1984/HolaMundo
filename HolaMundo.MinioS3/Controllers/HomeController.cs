using HolaMundo.MinioS3.Models;
using Microsoft.AspNetCore.Mvc;
using Minio;
using System.Diagnostics;

namespace HolaMundo.MinioS3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        string url;
        string accessKey;
        string secretKey;
        string bucketName;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this._configuration = configuration;
            //var data = _configuration.GetSection("S3").GetChildren();
            url = "127.0.0.1:9000";
            accessKey = "nPSktppiEAtAwnT3lBN4";
            secretKey = "v1oIvbRwHDREddFvUWp7mVoGhotE6rS7bBUgmRt6";
            bucketName = "entregadomicilio";
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(UploadModel upload)
        {
            var minio = new MinioClient()
                    .WithEndpoint(url)
                    .WithCredentials(accessKey, secretKey)
                    .WithSSL(false)
                    .Build();
            var objectName = upload.FormFile.FileName;            
            var contentType = "application/octet-stream";           

            var beArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
            // Upload a file to bucket.
            var response = await minio.PutObjectAsync(
                new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(upload.FormFile.OpenReadStream())  
                .WithObjectSize(upload.FormFile.Length)
                .WithContentType(contentType)
            );          

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}