using OpenQA.Selenium;

public class RecenzijePage
{
    private IWebDriver driver;

    public RecenzijePage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void ClickOnReviewButton()
    {
        driver.FindElement(By.CssSelector(".product-item:nth-child(1) .reviews-actions span")).Click();
    }

    public void EnterSummary(string summary)
    {
        driver.FindElement(By.Id("summary_field")).SendKeys(summary);
    }

    public void EnterReview(string review)
    {
        driver.FindElement(By.Id("review_field")).SendKeys(review);
    }


    public void EnterNickname(string nickname)
    {
        driver.FindElement(By.Id("nickname_field")).SendKeys(nickname);
    }

    public void SubmitReview()
    {
        driver.FindElement(By.CssSelector(".submit > span")).Click();
    }

    public bool IsReviewSubmittedSuccessfully()
    {
        var elements = driver.FindElements(By.XPath("//*[contains(text(),'You submitted your review for moderation.')]"));
        return elements.Count > 0;
    }
}
