using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;


namespace MagentoLumaTesting
{
    public class ProductPage
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            this.js = (IJavaScriptExecutor)driver;
        }
        public void SelectProduct(int productIndex)
        {
            driver.FindElement(By.CssSelector($".product-item:nth-child({productIndex}) .product-image-photo")).Click();
        }
        public void SelectProductOptions(string sizeOption, string colorOption)
        {
            driver.FindElement(By.Id(sizeOption)).Click();
            driver.FindElement(By.Id(colorOption)).Click();
        }
        public void AddToCart()
        {
            driver.FindElement(By.CssSelector("#product-addtocart-button > span")).Click();

        }
        public void GoToShoppingCart()
        {
            driver.FindElement(By.LinkText("shopping cart")).Click();
        }

        public void VerifyProductNameInCart(string productName)
        {
            Assert.That(driver.FindElement(By.XPath("//*[@id=\"shopping-cart-table\"]/tbody/tr[1]/td[1]/div/strong")).Text, Is.EqualTo(productName));
        }

        public void EmptyShoppingCart()
        {
            driver.FindElement(By.CssSelector(".action-delete")).Click();
        }
        public void SetQuantityOfProduct(int quantity)
        {
            driver.FindElement(By.Id("qty")).Clear();
            driver.FindElement(By.Id("qty")).SendKeys(quantity.ToString());
        }
        public void VerifyQuantityError()
        {
            var elements = driver.FindElements(By.XPath("//*[@id=\"qty-error\"]"));
            Assert.True(elements.Count > 0);
            Assert.That(driver.FindElement(By.XPath("//*[@id=\"qty-error\"]")).Text, Is.EqualTo("Please enter a quantity greater than 0."));
        }
        public void VerifyLargerQuantity()
        {
            int quantity = int.Parse(driver.FindElement(By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[3]/div/div/label/input")).GetAttribute("value"));
            Assert.AreEqual(7, quantity, "The quantity value retrieved from the input field is incorrect.");
        }
    }
}
