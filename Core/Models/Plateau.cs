namespace Zip.CodingTest.Core.Models
{
    public class Plateau
    {
        public Coordinate LowerLeft { get; } = new Coordinate(0, 0);
        public Coordinate UpperRight { get; set; }
    }
}
