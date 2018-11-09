using Components.Common;
using Components.Units;
using Unity.Entities;

namespace Builders.Unit
{
    public class UnitBuilder
    {
        private readonly EntityArchetype _unitArchetype;
        private readonly UnitConfiguration _unitConfiguration;
        private readonly UnitData _unitData;

        public UnitBuilder(UnitConfiguration unitConfiguration, UnitData unitData)
        {
            _unitConfiguration = unitConfiguration;
            _unitData = unitData;
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            _unitArchetype = entityManager.CreateArchetype(
                typeof(Components.Units.Unit),
                typeof(Position2D),
                typeof(Heading2D),
                typeof(Faction),
                typeof(Attack),
                typeof(Range),
                typeof(Health),
                typeof(MoveSpeed)
            );
        }

        public void Build()
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            Entity unit = entityManager.CreateEntity(_unitArchetype);
            entityManager.SetComponentData(unit, GetUnitComponent(_unitData));
            entityManager.SetComponentData(unit, GetPositionComponent(_unitData));
            entityManager.SetComponentData(unit, GetHeadingComponent(_unitData));
            entityManager.SetComponentData(unit, GetFactionComponent(_unitData));
            entityManager.SetComponentData(unit, GetAttackComponent(_unitConfiguration));
            entityManager.SetComponentData(unit, GetRangeComponent(_unitConfiguration));
            entityManager.SetComponentData(unit, GetHealthComponent(_unitConfiguration));
            entityManager.SetComponentData(unit, GetMoveSpeedComponent(_unitConfiguration));
        }

        private Faction GetFactionComponent(UnitData unitData)
        {
            return new Faction
            {
                Value = unitData.Faction.Value
            };
        }

        private MoveSpeed GetMoveSpeedComponent(UnitConfiguration unitConfiguration)
        {
            return new MoveSpeed
            {
                Value = unitConfiguration.MoveSpeed.Value
            };
        }

        private Health GetHealthComponent(UnitConfiguration unitConfiguration)
        {
            return new Health
            {
                Value = unitConfiguration.Health.Value
            };
        }

        private Position2D GetPositionComponent(UnitData unitData)
        {
            return new Position2D
            {
                Value = unitData.Position.Value
            };
        }
        
        private Heading2D GetHeadingComponent(UnitData unitData)
        {
            return new Heading2D
            {
                Value = unitData.Heading.Value
            };
        }

        private Components.Units.Unit GetUnitComponent(UnitData unitData)
        {
            return new Components.Units.Unit {Path = unitData.Unit.Path};
        }

        private Attack GetAttackComponent(UnitConfiguration unitConfiguration)
        {
            return new Attack
            {
                Damage = unitConfiguration.Attack.Damage,
                NextAvailableAttack = 0,
                Rate = unitConfiguration.Attack.Rate
            };
        }
        
        private Range GetRangeComponent(UnitConfiguration unitConfiguration)
        {
            return new Range
            {
                Value = unitConfiguration.Range.Value
            };
        }
    }
}