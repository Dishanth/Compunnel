using Compunnel.BaseTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Compunnel.TestLibrary;
using static Compunnel.Constants;
using OpenQA.Selenium;
using System.Linq;
using Compunnel.Pages;

namespace Compunnel
{
    [TestClass]
    public class Tests : BaseTest
    {
        [TestMethod]
        public void VerifyJobPostingAtCareerPage()
        {
            #region Test Data
            string jobPosition = "Senior QA Test Automation Developer / Engineer";
            string jobLocation1 = "Durham, North Carolina";
            string jobLocation2 = "Durham, NC";
            string jobId = "20-85412";
            string jobPostedDate = "12/10/2020";
            string expectedDescription = "The right candidate for this role will participate in the test automation technology development and best practice models.";
            string expectedBulletPoint = "Prepare test plans, budgets, and schedules.";
            string expectedRequirement1 = "5+ years of experience in QA automation development and scripting.";
            string expectedRequirement2 = "Selenium";
            #endregion

            // Step1: LaunchUrl
            Driver.Navigate().GoToUrl(LabCorpHomeUrl);
            WaitForPageLoad();
            Assert.AreEqual(LabCorpHomeUrl, Driver.Url, "Failed to navigate to Labcorp home Url");

            // Step2: Find and click Careers link"
            CareerPage.CareerLink().Click();
            Assert.AreEqual(2, Driver.WindowHandles.Count, "Failed to find new browser tab");            
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());  // Switch Driver focus to new tab                      
            WaitForPageLoad();
            Assert.AreEqual(LabCorpCareerUrl, Driver.Url, "Failed to navigate to Labcorp career Url");

            // Step3: Search for QA Test Automation Developer"
            CareerPage.SearchKeyword().SendKeys("QA Test Automation Developer");
            CareerPage.SearchLocation().Clear();
            CareerPage.SearchSubmit().Click();

            // Step4: Select Senior QA Test Automation Developer/Engineer – Durham, North Carolina – (posted on) 12/10/2020"            
            IWebElement jobElement = CareerPage.FindJobElmentById("3090755200");
            // Assert Job posting            
            Assert.AreEqual(jobPosition, CareerPage.JobPosition(jobElement).Text, "Position name does not match");           
            Assert.AreEqual(jobLocation1, CareerPage.JobLocation(jobElement).Text, "Position location does not match");            
            Assert.AreEqual(jobPostedDate, CareerPage.JobDatePosted(jobElement).Text, "Position date posted does not match");
            // Click Position
            jobElement.Click();

            // Step5: Confirm job title, job location, and job id (#20-85412)"           
            Assert.AreEqual(jobPosition, JobDescriptionPage.JobTitle().Text.Trim(), "Position name does not match");
            Assert.IsTrue(JobDescriptionPage.JobLocation().Text.Contains(jobLocation1), "Position location does not match");
            Assert.IsTrue(JobDescriptionPage.JobId().Text.Contains(jobId), "Position Job Id does not match");

            // Step6: Confirm first sentence of third paragraph under Description/Introduction"
            string actualDescription =  JobDescriptionPage.GetJobDescriptionByParagraphNum(3).Text;  // Get the Thrid paragraph
            Assert.IsTrue(actualDescription.Contains(expectedDescription));

            // Step7: Confirm second bullet point under Management Support as Prepare test plans, budgets, and schedules."
            string actualBulletPoint = JobDescriptionPage.GetJobDescriptionBulletPoint("Management Support", 2).Text;
            Assert.IsTrue(actualBulletPoint.Contains(expectedBulletPoint));

            // Step8: 5+ years of experience in QA automation development and scripting."
            string actualRequirement1 = JobDescriptionPage.GetJobDescriptionRequirement(1,3).Text;   //First section - 3rd line
            Assert.IsTrue(actualRequirement1.Contains(expectedRequirement1));

            // Step9: Confirm first suggested automation tool to be familiar with contains Selenium"
            string actualRequirement2 = JobDescriptionPage.GetJobDescriptionRequirement(2, 1).Text;   //Second section - 1st line
            Assert.IsTrue(actualRequirement2.Contains(expectedRequirement2));

            // Step10: Click Apply Now and confirm points 5 and 6 in the proceeding page."
            JobDescriptionPage.ApplyNowButton().Click();
            string expectedTitle = "Career Site - Self Service";
            WaitForPageLoad();
            WaitUntilTitleContains(expectedTitle);
            Assert.AreEqual(expectedTitle, Driver.Title, "Failed to match page title");

            JobDetailsPage.ClosePopOverContent().Click();
            Assert.AreEqual(jobPosition, JobDetailsPage.JobTitle().Text, "Position name does not match");
            Assert.IsTrue(JobDetailsPage.JobLocation().Text.Contains(jobLocation2), "Position location does not match");
            Assert.IsTrue(JobDetailsPage.JobId().Text.Contains(jobId), "Position Job Id does not match");

            string actualDescription1 = JobDetailsPage.GetJobDescriptionByParagraphNum(3).Text;  // Get the Thrid paragraph
            string expectedDescription1 = "The right candidate for this role will participate in the test automation technology development and best practice models.";
            Assert.IsTrue(actualDescription1.Contains(expectedDescription1));

            // Step11: Click to Return to Job Search
            JobDetailsPage.ReturnToJobSearch().Click();
            WaitForPageLoad();
            WaitUntilTitleContains(expectedTitle);
            Assert.AreEqual(expectedTitle, Driver.Title, "Failed to match page title");
            Assert.IsNotNull(JobDetailsPage.SearchForJobs(),"Failed to find search for jobs button");
        }
    }
}
