using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Interactions;

namespace SeleniumBasicTest
{
    class MainPage
    {
        private IWebDriver driver;
        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        private IWebElement NameInput => driver.FindElement(By.XPath("//input[@id='Name']"));
        private IWebElement PasswordInput => driver.FindElement(By.XPath("//input[@id='Password']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//input[@type='submit']"));
        
        public void LoginNW()
        {
            //NameInput.SendKeys("user");
            new Actions(driver).Click(NameInput).SendKeys("user").Build().Perform();
            //PasswordInput.SendKeys("user");
            new Actions(driver).Click(PasswordInput).SendKeys("user").Build().Perform();
            new Actions(driver).SendKeys(Keys.Enter).Build().Perform();
            //LoginButton.Click();
        }
        
    }
}

