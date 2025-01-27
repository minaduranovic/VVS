using MagentoLumaTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;



namespace MagentoLumaTesting
{
    [TestFixture]
    public class DodavanjeUKorpuICheckoutTests
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        private ProductPage productPage;
        private CheckoutPage checkoutPage;
        private DiscountPage discountPage;


        [SetUp]
        public void SetUp()
        {
            TestContext.WriteLine("Setup starts");
            SingletonWebDriver.QuitDriver();
            driver = SingletonWebDriver.GetDriver();
            js = (IJavaScriptExecutor)driver;
            driver.Manage().Window.Maximize();
            productPage = new ProductPage(driver);
            checkoutPage = new CheckoutPage(driver);
            discountPage = new DiscountPage(driver);
            TestContext.WriteLine("Setup complete");
        }

        [TearDown]
        public void TearDown()
        {
            TestContext.WriteLine("TearDown starts");
            SingletonWebDriver.QuitDriver();
            driver.Dispose();
            TestContext.WriteLine("TearDown complete");
        }


        public void Prijava_SetUp()
        {
            driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
            driver.FindElement(By.LinkText("Sign In")).Click();
            driver.FindElement(By.Id("email")).Click();
            driver.FindElement(By.Id("email")).SendKeys("mutole@cyclelove.cc");
            driver.FindElement(By.Id("pass")).Click();
            driver.FindElement(By.Id("pass")).SendKeys("Vvs19459.");
            driver.FindElement(By.CssSelector(".primary:nth-child(1) > #send2 > span")).Click();
        }


        [Test, Order(1)]
        public void DodavanjeVazecegProizvodaUKorpu()
        {
            Console.WriteLine($"Executing test: {TestContext.CurrentContext.Test.Name}");
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,352.79998779296875)");
            productPage.GoToShoppingCart();
            productPage.VerifyProductNameInCart("Radiant Tee");
            productPage.EmptyShoppingCart();
            Console.WriteLine($"Completed test: {TestContext.CurrentContext.Test.Name}");

        }

        [Test, Order(2)]
        public void DodavanjeVeceKolicineProizvodaUKorpu()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.SetQuantityOfProduct(7);
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,452)");
            productPage.GoToShoppingCart();
            productPage.VerifyProductNameInCart("Radiant Tee");
            productPage.VerifyLargerQuantity();
            productPage.EmptyShoppingCart();
        }


        [Test, Order(3)]
        public void DodavanjeNevalidneKolicineProizvoda()
        {
            TestContext.WriteLine($"Executing test: {TestContext.CurrentContext.Test.Name}");
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.SetQuantityOfProduct(-3);
            productPage.AddToCart();
            productPage.VerifyQuantityError();
            TestContext.WriteLine($"Completed test: {TestContext.CurrentContext.Test.Name}");
        }


        [Test, Order(4)]
        public void PlacanjeSaNepotpunimPodacimaZaDostavu()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,352)");
            productPage.GoToShoppingCart();
            checkoutPage.ProceedToCheckout();
            checkoutPage.EnterCity("city");
            checkoutPage.SelectRegion("Arkansas");
            js.ExecuteScript("window.scrollTo(0,505)");
            checkoutPage.EnterPostcode("71657");
            checkoutPage.EnterTelephone("123456789");
            checkoutPage.SelectShipping();
            checkoutPage.SubmitOrder();
            checkoutPage.VerifyRequiredFieldError("This is a required field.");
        }

        [Test, Order(5)]
        public void UspjesnoPlacanjeSaVazecimPodacima()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,352)");
            productPage.GoToShoppingCart();
            checkoutPage.ProceedToCheckout();
            checkoutPage.EnterAddress("Test Street 123");
            checkoutPage.EnterCity("city");
            checkoutPage.SelectRegion("Arkansas");
            js.ExecuteScript("window.scrollTo(0,505)");
            checkoutPage.EnterPostcode("71657");
            checkoutPage.EnterTelephone("123456789");
            checkoutPage.SelectShipping();
            checkoutPage.SubmitOrder();
            checkoutPage.ProceedToOrder();
            driver.Navigate().Refresh();
            checkoutPage.VerifyOrderConfirmation();
        }


        [Test, Order(6)]
        public void DodavanjeBrzeDostaveNaNarudzbu()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            productPage.GoToShoppingCart();
            driver.FindElement(By.CssSelector(".item > .primary")).Click();
            driver.FindElement(By.Name("ko_unique_2")).Click();
            driver.FindElement(By.CssSelector(".button")).Click();
            js.ExecuteScript("window.scrollTo(0,0)");
            checkoutPage.VerifyDeliveryMethod();
        }

        [Test, Order(7)]
        public void DodavanjeNevalidnogKodaZaPopustNaNarudbu()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,352)");
            productPage.GoToShoppingCart();
            checkoutPage.ProceedToCheckout();
            checkoutPage.SubmitOrder();
            discountPage.OpenDiscountPanel();
            discountPage.ApplyDiscountCode("123");
            discountPage.VerifyDiscountError("The coupon code isn't valid. Verify the code and try again.");
        }


        [Test, Order(8)]
        public void DodavanjeValidnogKodaZaPopustNaNarudzbu()
        {
            Prijava_SetUp();
            productPage.SelectProduct(1);
            productPage.SelectProductOptions("option-label-size-143-item-167", "option-label-color-93-item-56");
            productPage.AddToCart();
            js.ExecuteScript("window.scrollTo(0,452)");
            productPage.GoToShoppingCart();
            checkoutPage.ProceedToCheckout();
            checkoutPage.SubmitOrder();
            js.ExecuteScript("window.scrollTo(0,0)");
            discountPage.OpenDiscountPanel();
            discountPage.ApplyDiscountCode("20poff");
            discountPage.VerifyDiscountApplied("Discount (Get flat 20% off on all products)");
        }

    }
}
