using System;
using System.Text.RegularExpressions;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Interfaces;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool ValidateBoundary(Coordinate coordinate, Coordinate urCoordinate)
        {
            return coordinate.X >=0 && coordinate.X <= urCoordinate.X &&
                coordinate.Y >=0 && coordinate.Y <= urCoordinate.Y;
        }
        public bool ValidateCoordinate(string strCoordinate, out Coordinate coordinate)
        {
            bool isValid = false;
            coordinate = new Coordinate(0, 0);
            if(!string.IsNullOrEmpty(strCoordinate))
            {
                var coordArray = strCoordinate.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if(coordArray.Length == 2)
                {
                    int x = 0;
                    int y = 0;
                    if(
                        int.TryParse(coordArray[0], out x) && 
                        int.TryParse(coordArray[1], out y) &&
                        x > 0 && 
                        y > 0
                    )
                    {
                        coordinate.X = x;
                        coordinate.Y = y;
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        public bool ValidateRover(string strRover, out Coordinate coordinate, out EnumFacingDirection facingDirection)
        {
            bool isValid = false;
            coordinate = new Coordinate(0, 0);
            facingDirection = EnumFacingDirection.N;
            if(!string.IsNullOrEmpty(strRover))
            {
                var coordArray = strRover.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if(coordArray.Length == 3)
                {
                    int x = 0;
                    int y = 0;
                    if(
                        int.TryParse(coordArray[0], out x) &&
                        int.TryParse(coordArray[1], out y) &&
                        Enum.IsDefined(typeof(EnumFacingDirection), coordArray[2]) &&
                        Enum.TryParse(coordArray[2], out facingDirection)
                    )
                    {
                        coordinate.X = x;
                        coordinate.Y = y;
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        public bool ValidateRoverCommands(string strCommands)
        {
            var match = Regex.Match(strCommands, @"\b(L|M|R)*\b", RegexOptions.IgnoreCase);
            if(match.Success)
            {
                return match.Value.Equals(strCommands, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}