using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace GuitestVing
{
    public class Biljettest
    {
        EdgeDriver driver;
        Helpers helper = new Helpers();
        EmailSender sender = new EmailSender();

        [SetUp]
        public void Setup()
        {
            EdgeOptions options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            options.AddExcludedArguments("enable-automation");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--guest");
            string edgeDriverPath = @"C:\Users\martinl\Downloads\edgedriver_win64";
            driver = new EdgeDriver(edgeDriverPath, options);
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://smalandairport.se/");
            driver.FindElement(By.Id("menu-item-16")).Click();
            driver.FindElement(By.LinkText("Charter")).Click();
            driver.FindElement(By.PartialLinkText("Larnaca")).Click();
            driver.FindElement(By.LinkText("Boka")).Click();

            Thread.Sleep(1000);

            var browserTabs = driver.WindowHandles;
            driver.SwitchTo().Window(browserTabs[1]);
            driver.FindElement(By.XPath("//*[text()='Flyg']")).Click();

            driver.FindElement(By.XPath("//*[text()='Välj resmål']")).Click();

            var e = driver.FindElement(By.XPath("//*[text()='Larnaca']"));
            ((IJavaScriptExecutor)driver)
            .ExecuteScript("arguments[0].scrollIntoView(true);", e);

            e.Click();

            helper.ScrollToTop(driver);

            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[1]/div[3]/div/div[1]/div/div")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[1]/div[3]/div/div[3]/div/div[1]/div/div/div/div[2]/div[1]/button[2]")).Click();

            helper.WaitForElement(driver, "//*[text()='juli 2024']");

            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[1]/div[3]/div/div[3]/div/div[1]/div/div/div/div[2]/div[2]/div/div[3]/div/table/tbody/tr[1]/td[1]/button")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[1]/div[3]/div/div[3]/div/div[1]/div/div/div/div[2]/div[2]/div/div[3]/div/table/tbody/tr[3]/td[1]/button")).Click();
            driver.FindElement(By.XPath("//*[text()='Klar']")).Click();
            driver.FindElement(By.XPath("//*[text()='1 vuxen']")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/div/div[2]/div/div[1]/div/div/div[1]/div[3]/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/div/div[2]/div/div[1]/div/div/div[2]/div[3]/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/div/div[2]/div/div[1]/div/div[1]/div[2]/div[3]/div/button[2]")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/div/div[2]/div/div[1]/div/div[2]/div[1]/select")).Click();
            driver.FindElement(By.XPath("//*[text()='9 år']")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/div/div[2]/div/div[1]/div/div[2]/div[2]/select")).Click();
            driver.FindElement(By.XPath("//*[text()='13 år']")).Click();
            driver.FindElement(By.XPath("//*[text()='Klar']")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[2]/div/div/div[2]/button")).Click();

            Thread.Sleep(1000);

            var textResult = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[4]/div/div[2]/div[2]/div/div/div[1]/div[2]/div/div[3]/div[1]/div[2]/div[2]/div[1]/span[1]")).GetAttribute("textContent").Trim();

            var textTrim = textResult.Remove(2, 1);

            var resultTrim = textTrim.Replace("kr", string.Empty);
            var result = int.Parse(resultTrim);

            if (result <= 30000)
            {
                sender.SendEmailAsync("martin.landhammar@gmail.com", "Pris på cypernbiljetter", $"Pris på flygbiljett till Cypern är {result}kr hos Ving");

                Assert.Pass();
            }
            else
            {
                Assert.Fail("Priset är för högt");
            }
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}