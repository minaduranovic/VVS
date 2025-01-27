using NUnit.Framework;
using OpenQA.Selenium;

namespace MagentoLumaTesting
{
    [TestFixture]
    public class PretragaTest
    {
        private IWebDriver driver;
        private SearchPage searchPage;

        [SetUp]
        public void SetUp()
        {
            driver = SingletonWebDriver.GetDriver();
            searchPage = new SearchPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            SingletonWebDriver.QuitDriver();
            driver.Dispose();
        }

        [Test]
        public void ValidanUnosKljucneRijeci()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct("Yoga jacket");
            Assert.AreEqual("Josie Yoga Jacket", searchPage.GetFirstProductName());
        }

        [Test]
        public void KljucnaRijecSaSpecijalnimKarakterima()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct("!@#$%^&");
            Assert.AreEqual("Your search returned no results.", searchPage.GetNoResultsMessage());
        }

        [Test]
        public void PretragaIspodMinimalneGranice()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct("ay");
            Assert.AreEqual("Minimum Search query length is 3", searchPage.GetNoResultsMessage());
        }

        [Test]
        public void PretragaDjelimicneKljucneRijeci()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct("Yoga");
            Assert.IsTrue(searchPage.GetFirstProductName().Contains("Yoga"));
        }

        [Test]
        public void PretragaSaUnosomVecimOdMaksimalneGranice()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct(new string('a', 150));
            Assert.AreNotEqual(new string('a', 150), searchPage.GetSearchResultsHeader());
        }

        [Test]
        public void PrazanUnos()
        {
            searchPage.NavigateToHomePage();
            searchPage.SearchForProduct("");
            Assert.AreEqual("Home Page", driver.Title);
        }
    }
}
