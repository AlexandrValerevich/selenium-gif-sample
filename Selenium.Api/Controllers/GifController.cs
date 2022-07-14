using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using Selenium.Api.Contracts.V1.Requests;
using Selenium.Api.Interfaces;

namespace Selenium.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GifController : ControllerBase
{
    private readonly IGifService _gifService;
    private readonly IBrowserScreenshotService _screenshotService;

    public GifController(IGifService gifService,
                         IBrowserScreenshotService screenshotService)
    {
        _gifService = gifService;
        _screenshotService = screenshotService;
    }

    [HttpGet("helthcheck")]
    public IActionResult HelthCheck()
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Get([FromQuery] CreateGifRequest request)
    {
        IEnumerable<Screenshot> screenshots = _screenshotService.CreateScreenshots(new Uri(request.Url),
                                                               request.ElementSelector,
                                                               request.ScreenshotsCount);

        IMagickImageCollection gif = _gifService.CombineScreenshotsToGif(screenshots);
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "cool.gif");
        gif.Write(fileName, MagickFormat.Gif);

        return File(System.IO.File.Open(fileName, FileMode.Open), "image/gif");
    }
}
