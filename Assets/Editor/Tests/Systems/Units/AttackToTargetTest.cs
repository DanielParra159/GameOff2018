using Systems.Units;
using Components.Common;
using Components.Units;
using NUnit.Framework;
using Unity.Entities;

namespace Editor.Tests.Systems.Units
{
    public class AttackToTargetTest : BaseEcsTest
    {
        private Entity _entity;
        private Entity _opponent;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            
            _entity = EntityManager.CreateEntity(
                typeof(Target),
                typeof(Attack)
            );
            _opponent = EntityManager.CreateEntity(
                typeof(Damage)
            );
            EntityManager.SetComponentData(_entity, new Target {Entity = _opponent});
        }
        
        [Test]
        public void WhenAttackIsProcessedRemoveTarget()
        {
            // Arrange
            EntityManager.SetComponentData(_entity, new Attack {Damage = 10, IsReady = false});

            // Act
            World.CreateManager<AttackToTarget>().Update();

            // Assert
            Assert.IsFalse(EntityManager.HasComponent<Target>(_entity));
        }

        [Test]
        public void WhenAttackIsNotReadyDoNothing()
        {
            // Arrange
            EntityManager.SetComponentData(_entity, new Attack {Damage = 10, IsReady = false});

            // Act
            World.CreateManager<AttackToTarget>().Update();

            // Assert
            var damageBuffer = EntityManager.GetBuffer<Damage>(_opponent);
            Assert.AreEqual(0, damageBuffer.Length);
        }
        
        [Test]
        public void WhenAttackIsReadyAddTheCorrectDamage()
        {
            // Arrange
            const int damage = 10;
            EntityManager.SetComponentData(_entity, new Attack {Damage = damage, IsReady = true});

            // Act
            World.CreateManager<AttackToTarget>().Update();

            // Assert
            var damageBuffer = EntityManager.GetBuffer<Damage>(_opponent);
            Assert.AreEqual(damage, damageBuffer[0].Value);
        }
        
        [Test]
        public void WhenAttackIsReadyAddOneDamageComponent()
        {
            // Arrange
            EntityManager.SetComponentData(_entity, new Attack {Damage = 10, IsReady = true});

            // Act
            World.CreateManager<AttackToTarget>().Update();

            // Assert
            var damageBuffer = EntityManager.GetBuffer<Damage>(_opponent);
            Assert.AreEqual(1, damageBuffer.Length);
        }
    }
}