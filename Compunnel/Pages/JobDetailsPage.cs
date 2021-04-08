using OpenQA.Selenium;
using System.Linq;
using static Compunnel.TestLibrary;

namespace Compunnel.Pages
{
    public class JobDetailsPage
    {
        public static IWebElement JobDetailsContainer()
        {
            return WaitUntilElementExists(By.ClassName("jobdetails-container"));
        }

        public static IWebElement JobTitle()
        {
            return WaitUntilElementExists(By.ClassName("jobTitle"));
        }

        public static IWebElement JobId()
        {
            return WaitUntilElementExists(By.ClassName("jobnum"));
        }
        public static IWebElement JobLocation()
        {
            return WaitUntilElementExists(By.ClassName("resultLocationLink"));
        }

        public static IWebElement GetJobDescriptionByParagraphNum(int paragraphNum)
        {
            var leftSection = WaitUntilElementExists(By.Id("field2_left"), 10);
            var paragramItems = leftSection.FindElement(By.ClassName("content")).FindElements(By.TagName("p")).ToList();
            paragramItems.RemoveAll(x => x.Text.Trim() == ""); // This will remove empty paragram tags
            return ScrollIntoView(paragramItems[paragraphNum - 1]);
        }

        public static IWebElement ReturnToJobSearch()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(document.body.scrollHeight, 0)");
            return WaitUntilElementClickable(By.XPath("//span[contains(text(),'Return to Job Search')]/.."));
        }

        public static IWebElement SearchForJobs()
        {
            return WaitUntilElementExists(By.ClassName("search-btn"));
        }


        public static IWebElement ClosePopOverContent()
        {
            var popoverContent = WaitUntilElementExists(By.ClassName("popover-content"), 10);
            return popoverContent.FindElement(By.ClassName("closebutton"));
        }
    }
}
