using OpenQA.Selenium;

namespace MagentoLumaTesting
{
    public class SearchPage
    {
        private IWebDriver driver;

        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement SearchField => driver.FindElement(By.Id("search"));
        private IWebElement NoResultsMessage => driver.FindElement(By.CssSelector(".message.notice div"));
        private IWebElement FirstProductName => driver.FindElement(By.CssSelector(".product.name.product-item-name a"));
        private IWebElement SearchResultsHeader => driver.FindElement(By.CssSelector(".base"));

        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        }

        public void SearchForProduct(string query)
        {
            SearchField.Clear();
            SearchField.SendKeys(query);
            SearchField.SendKeys(Keys.Enter);
        }

        public string GetNoResultsMessage()
        {
            return NoResultsMessage.Text;
        }

        public string GetFirstProductName()
        {
            return FirstProductName.Text;
        }

        public string GetSearchResultsHeader()
        {
            return SearchResultsHeader.Text;
        }
    }
}
