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
    public class TestRoverService
    {
        private IRoverFactory _roverFactory;
        private IRoverService _roverService;

        public TestRoverService()
        {
            _roverFactory = new RoverFactory(new ValidatorService());
            _roverService =  new RoverService(new ValidatorService());
        }

        [TestMethod]
        public void TestBatchDrive1()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.BatchDrive(rover, "LMLMLMLMM");
            Assert.IsTrue(rover.ToString().Equals("1 3 N"));
        }

        [TestMethod]
        public void TestBatchDrive2()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("3 3 E");
            _roverService.BatchDrive(rover, "MMRMMRMRRM");
            Assert.IsTrue(rover.ToString().Equals("5 1 E"));
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBatchDrive3()
        {
            // Move outside of boundary
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 3 E");
            _roverService.BatchDrive(rover, "LMMM");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBatchDrive4()
        {
            // Move outside of boundary
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 3 E");
            _roverService.BatchDrive(rover, "LLMM");
        }

        [TestMethod]
        public void TestDrive1()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.Drive(rover, "L");
            _roverService.Drive(rover, "M");
            Assert.IsTrue(rover.ToString().Equals("0 2 W"));
        }

        [TestMethod]
        public void TestDrive2()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.Drive(rover, "R");
            _roverService.Drive(rover, "M");
            _roverService.Drive(rover, "M");

            Assert.IsTrue(rover.ToString().Equals("3 2 E"));
        }

        [TestMethod]
        public void TestTurnLeft1()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.TurnLeft(rover);
            Assert.IsTrue(rover.ToString().Equals("1 2 W"));
        }

        [TestMethod]
        public void TestTurnLeft2()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.TurnLeft(rover);
            _roverService.TurnLeft(rover);
            Assert.IsTrue(rover.ToString().Equals("1 2 S"));
        }

        [TestMethod]
        public void TestTurnLeft3()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.TurnLeft(rover);
            _roverService.TurnLeft(rover);
            _roverService.TurnLeft(rover);
            _roverService.TurnLeft(rover);
            Assert.IsTrue(rover.ToString().Equals("1 2 N"));
        }

        [TestMethod]
        public void TestTurnRight1()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("3 3 E");
            _roverService.TurnRight(rover);
            Assert.IsTrue(rover.ToString().Equals("3 3 S"));
        }

        [TestMethod]
        public void TestTurnRight2()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("3 3 E");
            _roverService.TurnRight(rover);
            _roverService.TurnRight(rover);
            Assert.IsTrue(rover.ToString().Equals("3 3 W"));
        }

        [TestMethod]
        public void TestTurnRight3()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("3 3 E");
            _roverService.TurnRight(rover);
            _roverService.TurnRight(rover);
            _roverService.TurnRight(rover);
            _roverService.TurnRight(rover);
            Assert.IsTrue(rover.ToString().Equals("3 3 E"));
        }

        [TestMethod]
        public void TestMove1()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 N");
            _roverService.Move(rover);
            Assert.IsTrue(rover.ToString().Equals("1 3 N"));
        }

        [TestMethod]
        public void TestMove2()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("1 2 W");
            _roverService.Move(rover);
            Assert.IsTrue(rover.ToString().Equals("0 2 W"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMove3()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("5 5 N");
            _roverService.Move(rover);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMove4()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("5 5 E");
            _roverService.Move(rover);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMove5()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("0 0 S");
            _roverService.Move(rover);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMove6()
        {
            _roverFactory.InitPlateau("5 5");
            var rover = _roverFactory.CreateRover("0 0 W");
            _roverService.Move(rover);
        }
    }
}
