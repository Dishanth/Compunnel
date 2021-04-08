using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;

namespace Compunnel
{
    public class TestLibrary
    {
        public static IWebDriver Driver { get; set; }

        #region BrowserSetup
        public static void InitializeBrowser()
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
                Driver.Manage().Window.Maximize();
            }
        }
        #endregion

        #region BrowserCleanup
        public static void CloseDriver()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver = null;
            }

            Process[] allProcesses = Process.GetProcessesByName("chromedriver");

            foreach (var process in allProcesses)
            {
                process.Kill();
            }
        }
        #endregion

        #region WaitFunctions

        public static bool WaitForPageLoad(int timeout = 15)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
                return true;
            }
            catch (Exception ex)
            {
                Console.Write($"[ERROR] Failed to load page in allocated time, More info " + ex.Message);

            }
            return false;
        }

        public static IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (Exception ex)
            {
                Console.Write($"[ERROR] Occured in :" + elementLocator + " More info " + ex.Message);
                return null;
            }
        }

        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementLocator));
            }
            catch (Exception ex)
            {
                Console.Write($"[ERROR] Occured in :" + elementLocator + " More info " + ex.Message);
                return null;
            }
        }

        public static IWebElement WaitUntilElementClassExists(IWebElement element, string className)
        {
            int count = 0;
            while (count < 10)
            {
                if (!element.GetAttribute("class").Contains(className))
                {
                    return element.FindElement(By.ClassName(className));
                }
                Thread.Sleep(500);
                count++;
            }
            return null;
        }

        public static bool WaitUntilElementIsVisible(By elementLocator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Occured in :" + elementLocator + " More info " + ex.Message);
                return false;
            }
        }

        public static bool WaitUntilTitleContains(string titleName, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(titleName));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Occured in titleName:" + titleName + " More info " + ex.Message);
                return false;
            }
        }
        public static bool WaitUntilElementIsInvisible(By elementLocator, int timeout = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Occured in :" + elementLocator + " More info " + ex.Message);
                throw;
            }
        }
        #endregion

        public static IWebElement ScrollIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return element;
        }
    }
}
    