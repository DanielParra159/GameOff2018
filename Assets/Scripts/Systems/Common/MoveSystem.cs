using Components.Common;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;

namespace Systems.Common
{
    [UsedImplicitly]
    public class MoveSystem : ComponentSystem
    {
        private struct Data
        {
#pragma warning disable 649
            public Position2D Position;
            public Heading2D Heading;
            public MoveSpeed MoveSpeed;
#pragma warning restore 649
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            foreach (var entity in GetEntities<Data>())
            {
                var position = entity.Position;
                position.Value += entity.Heading.Value * entity.MoveSpeed.Value * deltaTime;
            }
        }
    }
}