using ImageMagick;
using OpenQA.Selenium;

namespace Selenium.Api.Interfaces;

public interface IGifService
{
    IMagickImageCollection CombineScreenshotsToGif(IEnumerable<Screenshot> screenshots, int animationDelay = 10);
}