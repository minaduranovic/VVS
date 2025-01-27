using MagentoLumaTesting;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class FiltriranjeiSortiranjeTests
    {
        private IWebDriver driver;
        private FiltriranjeSortiranjePage filtriranjeSortiranjePage;

        [SetUp]
        public void SetUp()
        {
            driver = SingletonWebDriver.GetDriver();
            filtriranjeSortiranjePage = new FiltriranjeSortiranjePage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            SingletonWebDriver.QuitDriver();
            driver.Dispose();
        }

        [Test]
        public void SortiranjePoImenuProizvoda_EkvivalentneParticije()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-4 > span:nth-child(2)");
            filtriranjeSortiranjePage.SelectSubCategory("Bras & Tanks");
            filtriranjeSortiranjePage.SelectSortOption("Product Name");
            bool isSortedByName = filtriranjeSortiranjePage.AreProductsSortedByName();
            Assert.IsTrue(isSortedByName, "Proizvodi nisu sortirani abecedno po imenu.");
        }

        [Test]
        public void KombinovanoFiltriranjeISortiranjePoCijeni_UseCaseTesting()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-5 > span:nth-child(2)"); // Izmijenite prema kategoriji
            filtriranjeSortiranjePage.SelectSubCategory("Jackets"); // Izmijenite prema podkategoriji
            filtriranjeSortiranjePage.ApplyFilter(".filter-options-item:nth-child(4) > .filter-options-title"); // Filter za boju
            filtriranjeSortiranjePage.ApplyFilter(".swatch-option-link-layered:nth-child(2) > .color"); // Selektuje plavu boju
            filtriranjeSortiranjePage.SelectSortOption("Price"); // Sortira po cijeni
            Assert.IsTrue(filtriranjeSortiranjePage.AreAllProductsFilteredByColor("Blue"), "Nisu svi proizvodi plave boje.");
            Assert.IsTrue(filtriranjeSortiranjePage.AreProductsSortedByPrice(), "Proizvodi nisu sortirani po ceni.");

        }

        [Test]
        public void FiltriranjePoJednojOpcijiVelicina_EkvivalentneParticije()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-4 > span:nth-child(2)"); // Navigacija ka glavnoj kategoriji
            filtriranjeSortiranjePage.SelectSubCategory("Hoodies & Sweatshirts"); // Selektovanje podkategorije
            filtriranjeSortiranjePage.ApplyFilter(".filter-options-item:nth-child(2) > .filter-options-title"); // Otvaranje filtera za veličine
            filtriranjeSortiranjePage.ApplyFilter(".swatch-option-link-layered:nth-child(3) > .text"); // Selektovanje veličine "M"
            bool isFilteredBySize = filtriranjeSortiranjePage.AreAllProductsFilteredBySize("M");
            Assert.IsTrue(isFilteredBySize, "Nisu svi proizvodi filtrirani po veličini 'M'.");
        }
        [Test]
        public void FiltriranjePoDvijeOpcije_BojaIMaterijal_EkvivalentneParticije()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-4 > span:nth-child(2)");
            filtriranjeSortiranjePage.SelectSubCategory("Shorts");
            filtriranjeSortiranjePage.ApplyFilter(".filter-options-item:nth-child(4) > .filter-options-title");
            filtriranjeSortiranjePage.ApplyFilter(".swatch-option-link-layered:nth-child(1) > .color");
            filtriranjeSortiranjePage.ApplyFilter(".filter-options-item:nth-child(6) > .filter-options-title");
            filtriranjeSortiranjePage.ApplyFilter(".active .item:nth-child(4) > a");
            bool isFilteredByColor = filtriranjeSortiranjePage.AreAllProductsFilteredByColor("Black");
            Assert.IsTrue(isFilteredByColor, "Nisu svi proizvodi filtrirani po boji 'Black'.");
        }

        [Test]
        public void SortiranjePoCijeni_BVA()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-5 > span:nth-child(2)");
            filtriranjeSortiranjePage.SelectSubCategory("Pants");
            filtriranjeSortiranjePage.SelectSortOption("Price");
            bool isSortedByPrice = filtriranjeSortiranjePage.AreProductsSortedByPrice();
            Assert.That(isSortedByPrice, Is.True, "Proizvodi nisu sortirani rastuće po ceni.");
        }

        [Test]
        public void FiltriranjePoCijeni_BVA()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            filtriranjeSortiranjePage.NavigateToMainCategory("#ui-id-4 > span:nth-child(2)");
            filtriranjeSortiranjePage.SelectSubCategory("Jackets");
            filtriranjeSortiranjePage.ApplyFilter(".filter-options-item:nth-child(11) > .filter-options-title");
            filtriranjeSortiranjePage.ApplyFilter(".item:nth-child(4) .price:nth-child(2)");
            var expectedMinimumPrice = 70;
            var expectedMaximumPrice = 80;
            bool isFilteredByPrice = filtriranjeSortiranjePage.AreAllProductsFilteredByPriceRange(expectedMinimumPrice, expectedMaximumPrice);
            Assert.IsTrue(isFilteredByPrice, $"Neki proizvodi nisu filtrirani u opsegu ({expectedMinimumPrice}-{expectedMaximumPrice}).");
        }
    }
}
