using Components.Common;
using Components.Units;
using Unity.Entities;

namespace Builders.Unit
{
    public class UnitBuilder
    {
        private readonly UnitConfiguration _unitConfiguration;
        private readonly SpawnInfo _unitData;

        public UnitBuilder(UnitConfiguration unitConfiguration, SpawnInfo unitData)
        {
            _unitConfiguration = unitConfiguration;
            _unitData = unitData;
        }

        public void Build(Entity entity)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            entityManager.AddComponentData(entity, GetUnitComponent(_unitData));
            entityManager.AddComponentData(entity, GetPositionComponent(_unitData));
            entityManager.AddComponentData(entity, GetHeadingComponent(_unitData));
            entityManager.AddComponentData(entity, GetFactionComponent(_unitData));
            entityManager.AddComponentData(entity, GetAttackComponent(_unitConfiguration));
            entityManager.AddComponentData(entity, GetRangeComponent(_unitConfiguration));
            entityManager.AddComponentData(entity, GetHealthComponent(_unitConfiguration));
            entityManager.AddComponentData(entity, GetMoveSpeedComponent(_unitConfiguration));
            entityManager.AddComponentData(entity, new AnimationData{IsWaling = true});
            entityManager.AddBuffer<Damage>(entity);
        }

        
        private Position2D GetPositionComponent(SpawnInfo unitData)
        {
            return new Position2D
            {
                Value = unitData.Position
            };
        }
        
        private Heading2D GetHeadingComponent(SpawnInfo unitData)
        {
            return new Heading2D
            {
                Value = unitData.Heading
            };
        }

        private Components.Units.Unit GetUnitComponent(SpawnInfo unitData)
        {
            return new Components.Units.Unit {Path = unitData.Path};
        }
        
        private Faction GetFactionComponent(SpawnInfo unitData)
        {
            return new Faction
            {
                Value = unitData.Faction
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


        private Attack GetAttackComponent(UnitConfiguration unitConfiguration)
        {
            return new Attack
            {
                Damage = unitConfiguration.Attack.Damage,
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