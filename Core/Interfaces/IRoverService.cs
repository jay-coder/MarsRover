using System.Collections.Generic;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Interfaces
{
 
    public interface IRoverService
    {
        Rover BatchDrive(Rover rover, string commands);
        void Drive(Rover rover, string command);
        void Move(Rover rover);
        void TurnRight(Rover rover);
        void TurnLeft(Rover rover);

    }
}