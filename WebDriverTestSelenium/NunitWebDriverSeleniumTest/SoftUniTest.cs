using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NunitWebDriverSeleniumTest
{
    public class SoftUniTests
    {
        private WebDriver driver;

        [OneTimeSetUp]
        public void OpenBrowserAndNavigate()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--window-size=1920,1200");
            this.driver = new ChromeDriver(options);
            driver.Url = "https://softuni.bg/";
            //driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void BrowserShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_CheckMainTitle()
        {
            //act
            var findElement = driver.FindElement(By.CssSelector("#page-header > div > div > div > div.logo.hover-dropdown-btn > a > img.desktop-logo.hidden-xs"));
            findElement.Click();

            string expectedTitle = "Обучение по програмиране - Софтуерен университет";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_CheckCurriculumTitleByURL()
        {
            //act
            driver.Navigate().GoToUrl("https://softuni.bg/curriculum");

            string expectedTitle = "Учебен план - Софтуерен университет";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_CheckCertificatesTitle()
        {
            //act
            var findElement = driver.FindElement(By.CssSelector("#header-nav > div.toggle-nav.toggle-holder > ul > li:nth-child(5) > a > span"));
            findElement.Click();

            string expectedTitle = "Сертификати и дипломи за програмист - Софтуерен университет";

            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }
    }
}