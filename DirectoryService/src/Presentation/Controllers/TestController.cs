using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> TestGet()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        return Ok();
    }
}