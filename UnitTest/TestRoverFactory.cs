using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Factories;
using Zip.CodingTest.Core.Interfaces;
using Zip.CodingTest.Core.Models;
using Zip.CodingTest.Core.Services;

namespace Zip.CodingTest.UnitTest
{
    [TestClass]
    public class TestRoverFactory
    {
        private IRoverFactory _roverFactory;

        public TestRoverFactory()
        {
            _roverFactory = new RoverFactory(new ValidatorService());
        }

        [TestMethod]
        public void TestInit1()
        {
            var plateau = _roverFactory.InitPlateau("-5 -5");
            Assert.IsNull(plateau);
        }

        [TestMethod]
        public void TestInit2()
        {
            var plateau = _roverFactory.InitPlateau("N 5");
            Assert.IsNull(plateau);
        }

        [TestMethod]
        public void TestInit3()
        {
            var plateau = _roverFactory.InitPlateau("5 N");
            Assert.IsNull(plateau);
        }

        [TestMethod]
        public void TestInit4()
        {
            var plateau = _roverFactory.InitPlateau("5 5");
            Assert.IsTrue(
                plateau.LowerLeft.X == 0 && plateau.LowerLeft.Y == 0 &&
                plateau.UpperRight.X == 5 && plateau.UpperRight.Y == 5
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateRover1()
        {
            // Plateau needs to be intialized first
            var rover = _roverFactory.CreateRover("1 2 N");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRover2()
        {
            // Outside boundary
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("-1 -1 E");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRover3()
        {
            // Outside boundary
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("5 6 E");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRover4()
        {
            // Outside boundary
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("6 5 E");
        }

        [TestMethod]
        public void TestCreateRover5()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("2 2 Z");
            Assert.IsNull(rover);
        }

        [TestMethod]
        public void TestCreateRover6()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("2 2 2");
            Assert.IsNull(rover);
        }

        [TestMethod]
        public void TestCreateRover7()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("2.5 2.5 E");
            Assert.IsNull(rover);
        }
    }
}
