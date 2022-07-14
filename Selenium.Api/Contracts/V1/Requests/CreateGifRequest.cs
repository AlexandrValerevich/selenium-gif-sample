namespace Selenium.Api.Contracts.V1.Requests;

public class CreateGifRequest
{
    public string Url { get; set; }
    public string ElementSelector { get; set; }
    public int ScreenshotsCount { get; set; }
}