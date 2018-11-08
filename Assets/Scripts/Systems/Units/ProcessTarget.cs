using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Units
{
    [UpdateAfter(typeof(EnemyDetection))]
    [UsedImplicitly]
    public class ProcessTarget : ComponentSystem
    {
        private ComponentGroup _unitInfoGroup;
        
        protected override void OnCreateManager()
        {
            _unitInfoGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(Target))
            );
        }
        
        protected override void OnUpdate()
        {
            var entities = _unitInfoGroup.GetEntityArray();
            for (var i = 0; i < entities.Length; ++i)
            {
                PostUpdateCommands.RemoveComponent<Target>(entities[i]); 
            }
        }
    }
}