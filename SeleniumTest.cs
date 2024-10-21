using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var options = new ChromeOptions();
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://www.google.com/");

            IWebElement myField = driver.FindElement(By.TagName("textarea"));
            myField.SendKeys("Prometheus Group");
            myField.Submit();

            myField = driver.FindElement(By.PartialLinkText("Prometheus Group"));
            string browserUrl = myField.GetAttribute("href");
            Assert.That(browserUrl, Is.EqualTo("https://www.prometheusgroup.com/"));

            myField = driver.FindElement(By.PartialLinkText("Contact Us"));
            browserUrl = myField.GetAttribute("href");
            Assert.That(browserUrl, Is.EqualTo("https://www.prometheusgroup.com/contact-us"));
            myField.Click();

            myField = driver.FindElement(By.Name("firstname"));
            myField.SendKeys("Michael");

            myField = driver.FindElement(By.Name("lastname"));
            myField.SendKeys("Jaworski");
            myField.Submit();

            var reqFields = driver.FindElements(By.XPath("//*[@class=\"hs-error-msg hs-main-font-element\"]"));
            Assert.That(reqFields.Count(), Is.EqualTo(4));
            Assert.That(reqFields[0].Text, Is.EqualTo("Please complete this required field."));
        }
    }
}