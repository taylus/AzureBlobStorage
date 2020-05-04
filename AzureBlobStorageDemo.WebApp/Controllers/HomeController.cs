using System.Threading.Tasks;
using AzureBlobStorageDemo.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadToAzure.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IAzureBlobStorageContainer container;

        public HomeController(IAzureBlobStorageContainer container)
        {
            this.container = container;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var blob = await container.UploadFromStreamAsync(file.FileName, stream, file.ContentType);
            return Ok(new { src = blob.Uri });
        }
    }
}