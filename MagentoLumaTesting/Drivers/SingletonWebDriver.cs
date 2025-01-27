//SingletonWebDriver.cs
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoLumaTesting
{
    public class SingletonWebDriver
    {
        private static IWebDriver _driver;

        private SingletonWebDriver() { }

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            return _driver;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}