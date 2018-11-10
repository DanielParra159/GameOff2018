using Systems.Units;
using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Common
{
    [UpdateAfter(typeof(TargetDetection))]
    [UpdateBefore(typeof(ProcessTarget))]
    [UsedImplicitly]
    public class Move : ComponentSystem
    {
        private ComponentGroup _movementGroup;

        protected override void OnCreateManager()
        {
            _movementGroup = GetComponentGroup(
                typeof(Position2D),
                ComponentType.ReadOnly(typeof(Heading2D)),
                ComponentType.ReadOnly(typeof(MoveSpeed)),
                ComponentType.Subtractive(typeof(Target)),
                ComponentType.Subtractive(typeof(Dying))
            );
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;

            var positions = _movementGroup.GetComponentDataArray<Position2D>();
            var headings = _movementGroup.GetComponentDataArray<Heading2D>();
            var moveSpeeds = _movementGroup.GetComponentDataArray<MoveSpeed>();

            for (int i = 0; i < positions.Length; ++i)
            {
                positions[i] = new Position2D
                {
                    Value = positions[i].Value + headings[i].Value * moveSpeeds[i].Value * deltaTime
                };
            }
        }
    }
}