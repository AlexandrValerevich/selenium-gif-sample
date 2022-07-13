// See https://aka.ms/new-console-template for more information
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using var driver = new ChromeDriver();

driver.Navigate().GoToUrl("http://127.0.0.1:5500/Selenium.App/Index.html");
driver.Manage().Window.Maximize();

// var elementScreenshot = (element as ITakesScreenshot).GetScreenshot();
// // elementScreenshot.SaveAsFile("screenshot_of_element.png");

IWebElement element = driver.FindElement(By.CssSelector("body"));
var elementScreenshot = element as ITakesScreenshot;
IEnumerable<Screenshot> screenshots = CreateScreenshots(elementScreenshot, 15);
IMagickImageCollection gif = CombineScreenshotsToGif(screenshots, 10);

string destFolder = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
await SaveGif(gif, destFolder, "purecss.gif");







static IEnumerable<Screenshot> CreateScreenshots(ITakesScreenshot element, int count)
{
    var result = new List<Screenshot>();
    for (int i = 0; i < count; i++)
    {
        result.Add(element.GetScreenshot());
    }

    return result;
}

static IMagickImageCollection CombineScreenshotsToGif(IEnumerable<Screenshot> screenshots, int animationDelay = 10)
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

static async Task SaveGif(IMagickImageCollection gif, string folder, string gifName)
{
    await gif.WriteAsync(Path.Combine(folder, gifName), MagickFormat.Gif);
}

// Thread.Sleep(3000);
