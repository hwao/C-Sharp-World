using World;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using World.Map.Field;

namespace TestWorld
{
    
    
    /// <summary>
    ///This is a test class for WorldMapTest and is intended
    ///to contain all WorldMapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WorldMapTest
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
        ///A test for getField
        ///</summary>
        [TestMethod()]
        public void getFieldTest()
        {
            int width = 2; 
            int height = 2;
            Game1 Game = new Game1();
            WorldMap target = new WorldMap(width, height, Game); // TODO: Initialize to an appropriate value

            iField actual;

            // Element istniejacy
            actual = target.getField(0,0);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);

            // Element 'poza mapa' - gorna granica
            actual = target.getField(width+2,height+2);
            Assert.AreEqual(width-1, actual.X);
            Assert.AreEqual(height-1, actual.Y);

            // Element 'poza mapa' - dolna granica
            actual = target.getField(-1, -1);
            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);
        }
    }
}
