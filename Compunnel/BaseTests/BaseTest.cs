using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Compunnel.TestLibrary;


namespace Compunnel.BaseTests
{
    [TestClass]
    public class BaseTest
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            CloseDriver();
            InitializeBrowser();
        }

        [TestCleanup]
        public virtual void TestCleanUp()
        {
            CloseDriver();
        }
    }
}
