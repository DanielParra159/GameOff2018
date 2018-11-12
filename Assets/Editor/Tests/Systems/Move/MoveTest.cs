using Components.Common;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Tests.Systems.Move
{
    [TestFixture]
    public class MoveTest : BaseTest
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void MoveEntity(int speed)
        {
            // Arrange
            var entity = EntityManager.CreateEntity(typeof(Position2D),
                typeof(Heading2D),
                typeof(MoveSpeed));
            EntityManager.SetComponentData(entity, new Heading2D {Value = new float2 {x = 1, y = 0}});
            EntityManager.SetComponentData(entity, new MoveSpeed {Value = speed});

            // Act
            World.CreateManager<global::Systems.Common.Move>().Update();
            
            // Assert
            Assert.AreEqual(new float2 {x = speed * Time.deltaTime, y = 0}, EntityManager.GetComponentData<Position2D>(entity).Value);
        }
    }
}