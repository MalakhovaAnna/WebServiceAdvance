using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace SeleniumBasicTest
{
    public class Tests
    {
        private IWebDriver driver;
        private IWebDriver wait;
        private MainPage mainPage;
        private string baseURL;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost:5000";
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test1Login()
        {
            mainPage = new MainPage(driver);
            mainPage.LoginNW();
            Assert.AreEqual("Home page", driver.FindElement(By.XPath("//h2[text()='Home page']")).Text);
        }
        [Test]
        public void Test2AddNewProduct()
        {
            mainPage = new MainPage(driver);
            mainPage.LoginNW();
            driver.FindElement(By.XPath("//a[text()='All Products']")).Click();
            driver.FindElement(By.XPath("//a[text()='Create new']")).Click();
            driver.FindElement(By.XPath("//input[@id='ProductName']")).SendKeys("Ekzo");
            new SelectElement(driver.FindElement(By.Id("CategoryId"))).SelectByText("Confections");
            new SelectElement(driver.FindElement(By.Id("SupplierId"))).SelectByText("Pavlova, Ltd.");            
            driver.FindElement(By.XPath("//input[@id='UnitPrice']")).SendKeys("68");
            driver.FindElement(By.XPath("//input[@id='QuantityPerUnit']")).SendKeys("50 - 1kg box");
            driver.FindElement(By.XPath("//input[@id='UnitsInStock']")).SendKeys("40");
            driver.FindElement(By.XPath("//input[@id='UnitsOnOrder']")).SendKeys("30");
            driver.FindElement(By.XPath("//input[@id='ReorderLevel']")).SendKeys("0");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@type='checkbox']"))).Click();            
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            Assert.AreEqual("All Products", driver.FindElement(By.XPath("//h2[text()='All Products']")).Text);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td/a[text()='Remove']"))).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(2000);
            alert.Accept();
        }
        [Test]
        public void Test3OpenNewProduct()
        {
            mainPage = new MainPage(driver);
            mainPage.LoginNW();
            driver.FindElement(By.XPath("//a[text()='All Products']")).Click();
            driver.FindElement(By.XPath("//a[text()='Create new']")).Click();
            driver.FindElement(By.XPath("//input[@id='ProductName']")).SendKeys("Ekzo");
            new SelectElement(driver.FindElement(By.Id("CategoryId"))).SelectByText("Confections");
            new SelectElement(driver.FindElement(By.Id("SupplierId"))).SelectByText("Pavlova, Ltd.");
            driver.FindElement(By.XPath("//input[@id='UnitPrice']")).SendKeys("68");
            driver.FindElement(By.XPath("//input[@id='QuantityPerUnit']")).SendKeys("50 - 1kg box");
            driver.FindElement(By.XPath("//input[@id='UnitsInStock']")).SendKeys("40");
            driver.FindElement(By.XPath("//input[@id='UnitsOnOrder']")).SendKeys("30");
            driver.FindElement(By.XPath("//input[@id='ReorderLevel']")).SendKeys("0");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@type='checkbox']"))).Click();
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            
            Thread.Sleep(2000);
            Assert.AreEqual("Ekzo", driver.FindElement(By.XPath("//a[text()='Ekzo']")).Text);
            Assert.AreEqual("Confections", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='Confections']")).Text);
            Assert.AreEqual("Pavlova, Ltd.", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='Pavlova, Ltd.']")).Text);
            Assert.AreEqual("68,0000", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='68,0000']")).Text);
            Assert.AreEqual("50 - 1kg box", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='50 - 1kg box']")).Text);
            Assert.AreEqual("40", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='40']")).Text);
            Assert.AreEqual("30", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='30']")).Text);
            Assert.AreEqual("0", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='0']")).Text);
            Assert.AreEqual("True", driver.FindElement(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td[text()='True']")).Text);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//a[text()='Ekzo']/parent::td/following-sibling::td/a[text()='Remove']"))).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        [Test]
        public void Test4Logout()
        {
            mainPage = new MainPage(driver);
            mainPage.LoginNW();
            driver.FindElement(By.XPath("//a[text()='Logout']")).Click();
            Assert.AreEqual("Login", driver.FindElement(By.XPath("//h2[text()='Login']")).Text);
        }
        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}