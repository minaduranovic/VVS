using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MagentoLumaTesting
{
    public class PrijavaPage
    {
        private IWebDriver driver;

        public PrijavaPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement EmailField => driver.FindElement(By.Id("email"));
        private IWebElement PasswordField => driver.FindElement(By.Id("pass"));
        private IWebElement SignInButton => driver.FindElement(By.Id("send2"));
        private IWebElement ResetPasswordLink => driver.FindElement(By.CssSelector(".remind > span"));

        public void EnterEmail(string email) => EmailField.SendKeys(email);
        public void EnterPassword(string password) => PasswordField.SendKeys(password);
        public void Submit() => SignInButton.Click();
        public void ResetPassword() => ResetPasswordLink.Click();
    }
}
