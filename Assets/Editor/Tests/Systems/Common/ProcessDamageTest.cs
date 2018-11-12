using Components.Common;
using NUnit.Framework;
using Unity.Mathematics;

namespace Editor.Tests.Systems
{
    public class ProcessDamageTest : BaseTest
    {
        [TestCase(3, 1)]
        [TestCase(3, 3)]
        [TestCase(3, 4)]
        public void DoDamage(int initialHealth, int damage)
        {
            // Arrange
            var entity = EntityManager.CreateEntity(
                typeof(Health),
                typeof(Damage)
            );
            EntityManager.SetComponentData(entity, new Health {Value = initialHealth});
            var damageBuffer = EntityManager.GetBuffer<Damage>(entity);
            damageBuffer.Add(new Damage
            {
                Value = damage
            });

            // Act
            World.CreateManager<global::Systems.Common.ProcessDamage>().Update();

            // Assert
            Assert.AreEqual(math.max(0, initialHealth - damage),
                EntityManager.GetComponentData<Health>(entity).Value
            );
        }
    }
}