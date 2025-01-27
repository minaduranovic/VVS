using NUnit.Framework;
using OpenQA.Selenium;

namespace MagentoLumaTesting
{
    [TestFixture]
    public class PrijavaIRegistracijaTests
    {
        private IWebDriver driver;
        private PrijavaPage prijavaPage;
        private RegistracijaPage registracijaPage;

        [SetUp]
        public void SetUp()
        {
            driver = SingletonWebDriver.GetDriver();
            prijavaPage = new PrijavaPage(driver);
            registracijaPage = new RegistracijaPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            SingletonWebDriver.QuitDriver();
            driver.Dispose();
        }

        [Test]
        public void NeuspjesnaPrijava_PraznaPolja()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            prijavaPage.Submit();
        }

        [Test]
        public void NeuspjesnaPrijava_NevalidanEmail()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            prijavaPage.EnterEmail("d");
            prijavaPage.Submit();
        }

        [Test]
        public void NeuspjesnaPrijava_NeispravniPodaciIliBlokiranRacun()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            prijavaPage.EnterEmail("minaduranovic@gmail.com");
            prijavaPage.EnterPassword("123456");
            prijavaPage.Submit();
        }

        [Test]
        public void NeuspjesnaRegistracija_LozinkeSeNePoklapaju()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Create an Account")).Click();
            registracijaPage.EnterFirstName("Adna");
            registracijaPage.EnterLastName("Hajdarevic");
            registracijaPage.EnterEmail("adna.hajdarevic29092003@gmail.com");
            registracijaPage.EnterPassword("Md1234566");
            registracijaPage.ConfirmPassword("Minamina");
            registracijaPage.Submit();
        }

        [Test]
        public void NeuspjesnaRegistracija_SlabaLozinka()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Create an Account")).Click();
            registracijaPage.EnterFirstName("Adna");
            registracijaPage.EnterLastName("Hajdarevic");
            registracijaPage.EnterEmail("adna.hajdarevic29092003@gmail.com");
            registracijaPage.EnterPassword("1234");
            registracijaPage.ConfirmPassword("1234");
            registracijaPage.Submit();
        }

        [Test]
        public void NeuspjesnaRegistracija_PraznaPolja()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Create an Account")).Click();
            registracijaPage.Submit();
        }

        [Test]
        public void NeuspjesnaRegistracija_EmailVecPostoji()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Create an Account")).Click();
            registracijaPage.EnterFirstName("Mina");
            registracijaPage.EnterLastName("Duranovic");
            registracijaPage.EnterEmail("minaduranovic@gmail.com");
            registracijaPage.EnterPassword("Md123456");
            registracijaPage.ConfirmPassword("Md123456");
            registracijaPage.Submit();
        }

        [Test]
        public void UspjesnaPrijava_ResetLozinke()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            prijavaPage.ResetPassword();
            prijavaPage.EnterEmail("minaduranovic@gmail.com");
            prijavaPage.Submit();
        }

        [Test]
        public void UspjesnaPrijava_IspravniPodaci()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            prijavaPage.EnterEmail("minaduranovic@gmail.com");
            prijavaPage.EnterPassword("Md25082006");
            prijavaPage.Submit();
        }

        // [Test]
        // public void UspjesnaRegistracija_IspravniPodaci()
        // {
        //     driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        //     driver.FindElement(By.LinkText("Create an Account")).Click();
        //     registracijaPage.EnterFirstName("Adna");
        //     registracijaPage.EnterLastName("Hajdarevic");
        //     registracijaPage.EnterEmail("duranammar@gmail.com");
        //     registracijaPage.EnterPassword("Minaduranovic123");
        //     registracijaPage.ConfirmPassword("Minaduranovic123");
        //     registracijaPage.Submit();
        // }
    }
}

