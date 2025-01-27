using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MagentoLumaTesting
{
    public class RegistracijaPage
    {
        private IWebDriver driver;

        public RegistracijaPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement FirstNameField => driver.FindElement(By.Id("firstname"));
        private IWebElement LastNameField => driver.FindElement(By.Id("lastname"));
        private IWebElement EmailField => driver.FindElement(By.Id("email_address"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement ConfirmPasswordField => driver.FindElement(By.Id("password-confirmation"));
        private IWebElement SubmitButton => driver.FindElement(By.CssSelector(".submit > span"));

        public void EnterFirstName(string firstName) => FirstNameField.SendKeys(firstName);
        public void EnterLastName(string lastName) => LastNameField.SendKeys(lastName);
        public void EnterEmail(string email) => EmailField.SendKeys(email);
        public void EnterPassword(string password) => PasswordField.SendKeys(password);
        public void ConfirmPassword(string password) => ConfirmPasswordField.SendKeys(password);
        public void Submit() => SubmitButton.Click();
    }
}
