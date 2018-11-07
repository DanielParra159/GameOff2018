using Components.Common;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Common
{
    [UpdateAfter(typeof(MoveSystem))]
    [UsedImplicitly]
    public class SyncTransformPositionSystem : ComponentSystem
    {
        private struct Data
        {
#pragma warning disable 649
            [ReadOnly] public Position2D Position;
            public Transform TransformOutput;
#pragma warning restore 649
        }

        protected override void OnUpdate()
        {
            foreach (var entity in GetEntities<Data>())
            {

                var position = entity.Position.Value;
                entity.TransformOutput.position = new float3(position.x, position.y, 0);
            }
        }
    }
}