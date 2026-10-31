using Microsoft.AspNetCore.Mvc;

namespace SmartBA.API.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Message = "SmartBA API is running"
        });
    }
}