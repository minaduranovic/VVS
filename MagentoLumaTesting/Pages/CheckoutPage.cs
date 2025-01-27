using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static OpenQA.Selenium.Keys;



namespace MagentoLumaTesting
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            this.js = (IJavaScriptExecutor)driver;
        }

        public void ProceedToCheckout()
        {
            TestContext.WriteLine("Proceeding to checkout...");
            IWebElement checkoutButton = driver.FindElement(By.CssSelector(".checkout > span"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkoutButton);
            TestContext.WriteLine("Checkout button clicked.");
        }
        public void EnterCity(string city)
        {
            TestContext.WriteLine($"Entering city: {city}");
            IWebElement cityField = driver.FindElement(By.Name("city"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", cityField);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", cityField, city);
            cityField.SendKeys(Keys.Enter);  // Simulate pressing Enter
            TestContext.WriteLine("City entered.");
        }

        public void EnterAddress(string address)
        {
            TestContext.WriteLine($"Entering address: {address}");
            IWebElement addressField = driver.FindElement(By.Name("street[0]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addressField);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", addressField, address);
            addressField.SendKeys(Keys.Enter);  // Simulate pressing Enter
            TestContext.WriteLine("Address entered.");
        }

        public void SelectRegion(string region)
        {
            TestContext.WriteLine($"Selecting region: {region}");
            IWebElement regionField = driver.FindElement(By.Name("region_id"));
            SelectElement select = new SelectElement(regionField);
            select.SelectByText(region);
            TestContext.WriteLine("Region selected.");
        }

        public void EnterPostcode(string postcode)
        {
            TestContext.WriteLine($"Entering postcode: {postcode}");
            IWebElement postcodeField = driver.FindElement(By.Name("postcode"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", postcodeField, postcode);
            postcodeField.SendKeys(Keys.Enter);  // Simulate pressing Enter
            TestContext.WriteLine("Postcode entered.");
        }

        public void EnterTelephone(string telephone)
        {
            TestContext.WriteLine($"Entering telephone: {telephone}");
            IWebElement telephoneField = driver.FindElement(By.Name("telephone"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", telephoneField, telephone);
            telephoneField.SendKeys(Keys.Enter);  // Simulate pressing Enter
            TestContext.WriteLine("Telephone entered.");
        }


        public void SelectShipping()
        {
            TestContext.WriteLine("Selecting shipping method...");
            IWebElement shippingOption = driver.FindElement(By.CssSelector(".row:nth-child(1) .radio"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", shippingOption);
            TestContext.WriteLine("Shipping method selected.");
        }

        public void SubmitOrder()
        {
            TestContext.WriteLine("Submitting order...");

            // Wait for the submit button to be clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".button > span")));

            // Click on the submit button using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", submitButton);

            // Wait until the confirmation element is visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"shipping-method-buttons-container\"]/div/button")));

            TestContext.WriteLine("Order submitted.");
        }

        public void VerifyRequiredFieldError(string expectedErrorMessage)
        {
            TestContext.WriteLine("Verifying required field error...");
            string errorMessage = driver.FindElement(By.XPath("//span[contains(.,'This is a required field.')]")).Text;
            Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
            TestContext.WriteLine("Required field error verified.");
        }
        public void VerifyOrderConfirmation()
        {
            TestContext.WriteLine("Verifying order confirmation...");

            // Set up WebDriverWait to wait for up to 10 seconds for the element to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                // Wait until the element is visible
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/main/div[3]/div/div[2]")));

                // Verify that the element is present and visible
                Assert.True(element.Displayed, "Order confirmation is not visible.");
                TestContext.WriteLine("Order confirmation verified.");
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("Timeout: Order confirmation element not found within the time limit.");
                Assert.Fail("Order confirmation element was not found within the time limit.");
            }
            catch (NoSuchElementException ex)
            {
                TestContext.WriteLine($"Exception: {ex.Message}");
                Assert.Fail("Order confirmation element was not found.");
            }
        }

        public void ProceedToOrder()
        {
            TestContext.WriteLine("Proceeding to order...");

            // Wait for the proceed button to be visible and clickable
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement proceedButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/main/div[2]/div/div[2]/div[4]/ol/li[3]/div/form/fieldset/div[1]/div/div/div[2]/div[2]/div[4]/div/button")));

            // Click on the proceed button using JavaScript
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", proceedButton);

            // Wait until the element indicating order proceed is visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/main/div[3]/div/div[2]")));

            TestContext.WriteLine("Order proceeded.");
        }

        public void VerifyDeliveryMethod()
        {
            TestContext.WriteLine("Verifying delivery method...");
            driver.FindElement(By.CssSelector(".payment-method-title")).Click();
            string deliveryMethod = driver.FindElement(By.CssSelector(".mark > .value")).Text;
            Assert.That(deliveryMethod, Is.EqualTo("Best Way - Table Rate"));
            TestContext.WriteLine("Delivery method verified.");
        }
    }
}
