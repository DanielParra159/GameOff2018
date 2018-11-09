using Components.Common;

namespace Builders.Unit
{
    public class UnitData
    {
        public Components.Units.Unit Unit { get; }
        public Position2D Position { get; }
        public Heading2D Heading { get; }
        public Faction Faction { get; }

        public UnitData(Components.Units.Unit unit, Position2D position, Heading2D heading, Faction faction)
        {
            Unit = unit;
            Position = position;
            Heading = heading;
            Faction = faction;
        }
    }
}