using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase {

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string path) {
            string targetPath;

            if (path == null || path.Length == 0) {
                targetPath = Directory.GetCurrentDirectory();
            }
            else {
                targetPath = path;
            }

            bool folderExists = Directory.Exists(targetPath);
            if (folderExists == false) {
                return NotFound(new {
                    message = "Folder not found",
                    path = targetPath
                });
            }

            string[] folderNames = Directory.GetDirectories(targetPath);
            string[] fileNames = Directory.GetFiles(targetPath);

            List<string> folders = new List<string>();
            foreach (string folder in folderNames) {
                folders.Add(Path.GetFileName(folder));
            }

            List<string> files = new List<string>();
            foreach (string file in fileNames) {
                files.Add(Path.GetFileName(file));
            }

            folders.Sort(StringComparer.OrdinalIgnoreCase);
            files.Sort(StringComparer.OrdinalIgnoreCase);

            return Ok(new {
                message = "API Response",
                path = targetPath,
                folders = folders,
                files = files
            });
        }
    }
}
