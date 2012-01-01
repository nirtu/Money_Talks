using Money_Talks.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace TestProject1
{
    /// <summary>
    ///This is a test class for TransactionControllerTest and is intended
    ///to contain all TransactionControllerTest Unit Tests
    ///</summary>
    

    [TestClass()]
    public class TransactionControllerTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

//-------------------------------------------------------------------------------------------------

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Tests for balanceCheckAdd
        ///</summary>
        
        
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckAddTest1()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 900;
            int transactionAmount = 996;
            int expected = 1896;
            int actual;
            actual = target.balanceCheckAdd(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckAddTest2()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 100;
            int transactionAmount = 898;
            int expected = 998;
            int actual;
            actual = target.balanceCheckAdd(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckAddTest3()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 99;
            int transactionAmount = 333;
            int expected = 432;
            int actual;
            actual = target.balanceCheckAdd(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckAddTest4()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 0;
            int transactionAmount = 0;
            int expected = 0;
            int actual;
            actual = target.balanceCheckAdd(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }

//-------------------------------------------------------------------------------------------------

        /// <summary>
        ///Tests for balanceCheckRemove
        ///</summary>
        
        
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckRemoveTest1()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 1;
            int transactionAmount = 544;
            int expected = -543;
            int actual;
            actual = target.balanceCheckRemove(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckRemoveTest2()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 1000;
            int transactionAmount = 432;
            int expected = 568;
            int actual;
            actual = target.balanceCheckRemove(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckRemoveTest3()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 246;
            int transactionAmount = 600;
            int expected = -354;
            int actual;
            actual = target.balanceCheckRemove(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Nataly & Yaniv\\Documents\\Visual Studio 2010\\Projects\\Money_Talks\\Money_Talks", "/")]
        [UrlToTest("http://localhost:11021/")]
        [DeploymentItem("Money_Talks.dll")]
        public void balanceCheckRemoveTest4()
        {
            TransactionController_Accessor target = new TransactionController_Accessor();
            int balance = 0;
            int transactionAmount = 0;
            int expected = 0;
            int actual;
            actual = target.balanceCheckRemove(balance, transactionAmount);
            Assert.AreEqual(expected, actual);
        }
    }
}
