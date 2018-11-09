using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Units
{
    [UpdateAfter(typeof(TargetDetection))]
    [UsedImplicitly]
    public class ProcessTarget : ComponentSystem
    {
        private ComponentGroup _unitInfoGroup;
        
        protected override void OnCreateManager()
        {
            _unitInfoGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(Target)),
                ComponentType.ReadOnly(typeof(Attack))
            );
        }
        
        protected override void OnUpdate()
        {
            var targets = _unitInfoGroup.GetComponentDataArray<Target>();
            var attacks = _unitInfoGroup.GetComponentArray<Attack>();
            var entities = _unitInfoGroup.GetEntityArray();
            
            for (var i = 0; i < entities.Length; ++i)
            {
                var target = targets[i];

                var dam = EntityManager.GetBuffer<Damage>(target.Entity);
                dam.Add(new Damage
                {
                    Value = attacks[i].Value
                });
                
                PostUpdateCommands.RemoveComponent<Target>(entities[i]); 
            }
        }
    }
}