using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MagentoLumaTesting
{
    [TestFixture]
    public class RecenzijeTests
    {
        private IWebDriver driver;
        private RecenzijePage recenzijePage;
        private PrijavaPage prijavaPage;
        private RegistracijaPage registracijaPage;
        private IJavaScriptExecutor js;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = SingletonWebDriver.GetDriver();
            prijavaPage = new PrijavaPage(driver);
            registracijaPage = new RegistracijaPage(driver);
            recenzijePage = new RecenzijePage(driver);
            js = (IJavaScriptExecutor)driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            SingletonWebDriver.QuitDriver();
            driver.Dispose();
        }



        [Test]
        public void UspjesnaRecenzijaBezPrijave()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1066, 820);

            // Klikni na dugme za recenziju
            recenzijePage.ClickOnReviewButton();

            // Skroluj do oblasti sa recenzijama
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");

            // Odaberi ocenu
            IWebElement ratingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Rating_3_label")));
            js.ExecuteScript("arguments[0].click();", ratingElement);

            // Unesi podatke za recenziju
            recenzijePage.EnterNickname("neki nickname");
            recenzijePage.EnterSummary("neki summary");
            recenzijePage.EnterReview("neki review");

            // Pošaljite recenziju
            recenzijePage.SubmitReview();

            // Verifikacija uspešne recenzije
            Assert.True(recenzijePage.IsReviewSubmittedSuccessfully());
        }

        [Test]
        public void NeuspjesnaRecenzijaIzostavljenSummary()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1066, 820);

            recenzijePage.ClickOnReviewButton();
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");

            IWebElement ratingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Rating_3_label")));
            js.ExecuteScript("arguments[0].click();", ratingElement);
            recenzijePage.EnterNickname("neki nickname");
            recenzijePage.EnterReview("neki review");

            // Pošaljite recenziju
            recenzijePage.SubmitReview();

            // Verifikacija greške zbog nedostajućeg summary
            var elements = driver.FindElements(By.XPath("//*[contains(text(),'This is a required field')]"));
            Assert.True(elements.Count > 0);
        }

        [Test]
        public void NeuspjesnaRecenzijaIzostavljenReview()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1066, 820);

            recenzijePage.ClickOnReviewButton();
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");

            IWebElement ratingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Rating_3_label")));
            js.ExecuteScript("arguments[0].click();", ratingElement);

            recenzijePage.EnterNickname("neki nickname");
            recenzijePage.EnterSummary("neki summary");

            // Pošaljite recenziju
            recenzijePage.SubmitReview();

            // Verifikacija greške zbog nedostajućeg review
            var elements = driver.FindElements(By.XPath("//*[contains(text(),'This is a required field')]"));
            Assert.True(elements.Count > 0);
        }

        [Test]
        public void NeuspjesnaRecenzijaIzostavljenNickname()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1066, 820);

            recenzijePage.ClickOnReviewButton();
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");

            IWebElement ratingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Rating_3_label")));
            js.ExecuteScript("arguments[0].click();", ratingElement);

            recenzijePage.EnterSummary("neki summary");
            recenzijePage.EnterReview("neki review");

            // Pošaljite recenziju
            recenzijePage.SubmitReview();

            // Verifikacija greške zbog nedostajućeg nickname
            var elements = driver.FindElements(By.XPath("//*[contains(text(),'This is a required field')]"));
            Assert.True(elements.Count > 0);
        }

        [Test]
        public void NeuspjesnaRecenzijaIzostavljenaOcjena()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1068, 820);

            recenzijePage.ClickOnReviewButton();
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");
            recenzijePage.EnterNickname("neki nick");
            recenzijePage.EnterSummary("neki komentar");
            recenzijePage.EnterReview("nekiduzi review");

            // Pošaljite recenziju
            recenzijePage.SubmitReview();

            // Verifikacija greške zbog nedostajuće ocene
            var elements = driver.FindElements(By.XPath("//*[contains(text(),'Please select one of each of the ratings above.')]"));
            Assert.True(elements.Count > 0);
        }

        [Test]
        public void UspjesnaRecenzijaSPrethodnomPrijavom()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.Manage().Window.Size = new System.Drawing.Size(1070, 820);
            driver.FindElement(By.LinkText("Sign In")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement emailField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email")));
            emailField.SendKeys("jasminapilav3@gmail.com");

            IWebElement passField = driver.FindElement(By.Id("pass"));
            passField.SendKeys("Snjeguljica22");

            driver.FindElement(By.Id("send2")).Click();

            // Čekamo da se stranica učita i da "Reviews" dugme postane kliktabilno
            IWebElement reviewButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".product-item:nth-child(1) .reviews-actions span")));
            reviewButton.Click();

            // Skrolovanje da bi recenzija bila vidljiva
            js.ExecuteScript("window.scrollTo(0, 932.7999877929688)");

            // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            // Pričekaj da ocena bude kliktabilna
            IWebElement ratingElement = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Rating_3_label")));

            // Ako klasičan klik ne funkcioniše, kliknemo putem JavaScript-a
            js.ExecuteScript("arguments[0].click();", ratingElement);

            // Unos podataka za nickname, summary i review

            driver.FindElement(By.Id("summary_field")).SendKeys("neki summary");
            driver.FindElement(By.Id("review_field")).SendKeys("neki review");


            // Kliknite na dugme za slanje
            driver.FindElement(By.CssSelector(".submit > span")).Click();

            // Verifikacija greske ako nije unet nickname
            var elements = driver.FindElements(By.XPath("//*[contains(text(),'You submitted your review for moderation.')]"));
            Assert.True(elements.Count > 0);
        }
    }
}
