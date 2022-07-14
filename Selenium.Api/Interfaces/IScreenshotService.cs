using OpenQA.Selenium;

namespace Selenium.Api.Interfaces;

public interface IBrowserScreenshotService
{
    IEnumerable<Screenshot> CreateScreenshots(Uri url, string elementSelector, int count);
}