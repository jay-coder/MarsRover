using System;
using Zip.CodingTest.Core.Enums;
using Zip.CodingTest.Core.Interfaces;

namespace Zip.CodingTest.Core.Models
{
    public class Rover
    {
        public Plateau Plateau { get; set; }
        public Coordinate Current { get; set; }
        public EnumFacingDirection FacingDirection { get; set; }
        
        public Rover(Coordinate coordinate, EnumFacingDirection facingDirection)
        {
            Current = coordinate;
            FacingDirection = facingDirection;
        }
        
        public override string ToString() {
            return $"{Current.X} {Current.Y} {FacingDirection.ToString()}";
        }
    }
}
