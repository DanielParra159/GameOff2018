using Components.Common;
using Components.Units;
using Unity.Entities;

namespace Builders.Unit
{
    public class UnitBuilder
    {
        public void Build(UnitConfiguration unitConfiguration, SpawnInfo spawnInfo, Entity entity)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            entityManager.AddComponentData(entity, GetUnitComponent(spawnInfo));
            entityManager.AddComponentData(entity, GetPositionComponent(spawnInfo));
            entityManager.AddComponentData(entity, GetHeadingComponent(spawnInfo));
            entityManager.AddComponentData(entity, GetFactionComponent(spawnInfo));
            entityManager.AddComponentData(entity, GetAttackComponent(unitConfiguration));
            entityManager.AddComponentData(entity, GetRangeComponent(unitConfiguration));
            entityManager.AddComponentData(entity, GetHealthComponent(unitConfiguration));
            entityManager.AddComponentData(entity, GetMoveSpeedComponent(unitConfiguration));
            entityManager.AddComponentData(entity, new AnimationData{SetIsWaling = true});
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