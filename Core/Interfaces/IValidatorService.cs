using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Interfaces
{
    public interface IValidatorService
    {
        bool ValidateBoundary(Coordinate coordinate, Coordinate urCoordinate);
        bool ValidateCoordinate(string strCoordinate, out Coordinate coordinate);
        bool ValidateRover(string strRover, out Coordinate coordinate, out EnumFacingDirection facingDirection);
        bool ValidateRoverCommands(string strCommands);
    }
}