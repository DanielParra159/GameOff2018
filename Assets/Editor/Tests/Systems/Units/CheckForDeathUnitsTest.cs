using Systems.Units;
using Components.Common;
using Components.Units;
using NUnit.Framework;

namespace Editor.Tests.Systems.Units
{
    public class CheckForDeathUnitsTest : BaseEcsTest
    {
        [Test]
        public void WhenHealthIsZeroThenAddDyingComponent()
        {
            // Arrange
            var entity = EntityManager.CreateEntity(
                typeof(Unit),
                typeof(Health),
                typeof(AnimationData)
            );
            EntityManager.SetComponentData(entity, new Health {Value = 0});

            // Act
            World.CreateManager<CheckForDeathUnits>().Update();

            // Assert
            Assert.IsTrue(EntityManager.HasComponent<Dying>(entity));
        }
        
        [Test]
        public void WhenHealthIsGreaterThanZeroThenDoNotAddDyingComponent()
        {
            // Arrange
            var entity = EntityManager.CreateEntity(
                typeof(Unit),
                typeof(Health),
                typeof(AnimationData)
            );
            EntityManager.SetComponentData(entity, new Health {Value = 1});

            // Act
            World.CreateManager<CheckForDeathUnits>().Update();

            // Assert
            Assert.IsFalse(EntityManager.HasComponent<Dying>(entity));
        }
        
        [Test]
        public void WhenTheUnitHasDyingComponentDoNothing()
        {
            // Arrange
            EntityManager.CreateEntity(
                typeof(Unit),
                typeof(Health),
                typeof(AnimationData),
                typeof(Dying)
            );

            // Act
            World.CreateManager<CheckForDeathUnits>().Update();
        }
    }
}