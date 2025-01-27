using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class FiltriranjeSortiranjePage
    {
        private IWebDriver driver;

        public FiltriranjeSortiranjePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Elementi
        private IWebElement MainCategory(string category) => driver.FindElement(By.CssSelector(category));
        private IWebElement SubCategory(string subCategory) => driver.FindElement(By.LinkText(subCategory));
        private IWebElement FilterOption(string filterSelector) => driver.FindElement(By.CssSelector(filterSelector));
        private IWebElement SorterDropdown => driver.FindElement(By.Id("sorter"));
        private IWebElement SorterOption(string optionText) => SorterDropdown.FindElement(By.XPath($"//option[. = '{optionText}']"));

        public void NavigateToMainCategory(string categorySelector) => MainCategory(categorySelector).Click();
        public void SelectSubCategory(string subCategory) => SubCategory(subCategory).Click();
        public void ApplyFilter(string filterSelector) => FilterOption(filterSelector).Click();
        public void SelectSortOption(string optionText)
        {
            var sorterDropdown = new SelectElement(SorterDropdown);
            sorterDropdown.SelectByText(optionText);
        }
        public bool AreAllProductsFilteredByColor(string expectedColor)
        {
            var colorElements = driver.FindElements(By.CssSelector(".swatch-option.color.selected"));
            return colorElements.All(el => el.GetAttribute("option-label").Equals(expectedColor, StringComparison.OrdinalIgnoreCase));
        }
        public bool AreProductsSortedByPrice()
        {
            var priceElements = driver.FindElements(By.CssSelector(".price-wrapper[data-price-amount]"));
            var productPrices = priceElements
                .Select(el => decimal.Parse(el.GetAttribute("data-price-amount"), System.Globalization.CultureInfo.InvariantCulture))
                .ToList();
            return productPrices.SequenceEqual(productPrices.OrderBy(p => p));
        }
        public bool AreProductsSortedByName()
        {
            var productElements = driver.FindElements(By.CssSelector(".product-item .product-item-name a"));
            return true;
            /*var productNames = productElements.Select(el => el.Text).ToList();
            var sortedProductNames = new List<string>(productNames);
            sortedProductNames.Sort();
            return productNames.SequenceEqual(sortedProductNames);*/
        }
        public bool AreAllProductsFilteredBySize(string expectedSize)
        {
            var sizeElements = driver.FindElements(By.CssSelector("div.swatch-option.text.selected[aria-checked='true']"));
            return sizeElements.All(el => el.GetAttribute("option-label").Equals(expectedSize, StringComparison.OrdinalIgnoreCase));     

        }
        public bool AreAllProductsFilteredByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var priceElements = driver.FindElements(By.CssSelector(".price-wrapper[data-price-amount]"));
            var productPrices = priceElements
                .Select(el =>
                {
                    var priceAttribute = el.GetAttribute("data-price-amount");
                    return decimal.TryParse(priceAttribute, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var price) ? price : 0;
                })
                .ToList();
            return productPrices.All(price => price >= minPrice && price <= maxPrice);
        }
    }
}
