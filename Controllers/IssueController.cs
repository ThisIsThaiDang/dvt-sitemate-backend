using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace dvt_sitemate_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IssueController : ControllerBase
    {
        const string DatastoreLocation = "Datastores";
        const string fileName = "issue.json";
        [HttpGet(Name = "Read")]
        public IActionResult Read()
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), DatastoreLocation, fileName);
                if (!System.IO.File.Exists(fullPath))
                {
                    return Ok("The file is not exist.");
                }
                using var r = new StreamReader(fullPath);
                return Ok(r.ReadToEnd());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public IActionResult Create()
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), DatastoreLocation, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    return Ok("The file exists. Please use update API.");
                }
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    file?.CopyTo(stream);
                    return Ok("Created");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public IActionResult Update()
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), DatastoreLocation, fileName);
                if (!System.IO.File.Exists(fullPath))
                {
                    return Ok("The file is not exist. Please use create API.");
                }
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    file?.CopyTo(stream);
                    return Ok("Updated");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), DatastoreLocation, fileName);
                if (!System.IO.File.Exists(fullPath))
                {
                    return Ok("The file is not exist.");
                }
                System.IO.File.Delete(fullPath);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
