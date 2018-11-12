using Systems.Units;
using Components.Common;
using Components.Units;
using NUnit.Framework;
using Unity.Entities;

namespace Editor.Tests.Systems.Units
{
    public class TargetDetectionTest : BaseTest
    {
        private EntityArchetype GetTargetDetectionArchetype()
        {
            return EntityManager.CreateArchetype(
                typeof(Unit),
                typeof(Range),
                typeof(Position2D),
                typeof(Faction),
                typeof(AnimationData)
            );
        }

        [Test]
        public void WhenTheOpponentIsInTheSamePathAndInOfRangeThenAddTargetComponent()
        {
            // Arrange               
            var entity = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(entity, new Unit {Path = 0});
            EntityManager.SetComponentData(entity, new Range {Value = 2});
            EntityManager.SetComponentData(entity, new Position2D {Value = 1});
            EntityManager.SetComponentData(entity, new Faction {Value = 0});
            var opponent = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(opponent, new Unit {Path = 0});
            EntityManager.SetComponentData(opponent, new Range {Value = 2});
            EntityManager.SetComponentData(opponent, new Position2D {Value = 0});
            EntityManager.SetComponentData(opponent, new Faction {Value = 1});

            // Act
            World.CreateManager<TargetDetection>().Update();

            // Assert
            Assert.IsTrue(EntityManager.HasComponent<Target>(entity));
        }
        
        [Test]
        public void WhenTheOpponentIsInTheSamePathAndOutOfRangeThenDoNotAddTargetComponent()
        {
            // Arrange               
            var entity = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(entity, new Unit {Path = 0});
            EntityManager.SetComponentData(entity, new Range {Value = 2});
            EntityManager.SetComponentData(entity, new Position2D {Value = 1});
            EntityManager.SetComponentData(entity, new Faction {Value = 0});
            var opponent = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(opponent, new Unit {Path = 0});
            EntityManager.SetComponentData(opponent, new Range {Value = 2});
            EntityManager.SetComponentData(opponent, new Position2D {Value = 5});
            EntityManager.SetComponentData(opponent, new Faction {Value = 1});

            // Act
            World.CreateManager<TargetDetection>().Update();

            // Assert
            Assert.IsFalse(EntityManager.HasComponent<Target>(entity));
        }
        
        [Test]
        public void WhenTheOpponentIsInOtherPathThenDoNotAddTargetComponent()
        {
            // Arrange               
            var entity = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(entity, new Unit {Path = 0});
            EntityManager.SetComponentData(entity, new Range {Value = 2});
            EntityManager.SetComponentData(entity, new Position2D {Value = 1});
            EntityManager.SetComponentData(entity, new Faction {Value = 0});
            var opponent = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(opponent, new Unit {Path = 1});
            EntityManager.SetComponentData(opponent, new Range {Value = 2});
            EntityManager.SetComponentData(opponent, new Position2D {Value = 0});
            EntityManager.SetComponentData(opponent, new Faction {Value = 1});

            // Act
            World.CreateManager<TargetDetection>().Update();

            // Assert
            Assert.IsFalse(EntityManager.HasComponent<Target>(entity));
        }
        
        [Test]
        public void WhenOpponentDetectedThenStopWalking()
        {
            // Arrange               
            var entity = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(entity, new Unit {Path = 0});
            EntityManager.SetComponentData(entity, new Range {Value = 2});
            EntityManager.SetComponentData(entity, new Position2D {Value = 1});
            EntityManager.SetComponentData(entity, new Faction {Value = 0});
            var opponent = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(opponent, new Unit {Path = 0});
            EntityManager.SetComponentData(opponent, new Range {Value = 2});
            EntityManager.SetComponentData(opponent, new Position2D {Value = 0});
            EntityManager.SetComponentData(opponent, new Faction {Value = 1});

            // Act
            World.CreateManager<TargetDetection>().Update();

            // Assert
            Assert.IsFalse(EntityManager.GetComponentData<AnimationData>(entity).SetIsWaling);
        }
        
        [Test]
        public void WhenOpponentNotDetectedThenSetWalking()
        {
            // Arrange               
            var entity = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(entity, new Unit {Path = 0});
            EntityManager.SetComponentData(entity, new Range {Value = 2});
            EntityManager.SetComponentData(entity, new Position2D {Value = 1});
            EntityManager.SetComponentData(entity, new Faction {Value = 0});
            EntityManager.SetComponentData(entity, new AnimationData {SetIsWaling = false});
            var opponent = EntityManager.CreateEntity(GetTargetDetectionArchetype());
            EntityManager.SetComponentData(opponent, new Unit {Path = 0});
            EntityManager.SetComponentData(opponent, new Range {Value = 2});
            EntityManager.SetComponentData(opponent, new Position2D {Value = 5});
            EntityManager.SetComponentData(opponent, new Faction {Value = 1});

            // Act
            World.CreateManager<TargetDetection>().Update();

            // Assert
            Assert.IsTrue(EntityManager.GetComponentData<AnimationData>(entity).SetIsWaling);
        }
    }
}