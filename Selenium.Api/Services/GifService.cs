using ImageMagick;
using OpenQA.Selenium;
using Selenium.Api.Interfaces;

namespace Selenium.Api.Services;

public class GifService : IGifService
{
    public IMagickImageCollection CombineScreenshotsToGif(IEnumerable<Screenshot> screenshots, int animationDelay = 10)
    {
        var images = screenshots.Select(s =>
        {
            var image = new MagickImage(s.AsByteArray)
            {
                AnimationDelay = animationDelay
            };
            return image;
        });

        return new MagickImageCollection(images);
    }
}