using OpenQA.Selenium;
using System;
using static Compunnel.TestLibrary;

namespace Compunnel.Pages
{
    class CareerPage
    {
        public static IWebElement CareerLink()
        {
            IWebElement footerMenu = Driver.FindElement(By.ClassName("footer-menu-container"));
            ScrollIntoView(footerMenu);
            return footerMenu.FindElement(By.PartialLinkText("Careers(link is external)")).FindElement(By.XPath(".."));
        }

        public static IWebElement SearchKeyword()
        {
            return WaitUntilElementExists(By.ClassName("search-keyword"));
        }

        public static IWebElement SearchLocation()
        {
            return WaitUntilElementExists(By.ClassName("search-location"));
        }
        public static IWebElement SearchRadius()
        {
            return WaitUntilElementExists(By.ClassName("search-radius"));
        }

        public static IWebElement SearchSubmit()
        {
            return WaitUntilElementExists(By.ClassName("search-form__submit"));
        }

        public static IWebElement FindJobElmentById(String jobId)
        {
            IWebElement searchResultSection = WaitUntilElementExists(By.Id("search-results-list"));
            ScrollIntoView(searchResultSection);
            return searchResultSection.FindElement(By.XPath($"//li/a[@data-job-id='{jobId}']"));
        }

        public static IWebElement JobPosition(IWebElement jobElement)
        {
            return jobElement.FindElement(By.TagName("h2"));
        }

        public static IWebElement JobLocation(IWebElement jobElement)
        {
            return jobElement.FindElement(By.ClassName("job-location"));
        }

        public static IWebElement JobDatePosted(IWebElement jobElement)
        {
            return jobElement.FindElement(By.ClassName("job-date-posted"));
        }
    }
}
