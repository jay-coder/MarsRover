using System.Collections.Generic;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Models;

namespace Zip.CodingTest.Core.Interfaces
{
    public interface IRoverFactory
    {
        Plateau InitPlateau(string strCoordinate);        
        Rover CreateRover(string strRover);
    }
}