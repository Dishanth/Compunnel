using OpenQA.Selenium;
using System.Linq;
using static Compunnel.TestLibrary;

namespace Compunnel.Pages
{
    public class JobDescriptionPage
    {
        private static IWebElement JobDescriptionSection()
        {
            IWebElement jobDescptionPage = Driver.FindElement(By.Id("content"));
            WaitForPageLoad();
            ScrollIntoView(jobDescptionPage);
            return jobDescptionPage.FindElement(By.ClassName("job-description"));
        }

        public static IWebElement JobTitle()
        {
            return JobDescriptionSection().FindElement(By.ClassName("job-description__heading"));
        }

        public static IWebElement JobLocation()
        {
            return JobDescriptionSection().FindElement(By.ClassName("job-location"));
        }

        public static IWebElement JobId()
        {
            return JobDescriptionSection().FindElement(By.ClassName("job-id"));
        }

        public static IWebElement JobDescription()
        {
            return JobDescriptionSection().FindElement(By.ClassName("ats-description"));
        }

        public static IWebElement GetJobDescriptionByParagraphNum(int paragraphNum)
        {
            var paragramItems = JobDescription().FindElements(By.TagName("p")).ToList();
            paragramItems.RemoveAll(x => x.Text == ""); // This will remove empty paragram tags
            return ScrollIntoView(paragramItems[paragraphNum - 1]);
        }

        public static IWebElement GetJobDescriptionBulletPoint(string bulletHeader, int bulletNum)
        {
            bool isMatch = false;
            var descItems = JobDescription().FindElements(By.XPath("div")).FirstOrDefault().FindElements(By.XPath("*"));
            foreach (var item in descItems)
            {
                if (isMatch)
                {
                    var bulletItems = item.FindElements(By.TagName("li"));
                    return ScrollIntoView(bulletItems[bulletNum - 1]);
                }
                if (item.Text.Equals(bulletHeader))
                    isMatch = true;
            }
            return null;
        }

        public static IWebElement GetJobDescriptionRequirement(int requirementSection, int requirementNum)
        {
            var items = JobDescription().FindElements(By.XPath("div")).LastOrDefault()
                        .FindElements(By.XPath("//span/div/ul"))
                        [requirementSection - 1].FindElements(By.TagName("li"));
            return ScrollIntoView(items[requirementNum - 1]);
        }

        public static IWebElement ApplyNowButton()
        {
            IWebElement applyNow = JobDescriptionSection().FindElement(By.XPath("//a[@class ='button job-apply bottom']"));
            return ScrollIntoView(applyNow);
        }
    }
}
