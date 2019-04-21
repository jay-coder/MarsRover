using System;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Interfaces;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Services
{
    public class RoverService : IRoverService
    {
        private IValidatorService _validatorService;
        public RoverService(IValidatorService validatorService)
        {
            _validatorService = validatorService;
        }
        public Rover BatchDrive(Rover rover, string commands)
        {
            if(!_validatorService.ValidateRoverCommands(commands))
            {
                return null;
            }
            foreach(char cmd in commands)
            {
                Drive(rover, cmd.ToString());
            }
            return rover;
        }
        public void Drive(Rover rover, string command)
        {
            EnumDriveDirection driveDirection = EnumDriveDirection.M;
            if(Enum.TryParse(command, out driveDirection))
            {
                switch(driveDirection)
                {
                    case EnumDriveDirection.M:
                        Move(rover);
                        break;
                    case EnumDriveDirection.R:
                        TurnRight(rover);
                        break;
                    case EnumDriveDirection.L:
                        TurnLeft(rover);
                        break;
                }
            }
        }

        public void Move(Rover rover)
        {
            var currentCoordinate = rover.Current;
            switch(rover.FacingDirection)
            {
                case EnumFacingDirection.N:
                    currentCoordinate.Y ++;
                    break;
                case EnumFacingDirection.E:
                    currentCoordinate.X ++;
                    break;
                case EnumFacingDirection.S:
                    currentCoordinate.Y --;
                    break;
                case EnumFacingDirection.W:
                    currentCoordinate.X --;
                    break;
            }
            if(rover.Plateau != null && !_validatorService.ValidateBoundary(currentCoordinate, rover.Plateau.UpperRight))
            {
                throw new ArgumentException($"Rover instructions will drive rover out of boundary (0,0) - ({rover.Plateau.UpperRight.X},{rover.Plateau.UpperRight.Y})");
            }
            rover.Current = currentCoordinate;
        }

        public void TurnRight(Rover rover)
        {
            int iDirection = (int)rover.FacingDirection;
            iDirection ++;
            rover.FacingDirection = (EnumFacingDirection)(iDirection % 4);
        }

        public void TurnLeft(Rover rover)
        {
            int iDirection = (int)rover.FacingDirection;
            iDirection --;
            if(iDirection < 0)
            {
                iDirection += 4;
            }
            rover.FacingDirection = (EnumFacingDirection)(iDirection % 4);
        }
    }
}
