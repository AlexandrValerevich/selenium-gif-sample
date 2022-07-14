using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

string url = "http://frontend:80/Index.html";
Uri remoteWebDriver = new("http://localhost:4444");

using var driver = new RemoteWebDriver(remoteWebDriver, new ChromeOptions());

driver.Navigate().GoToUrl(url);
driver.Manage().Window.Maximize();

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
