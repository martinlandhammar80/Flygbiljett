using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;

namespace GuitestVing
{
    public class Helpers
    {
        public object ScrollToTop(EdgeDriver driver)
        {
            var scroll = ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");
            return scroll;
        }

        public IWebElement WaitForElement(EdgeDriver driver, string xpath)
        {
            var webDriver = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var wait = webDriver.Until(ExpectedConditions.ElementIsVisible(By.XPath($"{xpath}")));

            return wait;
        }

        public string TrimHtml(string text)
        {
            var trimmedText = Regex.Replace(text, @"&nbsp;", "").Trim();
            return trimmedText;
        }
    }
}
