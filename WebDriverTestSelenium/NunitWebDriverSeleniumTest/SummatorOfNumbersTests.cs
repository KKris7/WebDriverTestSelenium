using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace NunitWebDriverSeleniumTest
{
    internal class SummatorOfNumbersTests
    {
        private ChromeDriver driver;
        IWebElement firstNum;
        IWebElement secondNum;
        IWebElement operattion;
        IWebElement result;
        IWebElement clearAllButton;
        IWebElement calculateButton;

        [OneTimeSetUp]
        public void OpenBrowserAndNavigate()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--window-size=1920,1200");
            this.driver = new ChromeDriver(options);
            driver.Url = "https://number-calculator.nakov.repl.co/";
           //driver.Manage().Window.Maximize();
            firstNum = driver.FindElement(By.Id("number1"));
            secondNum = driver.FindElement(By.Id("number2"));
            operattion = driver.FindElement(By.Id("operation"));
            result = driver.FindElement(By.Id("result"));
            clearAllButton = driver.FindElement(By.Id("resetButton"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
        }

        [OneTimeTearDown]

        public void ShutdownBroser()
        {
            driver.Quit();
        }

        [TestCase("6", "4", "+", "Result: 10")]
        [TestCase("123", "321", "*", "Result: 39483")]
        [TestCase("63", "30", "-", "Result: 33")]
        [TestCase("18", "3", "/", "Result: 6")]

        public void Test_SumTwoIntNumbers(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }

        [TestCase("6.43", "4.12", "+", "Result: 10.55")]
        [TestCase("123.321", "321.123", "-", "Result: -197.802")]
        [TestCase("6.9", "3.4", "*", "Result: 23.46")]
        [TestCase("6.43", "4.00", "/", "Result: 1.6075")]

        public void Test_SumTwoDecimalNumbers(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }

        [TestCase("6.43", "4.1e52", "*", "Result: 2.6363e+53")]
        [TestCase("3.9e13", "30.43", "/", "Result: 1281629970420")]

        public void Test_SumTwoExponentialNumbers(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }

        [TestCase("", "wtf", "+", "Result: invalid input")]
        [TestCase("are", "be", "-", "Result: invalid input")]
        [TestCase("kur", "hello", "*", "Result: invalid input")]
        [TestCase("-", "+", "2", "Result: invalid input")]

        public void Test_InvalidInput(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }

        [TestCase("2", "4", "#", "Result: invalid operation")]
        [TestCase("2", "3", "@", "Result: invalid operation")]
        [TestCase("123", "5", "!", "Result: invalid operation")]
        [TestCase("1", "1", "2", "Result: invalid operation")]

        public void Test_InvalidOperation(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }

        [TestCase("1", "2", "+", "Result: 3")]
        [TestCase("", "hello", "+", "Result: invalid input")]
        public void Test_Reset_TwoNumbers(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            clearAllButton.Click();
            Assert.IsEmpty(firstNum.Text);
            Assert.IsEmpty(secondNum.Text);
        }

        [TestCase("Infinity", "1", "+", "Result: Infinity")]
        [TestCase("Infinity", "1", "-", "Result: Infinity")]
        [TestCase("Infinity", "1", "*", "Result: Infinity")]
        [TestCase("Infinity", "1", "/", "Result: Infinity")]
        [TestCase("Infinity", "Infinity", "+", "Result: Infinity")]
        [TestCase("Infinity", "Infinity", "-", "Result: invalid calculation")]
        [TestCase("Infinity", "Infinity", "*", "Result: Infinity")]
        [TestCase("Infinity", "Infinity", "/", "Result: invalid calculation")]
        [TestCase("1", "Infinity", "+", "Result: Infinity")]
        [TestCase("2", "Infinity", "-", "Result: -Infinity")]
        [TestCase("3", "Infinity", "*", "Result: Infinity")]
        [TestCase("4", "Infinity", "/", "Result: 0")]

        public void Test_Infinity(string num1, string num2, string oper, string expecedResult)
        {
            firstNum.SendKeys(num1);
            secondNum.SendKeys(num2);
            operattion.SendKeys(oper);
            calculateButton.Click();

            Assert.That(expecedResult, Is.EqualTo(result.Text));

            clearAllButton.Click();
        }
    }
}
