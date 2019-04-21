using System;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Interfaces;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Factories
{
    public class RoverFactory : IRoverFactory
    {
        private Plateau _plateau = null;
        private IValidatorService _validatorService;
        public RoverFactory(IValidatorService validatorService)
        {
            _validatorService = validatorService;
        }
        public Rover CreateRover(string strRover)
        {
            var rCoordinate = new Coordinate(0, 0);
            var rFacingDirection = EnumFacingDirection.N;
            if(!_validatorService.ValidateRover(strRover, out rCoordinate, out rFacingDirection))
            {
                return null;
            }
            var rover = new Rover(rCoordinate, rFacingDirection);
            if(_plateau == null)
            {
                throw new ArgumentNullException("Plateau has not been intialized yet");
            }
            else if(!_validatorService.ValidateBoundary(rCoordinate, _plateau.UpperRight))
            {
                throw new ArgumentException(
                    $"Rover coordinate ({rCoordinate.X},{rCoordinate.Y}) is outside the boundary (0,0) - ({_plateau.UpperRight.X},{_plateau.UpperRight.Y})"
                );
            }
            rover.Plateau = _plateau;
            return rover;
        }

        public Plateau InitPlateau(string strCoordinate)
        {
            Coordinate pCoordinate = new Coordinate(0, 0);
            if(!_validatorService.ValidateCoordinate(strCoordinate, out pCoordinate))
            {
                return null;
            }
            else
            {
                _plateau = new Plateau(){
                    UpperRight = pCoordinate
                };
            }
            return _plateau;
        }
    }
}