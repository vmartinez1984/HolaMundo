using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;
using HolaMundo.FirebaseStorage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace HolaMundo.FirebaseStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string authDomain = "archivos-54624.firebaseapp.com";
        string apikey = "AIzaSyAEnLgwQe4B-zwq-WYx0IMYHIYhcHyCkzw";
        string email = "vmartinez@gmail.com";
        string password = "123456";
        string token;
        string rutaDelStorage = "archivos-54624.appspot.com";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(UploadModel upload)
        {
           await ObtenerToken();
            var task = new Firebase.Storage.FirebaseStorage(
                    rutaDelStorage,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token),
                        ThrowOnCancel = true
                    }
                )
                    .Child("Imagenes")
                    .Child(upload.FormFile.FileName)
                    .PutAsync(upload.FormFile.OpenReadStream());

            var urlDescarga = await task;

            return RedirectToAction("Index");
        }

        private async Task ObtenerToken()
        {
            var client = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = apikey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });

            var credenciales = await client.SignInWithEmailAndPasswordAsync(email, password);
            token = await credenciales.User.GetIdTokenAsync();
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