using Microsoft.AspNetCore.Mvc;

namespace StackIndex.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    [HttpGet()]
    public ActionResult<string> GetPresignedUrlAsync()
    {
        return "";
    }
}
