using Systems.Player;
using Components.Player;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Systems.Player
{
    [TestFixture]
    public class RechargeEnergyTest : BaseEcsTest
    {
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(0, 5)]
        [TestCase(3, 5)]
        public void RechargeEnergy(int initialEnergy, int energyPerSecond)
        {
            // Arrange
            var entity = EntityManager.CreateEntity(
                typeof(Energy),
                typeof(EnergyRecharger)
            );
            EntityManager.SetComponentData(entity, new Energy {CurrentValue = initialEnergy, MaxValue = 10});
            EntityManager.SetComponentData(entity, new EnergyRecharger {EnergyPerSecond = energyPerSecond});

            // Act
            World.CreateManager<RechargeEnergy>().Update();

            // Assert
            var energyComponentData = EntityManager.GetComponentData<Energy>(entity);
            Assert.AreEqual(initialEnergy + energyPerSecond * Time.deltaTime, energyComponentData.CurrentValue);
        }
        
        [Test]
        public void WhenTheEnergyIsFullDoNotRecharge()
        {
            // Arrange
            var entity = EntityManager.CreateEntity(
                typeof(Energy),
                typeof(EnergyRecharger)
            );
            var maxEnergy = 10;
            EntityManager.SetComponentData(entity, new Energy {CurrentValue = maxEnergy, MaxValue = maxEnergy});
            EntityManager.SetComponentData(entity, new EnergyRecharger {EnergyPerSecond = 1});

            // Act
            World.CreateManager<RechargeEnergy>().Update();

            // Assert
            var energyComponentData = EntityManager.GetComponentData<Energy>(entity);
            Assert.AreEqual(energyComponentData.MaxValue, energyComponentData.CurrentValue);
        }
    }
}

