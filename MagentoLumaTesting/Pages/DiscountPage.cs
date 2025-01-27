using OpenQA.Selenium;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MagentoLumaTesting
{
    public class DiscountPage
    {
        private IWebDriver driver;


        public DiscountPage(IWebDriver driver)
        {
            this.driver = driver;
            TestContext.WriteLine("DiscountPage initialized.");
        }

        public void ApplyDiscountCode(string discountCode)
        {
            TestContext.WriteLine($"Entering discount code: {discountCode}");

            // Find the discount code input field and enter the discount code
            IWebElement discountCodeField = driver.FindElement(By.Id("discount-code"));
            discountCodeField.SendKeys(discountCode);
            TestContext.WriteLine("Discount code entered.");
            Thread.Sleep(2000);
            // Using JavaScriptExecutor to click the Apply Discount button
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IWebElement button = (IWebElement)js.ExecuteScript("return document.querySelector('#discount-form > div.actions-toolbar > div > button');");

            // Perform the click action using JavaScript
            js.ExecuteScript("arguments[0].click();", button);  // JavaScript click

            TestContext.WriteLine("Clicked the Apply Discount button.");
        }


        public void OpenDiscountPanel()
        {
            TestContext.WriteLine("Opening discount panel...");

            // Using JavaScriptExecutor to click the element
            IWebElement discountPanelHeading = driver.FindElement(By.CssSelector("#block-discount-heading > span"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", discountPanelHeading);
            TestContext.WriteLine("Discount panel opened.");
        }

        public void VerifyDiscountError(string expectedErrorMessage)
        {
            TestContext.WriteLine($"Checking if text '{expectedErrorMessage}' exists on the page...");

            // Define the wait time (3 seconds)
            System.Threading.Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            // Wait for the page source to contain the expected text
            try
            {
                wait.Until(driver =>
                {
                    string pageSource = driver.PageSource;
                    return pageSource.Contains(expectedErrorMessage); // Check if text exists
                });

                // If we reach here, the text has appeared
                TestContext.WriteLine($"Text '{expectedErrorMessage}' found on the page.");
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine($"Text '{expectedErrorMessage}' not found on the page within the timeout.");
                throw; // Rethrow the exception after logging it
            }
        }




        public void VerifyDiscountApplied(string discountMethod)
        {
            TestContext.WriteLine("Verifying if discount was applied...");
            Thread.Sleep(3000);
            string actualDiscountMethod = driver.FindElement(By.CssSelector("#opc-sidebar > div.opc-block-summary > table > tbody > tr.totals.discount > th > span.title")).Text;
            Assert.That(discountMethod, Does.Contain(actualDiscountMethod));
            TestContext.WriteLine("Verified discount was applied.");
        }
    }
}
