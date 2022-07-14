using OpenQA.Selenium;
using Selenium.Api.Interfaces;

namespace Selenium.Api.Services;

public class BrowserScreenshotService : IBrowserScreenshotService
{
    private readonly IWebDriver _driver;

    public BrowserScreenshotService(IWebDriver webDriver)
    {
        _driver = webDriver;
    }

    public IEnumerable<Screenshot> CreateScreenshots(Uri url, string elementId, int count)
    {
        _driver.Navigate().GoToUrl(url);
        ITakesScreenshot element = _driver.FindElement(By.CssSelector(elementId)) as ITakesScreenshot;

        var result = new List<Screenshot>();
        for (int i = 0; i < count; i++)
        {
            result.Add(element.GetScreenshot());
        }

        return result;
    }
}