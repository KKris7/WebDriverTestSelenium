using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NunitWebDriverSeleniumTest
{
    internal class URLShortenerTests
    {
        private ChromeDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--window-size=1920,1200");
            this.driver = new ChromeDriver(options);
            driver.Url = "https://shorturl.nakov.repl.co/";

        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_HomePageTitle()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(1)"));
            findElement.Click();
            var expectedTitle = "URL Shortener";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_ShortURLPage()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(3)"));
            findElement.Click();
            //driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/urls");

            var expectedTitle = "Short URLs";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            IList<IWebElement> firstCol = driver.FindElements(By.XPath("//tbody/tr/td[1]"));
            IList<IWebElement> secondCol = driver.FindElements(By.XPath("//tbody/tr/td[2]"));

            var firstOriginalUrl = "https://nakov.com";
            var firstShortUrl = "http://shorturl.nakov.repl.co/go/nak";

            Assert.That(firstCol[0].Text, Is.EqualTo(firstOriginalUrl));
            Assert.That(secondCol[0].Text, Is.EqualTo(firstShortUrl));


        }

        [Test]
        public void Test_EmptyURL_AddURLPage()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(5)"));
            findElement.Click();
            //driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/add-url");

            var expectedTitle = "Add Short URL";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            var createButton = driver.FindElement(By.CssSelector(
                "body > main > form > table > tbody > tr:nth-child(3) > td > button"));
            createButton.Click();
            var errorMessage = driver.FindElement(By.CssSelector("body > div"));
            var emptyResult = "URL cannot be empty!";

            Assert.That(errorMessage.Text, Is.EqualTo(emptyResult));
        }

        [Test]
        public void Test_InvalidURL_AddURLPage()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(5)"));
            findElement.Click();
            //driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/add-url");

            var expectedTitle = "Add Short URL";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            var urlInput = driver.FindElement(By.CssSelector("#url"));
            urlInput.SendKeys("alabala");

            var createButton = driver.FindElement(By.CssSelector(
                "body > main > form > table > tbody > tr:nth-child(3) > td > button"));
            createButton.Click();

            var errorMessage = driver.FindElement(By.CssSelector("body > div"));
            var emptyResult = "Invalid URL!";

            Assert.That(errorMessage.Text, Is.EqualTo(emptyResult));
        }

        [Test]
        public void Test_ValidURL_AddURLPage()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(5)"));
            findElement.Click();
            //driver.Navigate().GoToUrl("https://shorturl.nakov.repl.co/add-url");

            // checking if we are on the right page
            var expectedTitle = "Add Short URL";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));


            string urlInput = $"https://softuni{DateTime.Now.Ticks}.bg";
            driver.FindElement(By.CssSelector("#url")).SendKeys(urlInput);
            driver.FindElement(By.CssSelector(
                "body > main > form > table > tbody > tr:nth-child(3) > td > button")).Click();

            // checking if we are on the right page 
            var shortUrlTitle = "Short URLs";
            Assert.That(driver.Title, Is.EqualTo(shortUrlTitle));

            // getting all elements that were created
            var links = driver.FindElements(By.LinkText(urlInput));

            // checking if the added url is uniquely 
            Assert.That(links.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_VisitURL()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(3)"));
            findElement.Click();
            // checking if we are on the right page 
            var expectedTitle = "Short URLs";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            var visitsCountBeforeClick = driver.FindElement(By.CssSelector(
                "body > main > table > tbody > tr:nth-child(4) > td:nth-child(4)")).Text;
            
            driver.FindElement(By.CssSelector(
                "body > main > table > tbody > tr:nth-child(4) > td:nth-child(2) > a")).Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Thread.Sleep(1000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);

            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            var visitsCountAfterClick = driver.FindElement(By.CssSelector(
                "body > main > table > tbody > tr:nth-child(4) > td:nth-child(4)")).Text;

            // checking if the count has increased and if they are not equal
            Assert.AreNotEqual(visitsCountAfterClick, visitsCountBeforeClick);
            Assert.Greater(visitsCountAfterClick, visitsCountBeforeClick);

        }

        [Test]
        public void Test_VisitInvalidURL()
        {
            var findElement = driver.FindElement(By.CssSelector("body > header > a:nth-child(3)"));
            findElement.Click();
            // checking if we are on the right page 
            var expectedTitle = "Short URLs";
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));

            driver.FindElement(By.CssSelector("" +
                "body > main > table > tbody > tr:nth-child(30) > td:nth-child(1) > a")).Click();

            Assert.Pass("This site can’t be reached");
        }
    }
}
