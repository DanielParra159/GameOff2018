using Systems.Units;
using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
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
                ComponentType.Subtractive(typeof(Target))
            );
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            
            var positions = _movementGroup.GetComponentArray<Position2D>();
            var headings = _movementGroup.GetComponentArray<Heading2D>();
            var moveSpeeds = _movementGroup.GetComponentArray<MoveSpeed>();

            for (int i = 0; i < positions.Length; ++i)
            {
                positions[i].Value += headings[i].Value * moveSpeeds[i].Value * deltaTime;
            }
        }
    }
}